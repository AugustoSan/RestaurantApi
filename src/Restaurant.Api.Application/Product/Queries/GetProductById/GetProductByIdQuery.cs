using MediatR;
using Restaurant.Api.Application.Category.Dtos;

namespace Restaurant.Api.Application.Product.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public required string Id { get; set; }
    public required string CategoryId { get; set; }
}