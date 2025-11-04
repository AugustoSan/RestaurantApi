using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infraestructure.Repositories;
using Restaurant.Api.Infrastructure.Configuration;
using Restaurant.Api.Infrastructure.Persistance.Seeders;
using Restaurant.Api.Infrastructure.Repositories;
using Restaurant.Api.Infrastructure.Utils;

namespace Restaurant.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureMongoDb.Configure();

        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDbSettings"));
        
        services.AddSingleton<IMongoClient>(serviceProvider => 
            new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString")));
        
        services.AddScoped(serviceProvider => {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        // services.AddScoped<IRestaurantRepository, RestaurantRepository>();

        services.AddScoped<IAuthRepository, AuthRepository>();

        services.AddScoped<ISeederPersistance, SeederPersistance>();
        return services;
    }
}
