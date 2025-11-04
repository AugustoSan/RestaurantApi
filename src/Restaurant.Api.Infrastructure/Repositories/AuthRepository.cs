using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Utils;

namespace Restaurant.Api.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository {
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    public AuthRepository(IUserRepository userRepository, IJwtService jwtService) {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    public async Task<Auth> Login(string username, string password) {
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null) {
            throw new Exception("User not found");
        }
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) {
            throw new Exception("Invalid password");
        }
        var auth = new Auth {
            Token = _jwtService.GetSessionToken(user),
            RefreshToken = _jwtService.GetRefreshToken(user)
        };
        return auth;
    }
    public Task Logout() {
        throw new NotImplementedException();
    }
}