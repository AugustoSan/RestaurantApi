using MediatR;
using Restaurant.Api.Application.User.Dtos;

namespace Restaurant.Api.Application.User.Commands.Delete;

public class DeleteCommand : IRequest<Guid>
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
}