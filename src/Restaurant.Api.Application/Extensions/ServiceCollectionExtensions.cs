using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using Restaurant.Api.Application.Common.Behaviors;
namespace Restaurant.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

        // Configura el comportamiento de validación
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>)); // Es importante registrar ambos
        return services;
    }
}
