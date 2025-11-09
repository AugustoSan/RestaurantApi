using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Restaurant.Api.Core.Exceptions;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Core.Options;
using Restaurant.Api.Infraestructure.Repositories;
using Restaurant.Api.Infrastructure.Configuration;
using Restaurant.Api.Infrastructure.Persistance.Seeders;
using Restaurant.Api.Infrastructure.Repositories;
using Restaurant.Api.Infrastructure.Utils;
using System.Text;

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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

                if (jwtSettings == null)
                {
                    throw new InfrastructureException("JwtSettings no se encontraron en la configuración");
                }
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero // No margin for token expiration time
                };

                // Handle token validation events for custom validation
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // You can add custom validation here
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.Headers["Token-Expired"] = "true";
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        // services.AddScoped<IRestaurantRepository, RestaurantRepository>();

        services.AddScoped<IAuthRepository, AuthRepository>();

        services.Configure<InitialValue>(configuration.GetSection("InitialValue"));

        services.AddScoped<ISeederPersistance, SeederPersistance>();
        return services;
    }
}
