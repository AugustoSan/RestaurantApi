using MediatR;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Commands.Registry;

public class RegistryCommandHandler( IUserRepository userRepository) : IRequestHandler<RegistryCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Guid> Handle(RegistryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = UserMapper.Registry(request);
            await _userRepository.AddUser(user);
            return user.Id;
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}