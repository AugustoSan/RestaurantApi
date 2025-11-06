using MediatR;
using Restaurant.Api.Application.User.Queries.GetAllUsers;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Application.Common.Models;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(
    IUserRepository userRepository
) : IRequestHandler<GetAllUsersQuery, PagedResponse<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<PagedResponse<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers();
        var userDtos = users.Select(UserMapper.ToDto).ToList();
        return new PagedResponse<UserDto>(
            source: userDtos, 
            pageSize: request.PageSize, 
            currentPage: request.CurrentPage
        );
    }
}
