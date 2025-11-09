namespace Restaurant.Api.Core.Options;

public class EstablishmentOptions
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Token { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Logo { get; set; }
    public string? Email { get; set; }
}


public class UserOptions
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class InitialValue
{
    public required EstablishmentOptions Establishment { set; get; }
    public required UserOptions Administrator { set; get; }
}