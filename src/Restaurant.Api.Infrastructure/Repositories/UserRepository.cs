using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Core.Options;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infraestructure.Repositories;

public class UserRepository : IUserRepository {
    private readonly IMongoCollection<User> _userCollection;
    private readonly UserOptions userOptions;

    public UserRepository(IMongoDatabase database, IOptions<InitialValue> settings)
    {
        userOptions = settings.Value.Administrator;
        _userCollection = database.GetCollection<User>("User");
        // Create collection if it doesn't exist
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Any(x => x == "User"))
        {
            database.CreateCollection("User");

            var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(u => u.Username);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<User>(indexKeysDefinition, indexOptions);
            _userCollection.Indexes.CreateOne(indexModel);
        }
    }
    

    public async Task<List<User>> GetAllUsers() {
        // Usamos SelectMany para aplanar la lista de listas de productos en una sola lista.
        return await _userCollection.Find(_ => true).ToListAsync();
    }
    
    public async Task<User?> GetUserById(Guid id) {
        return await _userCollection.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUsername(string username) {
        return await _userCollection.Find(s => s.Username == username).FirstOrDefaultAsync();
    }   

    public async Task AddUser(User user) {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await _userCollection.InsertOneAsync(user);
    }

    public async Task UpdateUser(Guid id, User user) {
        await _userCollection.ReplaceOneAsync(p => p.Id == id, user);
    }

    public async Task DeleteUser(Guid id)
    {
        var administrator = await GetUserByUsername(userOptions.Username);
        if (administrator != null && administrator.Id == id) return;

        await _userCollection.DeleteOneAsync(p => p.Id == id);
    }

    public async Task ChangePassword(Guid id, string password, string newPassword)
    {
        var user = await GetUserById(id);
        if (user == null) return;
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) return;
        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await UpdateUser(id, user);
    }

    public async Task ChangeRole(Guid id, Guid roleId)
    {
        var user = await GetUserById(id);
        if (user == null) return;
        user.RoleId = roleId;
        await UpdateUser(id, user);
    }

}
