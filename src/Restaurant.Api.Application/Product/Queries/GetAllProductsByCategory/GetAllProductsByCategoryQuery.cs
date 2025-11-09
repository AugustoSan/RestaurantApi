using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Application.Common.Models;

namespace Restaurant.Api.Application.Product.Queries.GetAllProductsByCategory;

public class GetAllProductsByCategoryQuery : PagedQueryOptionsBase<ProductDto>
{
    public required string CategoryId { get; set; }
}