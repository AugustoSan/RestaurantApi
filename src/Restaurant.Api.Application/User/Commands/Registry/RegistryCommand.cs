using MediatR;
using Restaurant.Api.Application.User.Dtos;

namespace Restaurant.Api.Application.User.Commands.Registry;

public class RegistryCommand : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string RoleId { get; set; }
}