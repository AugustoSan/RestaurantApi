using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface IProductRepository {
    Task<List<Product>> GetAllProductsByCategoryId(Guid categoryId);
    Task<Product?> GetProductById(Guid categoryId, Guid id);
    Task AddProduct(Guid categoryId, Product product);
    Task UpdateProduct(Guid categoryId, Guid id, Product product);
    Task DeleteProduct(Guid categoryId, Guid id);
}

