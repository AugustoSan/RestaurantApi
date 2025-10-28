using Restaurant.Api.Core.Entities;
namespace Restaurant.Api.Core.Interfaces;

public interface ICategoryRepository {
    Task<List<Category>> GetAllCategories();
    Task<List<Product>> GetAllProductsByCategoryId(Guid categoryId);
    Task AddCategory(Category category);
    Task UpdateCategory(Guid id, Category category);
    Task DeleteCategory(Guid id);
}

