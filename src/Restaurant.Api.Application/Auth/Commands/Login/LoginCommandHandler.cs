using MediatR;
using Restaurant.Api.Application.Auth.Dtos;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Auth.Commands.Login;

public class LoginCommandHandler( IAuthRepository authRepository) : IRequestHandler<LoginCommand, AuthDto>
{
    private readonly IAuthRepository _authRepository = authRepository;
    public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var token = await _authRepository.Login(request.Username, request.Password);
        return new AuthDto { Token = token.Token, RefreshToken = token.RefreshToken };
    }
}