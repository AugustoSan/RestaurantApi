using MediatR;
using Restaurant.Api.Application.Categories.Queries.GetAllCategories;
using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Category.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository
) : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategories();
        var categoryDtos = categories.Select(category => new CategoryDto
        {
            Id = category.Id.ToString(),
            Name = category.Name,
            Products = category.Products.Select(product => new ProductDto{
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Image = product.ImageUrl
            }).ToList(),
        }).ToList();
        return categoryDtos;
    }
}
