using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface IProductRepository {
    Task<List<Product>> GetAllProducts();
    Task<List<Section>> GetSections();
    Task<List<Product>> GetAllProductsBySectionId(Guid sectionId);
}

