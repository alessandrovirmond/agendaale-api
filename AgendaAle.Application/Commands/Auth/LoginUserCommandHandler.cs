using AgendaAle.Application.Services;
using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using MediatR;

namespace AgendaAle.Application.Commands.Auth;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

public LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService; 
    }
   public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
{
    var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

    if (user == null)
    {
        user = new User(request.Email, request.Name, request.ExternalAuthId);
        _userRepository.Add(user);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    var token = _tokenService.GenerateToken(user);

    return token;
}
}