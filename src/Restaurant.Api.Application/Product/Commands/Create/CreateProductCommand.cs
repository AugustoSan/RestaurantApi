using MediatR;

namespace Restaurant.Api.Application.Product.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public required string CategoryId { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}