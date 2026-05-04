using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using MediatR;

namespace AgendaAle.Application.Commands.Auth;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            user = new User(request.Email, request.Name, request.ExternalAuthId);
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        return user.Id;
    }
}