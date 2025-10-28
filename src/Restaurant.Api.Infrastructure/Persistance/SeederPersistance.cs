using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Infrastructure.Persistance.Seeders;

public class SeederPersistance(
    IMongoDatabase database,
    ICategoryRepository categoryRepository
) : ISeederPersistance
{
    private readonly IMongoDatabase _database = database;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    
    public async Task SeedData()
    {
        await SeedCategories();
    }

    private async Task SeedCategories()
    {
        var categories = await _categoryRepository.GetAllCategories();
        if (categories.Count == 0)
        {
            await _categoryRepository.AddCategory(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Bebidas calientes",
                Products = new List<Product>{
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Café capuchino", 
                        Price = 40.00,
                        Description = "Café capuchino",
                    },
                    new() {
                        Id = Guid.NewGuid(),
                        Name = "Café americano", 
                        Price = 40.00,
                        Description = "Café americano",
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Café latte", 
                        Price = 40.00,
                        Description = "Café latte",
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Café macchiato", 
                        Price = 40.00,
                        Description = "Café macchiato",
                    }
                }
            });
            await _categoryRepository.AddCategory(new Category
            {
                Id = Guid.NewGuid(),
                Name = "Bebidas frías",
                Products = new List<Product>{
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frappuccino", 
                        Price = 40.00,
                        Description = "Frappuccino",
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé de taro", 
                        Price = 40.00,
                        Description = "Frapé de taro",
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé de yogurt", 
                        Price = 40.00,
                        Description = "Frapé de yogurt",
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé cookies & cream", 
                        Price = 40.00,
                        Description = "Frapé cookies & cream",
                    },
                    new(){
                        Id = Guid.NewGuid(),
                        Name = "Frapé de mazapán", 
                        Price = 40.00,
                        Description = "Frapé de mazapán",
                    }
                }
            });
        }
    }
}
