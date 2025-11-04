using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Exceptions;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Filters;
using Restaurant.Api.Infrastructure.Utils;

namespace Restaurant.Api.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository 
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMongoCollection<RefreshTokenUser> _refreshTokenUserCollection;
    private readonly int _refreshTokenExpiration;
    private readonly ILogger<AuthRepository> _logger;

    public AuthRepository(
        IUserRepository userRepository, 
        IJwtService jwtService, 
        IOptions<JwtSettings> settings, 
        IMongoDatabase database,
        ILogger<AuthRepository> logger) 
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _refreshTokenUserCollection = database.GetCollection<RefreshTokenUser>("RefreshTokenUser");
        _refreshTokenExpiration = settings.Value.TokenRefreshExpiration;
        _logger = logger;

        // Initialize collection if it doesn't exist
        try
        {
            var collections = database.ListCollectionNames().ToList();
            if (!collections.Any(x => x == "RefreshTokenUser"))
            {
                database.CreateCollection("RefreshTokenUser");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al inicializar la colección RefreshTokenUser");
            throw new DatabaseConnectionException("Error al inicializar la base de datos", ex);
        }
    }

    public async Task<Auth> Login(string username, string password) 
    {
        try
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                _logger.LogWarning("Intento de inicio de sesión fallido: Usuario no encontrado - {Username}", username);
                throw new UnauthorizedException("Credenciales inválidas");
            }

            if (!_jwtService.VerifyHash(password, user.Password))
            {
                _logger.LogWarning("Intento de inicio de sesión fallido: Contraseña incorrecta para el usuario - {Username}", username);
                throw new UnauthorizedException("Credenciales inválidas");
            }

            string refreshToken = _jwtService.GetRefreshToken(user);
            var refreshTokenUser = new RefreshTokenUser 
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenExpiration)
            };

            await SaveRefreshToken(refreshTokenUser);
            
            _logger.LogInformation("Inicio de sesión exitoso para el usuario: {Username}", username);
            
            return new Auth 
            {
                Token = _jwtService.GetSessionToken(user),
                RefreshToken = refreshToken
            };
        }
        catch (UnauthorizedException)
        {
            throw; // Re-lanzar excepciones de autorización
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en el proceso de inicio de sesión para el usuario: {Username}", username);
            throw new AppException("Ocurrió un error al intentar iniciar sesión", ex);
        }
    }

    public async Task<Auth> RefreshToken(string refreshToken) 
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedException("Token de actualización no proporcionado");
        }

        try
        {
            var refreshTokenUser = await FindRefreshTokenByToken(refreshToken);
            if (refreshTokenUser == null)
            {
                _logger.LogWarning("Intento de refrescar token fallido: Token no encontrado");
                throw new UnauthorizedException("Token de actualización inválido");
            }

            if (refreshTokenUser.RefreshTokenExpiration < DateTime.UtcNow)
            {
                _logger.LogWarning("Intento de refrescar token fallido: Token expirado");
                throw new UnauthorizedException("El token de actualización ha expirado");
            }

            var user = await _userRepository.GetUserById(refreshTokenUser.UserId);
            if (user == null)
            {
                _logger.LogError("Usuario no encontrado para el token de actualización: {UserId}", refreshTokenUser.UserId);
                throw new AppException("Error al procesar la solicitud");
            }

            string newRefreshToken = _jwtService.GetRefreshToken(user);
            refreshTokenUser.RefreshToken = newRefreshToken;
            refreshTokenUser.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenExpiration);

            await SaveRefreshToken(refreshTokenUser);
            
            _logger.LogInformation("Token refrescado exitosamente para el usuario: {UserId}", user.Id);
            
            return new Auth 
            {
                Token = _jwtService.GetSessionToken(user),
                RefreshToken = newRefreshToken
            };
        }
        catch (UnauthorizedException)
        {
            throw; // Re-lanzar excepciones de autorización
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al refrescar el token");
            throw new AppException("Ocurrió un error al intentar refrescar el token", ex);
        }
    }

    public async Task Logout(Guid userId) 
    {
        try
        {
            await _refreshTokenUserCollection.DeleteManyAsync(x => x.UserId == userId);
            _logger.LogInformation("Sesión cerrada exitosamente para el usuario: {UserId}", userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cerrar la sesión del usuario: {UserId}", userId);
            throw new AppException("Ocurrió un error al cerrar la sesión", ex);
        }
    }

    private async Task<RefreshTokenUser?> FindRefreshTokenByToken(string refreshToken)
    {
        try
        {
            return await RepositoryExceptionFilter.ExecuteWithExceptionHandlingAsync(
                () => _refreshTokenUserCollection
                    .Find(s => s.RefreshToken == refreshToken)
                    .FirstOrDefaultAsync(),
                "RefreshToken",
                refreshToken
            );
        }
        catch (EntityNotFoundException)
        {
            return null;
        }
    }

    private async Task SaveRefreshToken(RefreshTokenUser refreshTokenUser)
    {
        try
        {
            await RepositoryExceptionFilter.ExecuteWithExceptionHandlingAsync(
                async () => 
                {
                    await _refreshTokenUserCollection.ReplaceOneAsync(
                        s => s.UserId == refreshTokenUser.UserId, 
                        refreshTokenUser, 
                        new ReplaceOptions { IsUpsert = true });
                    return true;
                },
                "RefreshToken",
                refreshTokenUser.UserId
            );
        }
        catch (Exception ex) when (ex is not EntityNotFoundException)
        {
            _logger.LogError(ex, "Error al guardar el token de actualización para el usuario: {UserId}", 
                refreshTokenUser.UserId);
            throw;
        }
    }
}