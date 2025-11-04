using MediatR;
using Restaurant.Api.Application.Auth.Dtos;

namespace Restaurant.Api.Application.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthDto>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}