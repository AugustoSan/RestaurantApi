namespace Restaurant.Api.Application.Establishment.Dtos;

public class EstablishmentDto {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Token { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Logo { get; set; }
}