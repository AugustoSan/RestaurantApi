using MediatR;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Commands.Update;

public class UpdateCommandHandler( IUserRepository userRepository) : IRequestHandler<UpdateCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Guid> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(Guid.Parse(request.Id));
            if (user == null) return Guid.Empty;
            var userUpdate = UserMapper.ToUpdate(request, user);
            await _userRepository.UpdateUser(user.Id, userUpdate);
            return user.Id;
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}