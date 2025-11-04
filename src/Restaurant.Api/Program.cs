using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using Restaurant.Api.Application.Extensions;
using Restaurant.Api.Infrastructure.Extensions;
using Restaurant.Api.Infrastructure.Persistance.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    }
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.ListenAnyIP(80);
//     serverOptions.ListenAnyIP(443, listenOptions =>
//     {
//         listenOptions.UseHttps(httpsOptions =>
//         {
//             var certPath = builder.Configuration["Kestrel:Certificates:Default:Path"];
//             var certPassword = builder.Configuration["Kestrel:Certificates:Default:Password"];
            
//             if (File.Exists(certPath))
//             {
//                 httpsOptions.ServerCertificate = new X509Certificate2(certPath, certPassword);
//             }
//         });
//     });
// });

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? context.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var seederPersistance = scope.ServiceProvider.GetRequiredService<ISeederPersistance>();
await seederPersistance.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();


app.Run();
