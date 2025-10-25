namespace Restaurant.Api.Application.Products.Dtos;

public class ProductDto {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
}