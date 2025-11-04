using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface IJwtService {
    string GetSessionToken(User user);
    string GetRefreshToken(User user);
    bool VerifySessionToken(string token, string username);
    bool VerifyRefreshToken(string token, string username);
    string GeneratePasswordHash(string password);
    bool VerifyPassword(string password, string hash);
}

