namespace Restaurant.Api.Core.Entities;

public class Restaurant{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Logo { get; set; } = string.Empty;
}