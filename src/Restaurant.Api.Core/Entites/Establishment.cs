namespace Restaurant.Api.Core.Entities;

public class Establishment {
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Token { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Logo { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
}