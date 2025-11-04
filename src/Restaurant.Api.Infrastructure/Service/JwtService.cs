using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Exceptions;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Infrastructure.Utils;

public record JwtSettings {
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int TokenSessionExpiration { get; set; }
    public required int TokenRefreshExpiration { get; set; }
}

public class JwtService : IJwtService 
{
    private readonly JwtSettings _settings;
    private readonly ILogger<JwtService> _logger;

    public JwtService(IOptions<JwtSettings> settings, ILogger<JwtService> logger)
    {
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        if (string.IsNullOrWhiteSpace(_settings.SecretKey))
        {
            throw new ArgumentException("La clave secreta no puede estar vacía", nameof(_settings.SecretKey));
        }
    }

    public string GetSessionToken(User user)
    {
        try
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return GenerateToken("session", user, _settings.TokenSessionExpiration);
        }
        catch (Exception ex) when (ex is not AppException)
        {
            _logger.LogError(ex, "Error al generar el token de sesión para el usuario: {UserId}", user?.Id);
            throw new AppException("Error al generar el token de sesión", ex);
        }
    }

    public string GetRefreshToken(User user)
    {
        try
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return GenerateToken("refresh_session", user, _settings.TokenRefreshExpiration);
        }
        catch (Exception ex) when (ex is not AppException)
        {
            _logger.LogError(ex, "Error al generar el token de actualización para el usuario: {UserId}", user?.Id);
            throw new AppException("Error al generar el token de actualización", ex);
        }
    }

    public bool VerifySessionToken(string token, string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new UnauthorizedException("El token no puede estar vacío");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new UnauthorizedException("El nombre de usuario no puede estar vacío");
            }

            return VerifyToken(token, "session", username);
        }
        catch (UnauthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar el token de sesión para el usuario: {Username}", username);
            throw new UnauthorizedException("Error al verificar el token de sesión");
        }
    }

    public bool VerifyRefreshToken(string token, string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new UnauthorizedException("El token de actualización no puede estar vacío");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new UnauthorizedException("El nombre de usuario no puede estar vacío");
            }

            return VerifyToken(token, "refresh_session", username);
        }
        catch (UnauthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar el token de actualización para el usuario: {Username}", username);
            throw new UnauthorizedException("Error al verificar el token de actualización");
        }
    }

    public string GenerateHash(string value)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El valor a encriptar no puede estar vacío", nameof(value));
            }

            return BCrypt.Net.BCrypt.HashPassword(value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar el hash");
            throw new AppException("Error al generar el hash de la contraseña", ex);
        }
    }

    public bool VerifyHash(string value, string hash)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El valor no puede estar vacío", nameof(value));
            }

            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new ArgumentException("El hash no puede estar vacío", nameof(hash));
            }

            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar el hash");
            throw new AppException("Error al verificar la contraseña", ex);
        }
    }

    private string GenerateToken(string action, User user, int expiration)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("action", action)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiration),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar el token para el usuario: {UserId}, acción: {Action}", 
                user?.Id, action);
            throw new AppException("Error al generar el token", ex);
        }
    }

    private bool VerifyToken(string token, string action, string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new UnauthorizedException("El token no puede estar vacío");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            
            if (!tokenHandler.CanReadToken(token))
            {
                throw new UnauthorizedException("Formato de token inválido");
            }

            var key = Encoding.UTF8.GetBytes(_settings.SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero // No permitir margen de tiempo para la expiración
            };

            try
            {
                // Validar el token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var tokenSub = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var tokenAction = principal.FindFirst("action")?.Value;
                var tokenIss = principal.FindFirst(JwtRegisteredClaimNames.Iss)?.Value;
                var tokenAud = principal.FindFirst(JwtRegisteredClaimNames.Aud)?.Value;

                if (tokenAction != action || 
                    tokenSub != username || 
                    tokenIss != _settings.Issuer || 
                    tokenAud != _settings.Audience)
                {
                    _logger.LogWarning("Token inválido para el usuario: {Username}, acción: {Action}", 
                        username, action);
                    return false;
                }

                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning($"Token expirado para el usuario: {username}", ex);
                throw new UnauthorizedException("El token ha expirado");
            }
            catch (SecurityTokenValidationException ex)
            {
                _logger.LogWarning($"Error de validación del token para el usuario: {username}", ex);
                throw new UnauthorizedException("Token inválido");
            }
        }
        catch (UnauthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al validar el token para el usuario: {username}", username);
            throw new UnauthorizedException("Error al validar el token");
        }
    }
}