namespace Restaurant.Api.Core.Entities;

public class Role {
    public required Guid Id { get; set; }
    public required string Name {get; set; }
    public string? Description {get; set; }
    public DateTime? CreatedAt {get; set; }
    public DateTime? UpdatedAt {get; set; }
}
