using MediatR;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Commands.ChangeRole;

public class ChangeRoleCommandHandler( IUserRepository userRepository) : IRequestHandler<ChangeRoleCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Guid> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(Guid.Parse(request.Id));
            if (user == null) return Guid.Empty;
            await _userRepository.ChangeRole(user.Id, Guid.Parse(request.RoleId));
            return Guid.Empty;
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}