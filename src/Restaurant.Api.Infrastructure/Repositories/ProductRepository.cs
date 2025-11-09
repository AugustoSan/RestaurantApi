using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Restaurant.Api.Core.Entities;
using Restaurant.Api.Core.Interfaces;
using Restaurant.Api.Infrastructure.Configuration;

namespace Restaurant.Api.Infraestructure.Repositories;

public class ProductRepository : IProductRepository {
    private readonly IMongoCollection<Category> _categoryCollection;

    public ProductRepository(IMongoDatabase database, IOptions<MongoDbSettings> settings)
    {
        _categoryCollection = database.GetCollection<Category>("Category");
        // Create collection if it doesn't exist
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Any(x => x == "Category"))
        {
            database.CreateCollection("Category");
        }
    }

    public async Task AddProduct(Guid categoryId, Product product) {
        Category? category = await GetCategoryById(categoryId);
        if (category != null)
        {
            category.Products.Add(product);
            await _categoryCollection.ReplaceOneAsync(p => p.Id == categoryId, category);
        }
    }

    public async Task UpdateProduct(Guid categoryId, Guid id, Product product) {
        Category? category = await GetCategoryById(categoryId);
        if (category != null)
        {
            var _product = category.Products.Find(p => p.Id == id);
            if (_product != null)
            {
                _product.Name = product.Name;
                _product.Price = product.Price;
                _product.Description = product.Description;
                _product.ImageUrl = product.ImageUrl;
                _product.Available = product.Available;
                category.Products.Remove(_product);
                category.Products.Add(product);
                await _categoryCollection.ReplaceOneAsync(p => p.Id == categoryId, category);
            }
        }
    }

    public async Task DeleteProduct(Guid categoryId, Guid id)
    {
        Category? category = await GetCategoryById(categoryId);
        if (category != null)
        {
            var _product = category.Products.Find(p => p.Id == id);
            if (_product != null)
            {
                category.Products.Remove(_product);
                await _categoryCollection.ReplaceOneAsync(p => p.Id == categoryId, category);
            }
        }
    }

    public async Task<List<Product>> GetAllProductsByCategoryId(Guid categoryId)
    {
        Category? category = await GetCategoryById(categoryId);
        return category?.Products ?? new List<Product>();
    }
    
    public async Task<Product?> GetProductById(Guid categoryId, Guid id)
    {
        Category? category = await GetCategoryById(categoryId);
        if (category != null)
        {
            return category.Products.Find(p => p.Id == id);
        }
        return null;
    }


    private async Task<Category?> GetCategoryById(Guid categoryId) {
        return await _categoryCollection.Find(p => p.Id == categoryId).FirstOrDefaultAsync();
    }
}
