using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface IUserRepository {
    Task AddUser(User user);
    Task UpdateUser(Guid id, User user);
    Task DeleteUser(Guid id);
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByUsername(string username);
    Task<List<User>> GetAllUsers();
    Task SaveRefreshTokenUser(RefreshTokenUser refreshTokenUser);
    Task<User?> GetUserByRefreshToken(string refreshToken);
    Task<RefreshTokenUser?> GetRefreshTokenUserByUser(Guid userId);
}

