using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface IRoleRepository {
    Task AddRole(Role role);
    Task UpdateRole(Guid id, Role role);
    Task DeleteRole(Guid id);
    Task<Role?> GetRoleById(Guid id);
    Task<Role?> GetRoleByName(string name);
    Task<List<Role>> GetAllRoles();
}

