using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infraestructure.Repositories;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDbSettings"));
        
        services.AddSingleton<IMongoClient>(serviceProvider => 
            new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString")));
        
        services.AddScoped(serviceProvider => {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
