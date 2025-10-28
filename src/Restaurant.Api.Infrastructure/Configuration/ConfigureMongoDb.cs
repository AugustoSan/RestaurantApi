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
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(Category)))
        {
            BsonClassMap.RegisterClassMap<Category>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                  .SetIdGenerator(CombGuidGenerator.Instance);
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