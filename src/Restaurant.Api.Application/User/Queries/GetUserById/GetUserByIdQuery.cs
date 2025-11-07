using MediatR;
using Restaurant.Api.Application.User.Dtos;

namespace Restaurant.Api.Application.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public required string Id { get; set; }
    }
}