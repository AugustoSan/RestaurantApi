using MediatR;
using Restaurant.Api.Application.User.Queries.GetUserById;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Application.Common.Models;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    IUserRepository userRepository
) : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(Guid.Parse(request.Id));
        if (user == null) return null;
        return UserMapper.ToDto(user);
    }
}
