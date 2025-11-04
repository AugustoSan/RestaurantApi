namespace Restaurant.Api.Application.Auth.Dtos;

public class AuthDto {
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}