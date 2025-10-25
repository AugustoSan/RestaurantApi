using Microsoft.Extensions.DependencyInjection;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infraestructure.Repositories;

namespace Restaurant.Api.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
