using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Application.Product.Commands.CreateProduct;
using Restaurant.Api.Application.Product.Commands.UpdateProduct;
using ProductCore = Restaurant.Api.Core.Entities.Product;

namespace Restaurant.Api.Application.Category.Mapper;

public class ProductMapper {
    public static ProductDto ToDto(ProductCore product) {
        return new ProductDto {
            Id = product.Id.ToString(),
            Name = product.Name,
            Price = product.Price,
            Available = product.Available,
            Description = product.Description,
            Image = product.ImageUrl
        };
    }
    public static ProductCore Create(CreateProductCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        return new ProductCore
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Available = true
        };
    }
    public static ProductCore ToUpdate(UpdateProductCommand command, ProductCore Product)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        return new ProductCore
        {
            Id = Product.Id,
            Name = command.Name ?? Product.Name,
            Price = command.Price ?? Product.Price,
            Description = command.Description ?? Product.Description,
            ImageUrl = command.ImageUrl ?? Product.ImageUrl,
            Available = Product.Available
        };
    }
    
    public static ProductCore ToEntity(ProductDto productDto) {
        return new ProductCore {
            Id = Guid.Parse(productDto.Id),
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description,
            ImageUrl = productDto.Image,
            Available = productDto.Available
        };
    }
}