using MediatR;
using Restaurant.Api.Application.Auth.Dtos;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Auth.Commands.RefreshToken;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthDto>
{
    private readonly IAuthRepository _authRepository;
    public RefreshTokenCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public async Task<AuthDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _authRepository.RefreshToken(request.RefreshToken);
        return new AuthDto { Token = token.Token, RefreshToken = token.RefreshToken };
    }
}
