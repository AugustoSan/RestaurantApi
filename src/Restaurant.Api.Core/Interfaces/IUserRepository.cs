using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface IUserRepository {
    Task AddUser(User user);
    Task UpdateUser(Guid id, User user);
    Task ChangePassword(Guid id, string password, string newPassword);
    Task ChangeRole(Guid id, Guid roleId);
    Task DeleteUser(Guid id);
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByUsername(string username);
    Task<List<User>> GetAllUsers();
}

