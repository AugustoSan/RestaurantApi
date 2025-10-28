using System;
using System.Linq;
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

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
