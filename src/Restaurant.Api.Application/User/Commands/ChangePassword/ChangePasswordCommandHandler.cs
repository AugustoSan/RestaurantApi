using MediatR;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.User.Mapper;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.User.Commands.ChangePassword;

public class ChangePasswordCommandHandler( IUserRepository userRepository) : IRequestHandler<ChangePasswordCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Guid> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserById(Guid.Parse(request.Id));
            if (user == null) return Guid.Empty;
            await _userRepository.ChangePassword(user.Id, request.Password, request.NewPassword);
            return Guid.Empty;
        }
        catch (System.Exception)
        {
            throw;
        }
    
    }
}