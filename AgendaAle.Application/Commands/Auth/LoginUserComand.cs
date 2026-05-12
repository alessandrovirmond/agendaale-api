using MediatR;

namespace AgendaAle.Application.Commands.Auth;
public class LoginUserCommand : IRequest<string>
{
    public string GoogleToken { get; set; }
    
}