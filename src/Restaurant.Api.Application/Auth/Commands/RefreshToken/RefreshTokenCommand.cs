using MediatR;
using Restaurant.Api.Application.Auth.Dtos;

namespace Restaurant.Api.Application.Auth.Commands.RefreshToken;
public class RefreshTokenCommand(string RefreshToken) : IRequest<AuthDto>
{
    public string RefreshToken { get; set; } = RefreshToken;
    public string UserId { get; set; } = "";
}
