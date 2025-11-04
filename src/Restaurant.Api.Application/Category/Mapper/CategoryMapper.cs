using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Core.Entities;
using CoreCategory = Restaurant.Api.Core.Entities.Category;

namespace Restaurant.Api.Application.Category.Mapper;

public class CategoryMapper {
    public static CategoryDto ToDto(CoreCategory category) {
        if (category == null)
            throw new ArgumentNullException(nameof(category));
        return new CategoryDto {
            Id = category.Id.ToString(),
            Name = category.Name,
            Products = category.Products.Select(product => ProductMapper.ToDto(product)).ToList() ?? new List<ProductDto>(),
        };
    }
    public static CoreCategory ToEntity(CategoryDto categoryDto) {
        if (categoryDto == null)
            throw new ArgumentNullException(nameof(categoryDto));
        return new CoreCategory {
            Id = Guid.Parse(categoryDto.Id),
            Name = categoryDto.Name,
            Products = categoryDto.Products.Select(product => ProductMapper.ToEntity(product)).ToList() ?? new List<Product>(),
        };
    }
}