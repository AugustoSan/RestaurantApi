namespace Restaurant.Api.Core.Entities;

public class Product {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required double Price {get; set; }
    public required string Category { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}

