using Restaurant.Api.Core.Entities;

namespace Restaurant.Api.Core.Interfaces;

public interface IAuthRepository {
    Task<Auth> Login(string username, string password);
    Task Logout();
}
