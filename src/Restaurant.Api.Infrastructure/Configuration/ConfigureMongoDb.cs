using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Restaurant.Api.Core.Entities;

namespace Restaurant.Api.Infrastructure.Configuration;

public static class ConfigureMongoDb
{
    public static void Configure()
    {
        // Configurar serializador de GUID
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        
        // Configurar el mapeo de clases
        if (!BsonClassMap.IsClassMapRegistered(typeof(Product)))
        {
            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(p => p.Id)
                  .SetIdGenerator(CombGuidGenerator.Instance);
                
                cm.MapMember(p => p.Name).SetElementName("name");
                cm.MapMember(p => p.Price).SetElementName("price");
                cm.MapMember(p => p.Description).SetElementName("description");
                cm.MapMember(p => p.ImageUrl).SetElementName("imageUrl");
                cm.MapMember(p => p.Available).SetElementName("available");
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(Category)))
        {
            BsonClassMap.RegisterClassMap<Category>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                  .SetIdGenerator(CombGuidGenerator.Instance);
                
                cm.MapMember(c => c.Name).SetElementName("name");
                cm.MapMember(c => c.Products).SetElementName("products");
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(u => u.Id)
                  .SetIdGenerator(CombGuidGenerator.Instance);
                
                cm.MapMember(u => u.Name).SetElementName("name");
                cm.MapMember(u => u.Username).SetElementName("username");
                cm.MapMember(u => u.Password).SetElementName("password");
                cm.MapMember(u => u.RoleId).SetElementName("roleId");
                cm.MapMember(u => u.CreatedAt).SetElementName("createdAt");
                cm.MapMember(u => u.UpdatedAt).SetElementName("updatedAt");
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(RefreshTokenUser)))
        {
            BsonClassMap.RegisterClassMap<RefreshTokenUser>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(u => u.UserId)
                  .SetIdGenerator(CombGuidGenerator.Instance);
                
                cm.MapMember(u => u.RefreshToken).SetElementName("refreshToken");
                cm.MapMember(u => u.RefreshTokenExpiration).SetElementName("refreshTokenExpiration");
            });
        }

        // Configurar convenciones
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true)
        };
        ConventionRegistry.Register("MyConventions", pack, t => true);
    }
}