namespace Restaurant.Api.Application.Category.Dtos;

public class CategoryDto {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required List<ProductDto> Products { get; set; }
}