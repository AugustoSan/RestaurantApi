using MediatR;

namespace Restaurant.Api.Application.Establishment.Commands;

public class UpdateEstablishmentCommand : IRequest<Guid>
{
    public required string Id { set; get; }
    public required string Token { set; get; }
    public string? Name { set; get; }
    public string? Description { set; get; }
    public string? Address { set; get; }
    public string? Phone { set; get; }
    public string? Logo { set; get; }
    public string? Email { set; get; }
}