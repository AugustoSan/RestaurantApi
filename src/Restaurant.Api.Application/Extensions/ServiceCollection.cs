using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Restaurant.Api.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
