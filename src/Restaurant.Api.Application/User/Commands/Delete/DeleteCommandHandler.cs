using MediatR;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Commands.Delete;

public class DeleteCommandHandler( IUserRepository userRepository) : IRequestHandler<DeleteCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Guid> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(Guid.Parse(request.Id));
            if (user == null) return Guid.Empty;
            await _userRepository.DeleteUser(user.Id);
            return user.Id;
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}