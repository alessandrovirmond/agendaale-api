using MediatR;

namespace AgendaAle.Application.Commands.Auth;

public class LoginUserCommand : IRequest<string>
{
    public string ExternalAuthId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}