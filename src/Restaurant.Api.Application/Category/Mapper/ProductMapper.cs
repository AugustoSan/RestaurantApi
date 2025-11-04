using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Core.Entities;

namespace Restaurant.Api.Application.Category.Mapper;

public class ProductMapper {
    public static ProductDto ToDto(Product product) {
        return new ProductDto {
            Id = product.Id.ToString(),
            Name = product.Name,
            Price = product.Price,
            Available = product.Available,
            Description = product.Description,
            Image = product.ImageUrl
        };
    }
    public static Product ToEntity(ProductDto productDto) {
        return new Product {
            Id = Guid.Parse(productDto.Id),
            Name = productDto.Name,
            Price = productDto.Price,
            Available = productDto.Available,
            Description = productDto.Description,
            ImageUrl = productDto.Image
        };
    }
}