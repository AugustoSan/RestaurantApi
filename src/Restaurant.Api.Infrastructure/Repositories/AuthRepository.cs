using Microsoft.Extensions.Options;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Utils;

namespace Restaurant.Api.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository {
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly int _refreshTokenExpiration;
    public AuthRepository(IUserRepository userRepository, IJwtService jwtService, IOptions<JwtSettings> settings) {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _refreshTokenExpiration = settings.Value.TokenRefreshExpiration;
    }
    public async Task<Auth> Login(string username, string password) {
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null) {
            throw new Exception("User not found");
        }
        if (!_jwtService.VerifyHash(password, user.Password)) {
            throw new Exception("Invalid password");
        }

        string refreshToken = _jwtService.GetRefreshToken(user);
        string hashRefreshToken = _jwtService.GenerateHash(refreshToken);
        
        RefreshTokenUser refreshTokenUser = new RefreshTokenUser {
            UserId = user.Id,
            RefreshToken = hashRefreshToken,
            RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenExpiration)
        };
        await _userRepository.SaveRefreshTokenUser(refreshTokenUser);
        var auth = new Auth {
            Token = _jwtService.GetSessionToken(user),
            RefreshToken = refreshToken
        };
        return auth;
    }
    public async Task<Auth> RefreshToken(string refreshToken) {
        var user = await _userRepository.GetUserByRefreshToken(refreshToken);
        if (user == null) {
            throw new Exception("User not found");
        }

        var refreshTokenUser = await _userRepository.GetRefreshTokenUserByUser(user.Id);
        if (refreshTokenUser == null) {
            throw new Exception("Refresh token not found");
        }
        if(refreshTokenUser.RefreshToken != refreshToken) {
            throw new Exception("Invalid refresh token");
        }

        string newRefreshToken = _jwtService.GetRefreshToken(user);
        string hashRefreshToken = _jwtService.GenerateHash(newRefreshToken);
        refreshTokenUser.RefreshToken = hashRefreshToken;
        refreshTokenUser.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenExpiration);
            

        await _userRepository.SaveRefreshTokenUser(refreshTokenUser);
        
        var auth = new Auth {
            Token = _jwtService.GetSessionToken(user),
            RefreshToken = newRefreshToken
        };
        return auth;
    }
    public Task Logout() {
        throw new NotImplementedException();
    }

}