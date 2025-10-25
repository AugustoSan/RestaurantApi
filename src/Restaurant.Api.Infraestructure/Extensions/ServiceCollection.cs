using System.Reflection;
using Microsoft.Extensions.DependencyInjection; 
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infraestructure.Repositories;

namespace Restaurant.Api.Infraestructure;

// La clase contenedora para el método de extensión DEBE ser estática
public static class InfraestructureServiceRegistration // Le di un nombre descriptivo a la clase estática
{
    // Ahora el método de extensión está dentro de una clase estática
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        // Esto es correcto para registrar un Singleton con una factory.
        // Sin embargo, si tu ProductRepository NO tiene dependencias en su constructor,
        // la forma más sencilla es:
        services.AddSingleton<IProductRepository, ProductRepository>();

        // Si ProductRepository SÍ tuviera dependencias, la factory sería útil.
        // Pero para el repositorio en memoria que tienes, no las necesita.
        // services.AddSingleton<IProductRepository, ProductRepository>(() => new ProductRepository());

        return services;
    }
}