using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infraestructure.Repositories;

public class EstablishmentRepository : IEstablishmentRepository {
    private readonly IMongoCollection<Establishment> _establishmentCollection;

    public EstablishmentRepository(IMongoDatabase database)
    {
        _establishmentCollection = database.GetCollection<Establishment>("Establishment");
        // Create collection if it doesn't exist
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Any(x => x == "Establishment"))
        {
            database.CreateCollection("Establishment");
        }
    }

    public async Task Update(Establishment restaurant)
    {

        var establishment = await GetInfo();
        if (establishment == null)
        {
            await _establishmentCollection.InsertOneAsync(restaurant);
        }
        else
        {
            await _establishmentCollection.ReplaceOneAsync(p => p.Id == establishment.Id, restaurant);
        }
        return;
    }
    
    public async Task<Establishment?> GetInfo()
    {
        var restaurant = await _establishmentCollection.Find(_ => true).ToListAsync();
        return restaurant.Count > 0 ? restaurant[0] : null;
    }

    // public override async Task ChangePassword(Guid id, string password, string newPassword)
    // {
    //     var user = await GetUserById(id);
    //     if (user == null) return;
    //     if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) return;
    //     user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
    //     await UpdateUser(id, user);
    // }

    // public async Task ChangeRole(Guid id, Guid roleId)
    // {
    //     var user = await GetUserById(id);
    //     if (user == null) return;
    //     user.RoleId = roleId;
    //     await UpdateUser(id, user);
    // }

}
