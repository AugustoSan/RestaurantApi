using MediatR;

namespace Restaurant.Api.Application.Product.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Guid>
{
    public required string CategoryId { get; set; }
    public required string Id { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool? Available { get; set; }

}