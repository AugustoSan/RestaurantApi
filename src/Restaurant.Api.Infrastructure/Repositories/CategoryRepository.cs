using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infraestructure.Repositories;

public class CategoryRepository : ICategoryRepository {
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryRepository(IMongoDatabase database, IOptions<MongoDbSettings> settings)
    {
        _categoryCollection = database.GetCollection<Category>("Category");

        // Create collection if it doesn't exist
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Any(x => x == "Category"))
        {
            database.CreateCollection("Category");
        }
    }
    

    public async Task<List<Category>> GetAllCategories() {
        // Usamos SelectMany para aplanar la lista de listas de productos en una sola lista.
        return await _categoryCollection.Find(_ => true).ToListAsync();
    }

    public async Task<List<Product>> GetAllProductsByCategoryId(Guid categoryId) {
        Category category = await _categoryCollection.Find(s => s.Id == categoryId).FirstOrDefaultAsync();
        return category?.Products ?? new List<Product>();
    }

    public async Task AddCategory(Category category) {
        await _categoryCollection.InsertOneAsync(category);
    }

    public async Task UpdateCategory(Guid id, Category category) {
        await _categoryCollection.ReplaceOneAsync(p => p.Id == id, category);
    }

    public async Task DeleteCategory(Guid id) {
        await _categoryCollection.DeleteOneAsync(p => p.Id == id);
    }
}
