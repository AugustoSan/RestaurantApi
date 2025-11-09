// using Restaurant.Api.Application.Category.Dtos;
// using Restaurant.Api.Application.Product.Commands.CreateProduct;
// using Restaurant.Api.Application.Product.Commands.UpdateProduct;
// using Restaurant.Api.Core.Entities;
// using CoreProduct = Restaurant.Api.Core.Entities.Product;

// namespace Restaurant.Api.Application.Product.Mapper;

// public class ProductMapper {
//     public static ProductDto ToDto(CoreProduct Product) {
//         if (Product == null)
//             throw new ArgumentNullException(nameof(Product));
//         return new ProductDto {
//             Id = Product.Id.ToString(),
//             Name = Product.Name,
//             Price = Product.Price,
//             Description = Product.Description,
//             Image = Product.ImageUrl,
//             Available = Product.Available
//         };
//     }
//     public static CoreProduct Create(CreateProductCommand command)
//     {
//         if (command == null)
//             throw new ArgumentNullException(nameof(command));
//         return new CoreProduct
//         {
//             Id = Guid.NewGuid(),
//             Name = command.Name,
//             Price = command.Price,
//             Description = command.Description,
//             ImageUrl = command.ImageUrl,
//             Available = true
//         };
//     }
//     public static CoreProduct ToUpdate(UpdateProductCommand command, CoreProduct Product)
//     {
//         if (command == null)
//             throw new ArgumentNullException(nameof(command));
//         return new CoreProduct
//         {
//             Id = Product.Id,
//             Name = command.Name ?? Product.Name,
//             Price = command.Price ?? Product.Price,
//             Description = command.Description ?? Product.Description,
//             ImageUrl = command.ImageUrl ?? Product.ImageUrl,
//             Available = Product.Available
//         };
//     }
    
//     public static CoreProduct ToEntity(ProductDto productDto) {
//         return new CoreProduct {
//             Id = Guid.Parse(productDto.Id),
//             Name = productDto.Name,
//             Price = productDto.Price,
//             Description = productDto.Description,
//             ImageUrl = productDto.Image,
//             Available = productDto.Available
//         };
//     }
// }