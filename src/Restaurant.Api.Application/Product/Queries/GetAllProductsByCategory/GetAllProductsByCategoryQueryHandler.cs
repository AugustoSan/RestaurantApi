using MediatR;
using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Application.Category.Mapper;
using Restaurant.Api.Application.Common.Models;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Product.Queries.GetAllProductsByCategory;

public class GetAllProductsByCategoryQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<GetAllProductsByCategoryQuery, PagedResponse<ProductDto>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<PagedResponse<ProductDto>> Handle(GetAllProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsByCategoryId(Guid.Parse(request.CategoryId));
        var productDtos = products.Select(ProductMapper.ToDto).ToList();
        return new PagedResponse<ProductDto>(
            source: productDtos, 
            pageSize: request.PageSize, 
            currentPage: request.CurrentPage
        );
    }
}