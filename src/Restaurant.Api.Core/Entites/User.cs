namespace Restaurant.Api.Core.Entities;

public class User {
    public required Guid Id { get; set; }
    public required string Name {get; set; }
    public required string Username {get; set; }
    public required string Password {get; set; }
    public required Guid RoleId {get; set; }
    public required DateTime CreatedAt {get; set; }
    public required DateTime UpdatedAt {get; set; }
}

public class Auth {
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}
