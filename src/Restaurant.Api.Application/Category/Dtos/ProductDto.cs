using Restaurant.Api.Core.Entities;

namespace Restaurant.Api.Application.Category.Dtos;

public class ProductDto {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public required bool Available { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
}