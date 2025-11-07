using MediatR;
using Restaurant.Api.Application.User.Dtos;

namespace Restaurant.Api.Application.User.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<Guid>
{
    public required string Id { get; set; }
    public required string Password { get; set; }
    public required string NewPassword { get; set; }
}