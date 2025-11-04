using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infraestructure.Repositories;

public class RoleRepository : IRoleRepository {
    private readonly IMongoCollection<Role> _roleCollection;

    public RoleRepository(IMongoDatabase database)
    {
        _roleCollection = database.GetCollection<Role>("Role");

        // Create collection if it doesn't exist
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Any(x => x == "Role"))
        {
            database.CreateCollection("Role");
        }
    }
    

    public async Task<List<Role>> GetAllRoles() {
        // Usamos SelectMany para aplanar la lista de listas de productos en una sola lista.
        return await _roleCollection.Find(_ => true).ToListAsync();
    }
    
    public async Task<Role?> GetRoleById(Guid id) {
        return await _roleCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Role?> GetRoleByName(string name) {
        return await _roleCollection.Find(s => s.Name == name).FirstOrDefaultAsync();
    }   

    public async Task AddRole(Role role) {
        await _roleCollection.InsertOneAsync(role);
    }

    public async Task UpdateRole(Guid id, Role role) {
        await _roleCollection.ReplaceOneAsync(p => p.Id == id, role);
    }

    public async Task DeleteRole(Guid id) {
        await _roleCollection.DeleteOneAsync(p => p.Id == id);
    }

}
