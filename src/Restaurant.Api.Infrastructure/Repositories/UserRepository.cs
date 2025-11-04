using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infraestructure.Repositories;

public class UserRepository : IUserRepository {
    private readonly IMongoCollection<User> _userCollection;
    private readonly IMongoCollection<RefreshTokenUser> _refreshTokenUserCollection;

    public UserRepository(IMongoDatabase database)
    {
        _userCollection = database.GetCollection<User>("User");
        _refreshTokenUserCollection = database.GetCollection<RefreshTokenUser>("RefreshTokenUser");
        // Create collection if it doesn't exist
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Any(x => x == "User"))
        {
            database.CreateCollection("User");
        }
        if (!collections.Any(x => x == "RefreshTokenUser"))
        {
            database.CreateCollection("RefreshTokenUser");
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

    public async Task DeleteUser(Guid id) {
        await _userCollection.DeleteOneAsync(p => p.Id == id);
    }
    public async Task<User?> GetUserByRefreshToken(string refreshToken) {
        var refreshTokenUser = await _refreshTokenUserCollection.Find(s => s.RefreshToken == refreshToken).FirstOrDefaultAsync();
        return await _userCollection.Find(s => s.Id == refreshTokenUser.UserId).FirstOrDefaultAsync();
    }

    public async Task<RefreshTokenUser?> GetRefreshTokenUserByUser(Guid userId) {
        return await _refreshTokenUserCollection.Find(s => s.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task SaveRefreshTokenUser(RefreshTokenUser refreshTokenUser) {
        await _refreshTokenUserCollection.ReplaceOneAsync(s => s.UserId == refreshTokenUser.UserId, refreshTokenUser, new ReplaceOptions { IsUpsert = true });
    }

}
