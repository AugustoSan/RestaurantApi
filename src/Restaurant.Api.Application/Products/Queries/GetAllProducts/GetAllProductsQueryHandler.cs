using MediatR;
using Restaurant.Api.Application.Products.Dtos;
using Restaurant.Api.Application.Products.Queries.GetAllProductsQuery;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Products.Queries.GetAllProductsQuery;

public class GetAllProductsQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProducts();
        var productDtos = products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Image = product.ImageUrl
        }).ToList();
        return productDtos;
    }
}
