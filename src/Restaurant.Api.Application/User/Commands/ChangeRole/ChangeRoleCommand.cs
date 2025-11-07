using MediatR;
using Restaurant.Api.Application.User.Dtos;

namespace Restaurant.Api.Application.User.Commands.ChangeRole;

public class ChangeRoleCommand : IRequest<Guid>
{
    public required string Id { get; set; }
    public required string RoleId { get; set; }
}