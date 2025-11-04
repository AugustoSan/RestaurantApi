using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Infrastructure.Utils;

public record JwtSettings {
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int TokenSessionExpiration { get; set; }
    public required int TokenRefreshExpiration { get; set; }
}
public class JwtService(IOptions<JwtSettings> settings, ILogger<JwtService> logger) : IJwtService {
    private readonly JwtSettings _settings = settings.Value;
    private readonly ILogger<JwtService> _logger = logger;

    public string GetSessionToken(User user) {
        return GenerateToken("session", user, _settings.TokenSessionExpiration);
    }

    public string GetRefreshToken(User user) {
        return GenerateToken("refresh_session", user, _settings.TokenRefreshExpiration);
    }

    public bool VerifySessionToken(string token, string username) {
        return VerifyToken(token, "session", username);
    }

    public bool VerifyRefreshToken(string token, string username) {
        return VerifyToken(token, "refresh_session", username);
    }

    public bool VerifyPassword(string password, string hash) {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public string GeneratePasswordHash(string password) {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateToken(string action, User user, int expiration) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("action", action)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool VerifyToken(string token, string action, string username) {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            // Validar el token
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            var sub = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            var tokenSub = principal.FindFirst(sub)?.Value;
            var tokenAction = principal.FindFirst("action")?.Value;
            var tokenIss = principal.FindFirst("iss")?.Value;
            var tokenAud = principal.FindFirst("aud")?.Value;

            if (tokenAction == action && tokenSub == username && tokenIss == _settings.Issuer && tokenAud == _settings.Audience )
            {
                return true;  // Token válido
            }
            return false;  // El correo no coincide
        }
        catch (SecurityTokenExpiredException ex)
        {
            _logger.LogError(ex, $"Token expirado: {ex.Message}");
            return false; // Puedes retornar un código de estado o una respuesta más específica
        }
        catch (SecurityTokenSignatureKeyNotFoundException ex)
        {
            _logger.LogError(ex, $"Firma no encontrada: {ex.Message}");
            return false; // Puedes retornar un código de estado o una respuesta más específica
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al validar el token");
            return false;  // El token no es válido o ha expirado
        }
    }
}
