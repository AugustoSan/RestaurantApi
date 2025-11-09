using MediatR;
using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Application.Category.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Product.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(Guid.Parse(request.CategoryId), Guid.Parse(request.Id));
        if (product == null) return null;
        return ProductMapper.ToDto(product);
    }
}