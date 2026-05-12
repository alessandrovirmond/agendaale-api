using AgendaAle.Application.Services;
using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using MediatR;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using AgendaAle.Domain.Interfaces; // <-- Adicione este using se não tiver

namespace AgendaAle.Application.Commands.Auth;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    private readonly IConfiguration _configuration;
    
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var clientId = _configuration["Authentication:Google:ClientId"];
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { clientId }
        };

        GoogleJsonWebSignature.Payload googlePayload;

        try
        {
            googlePayload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken, settings);
        }
        catch (InvalidJwtException)
        {
            throw new UnauthorizedAccessException("Token do Google inválido.");
        }

        string externalId = googlePayload.Subject;
        string email = googlePayload.Email;
        string name = googlePayload.Name;


        var user = await _userRepository.GetByExternalIdAsync(externalId);

       if (user == null)
{

    user = new User(email, name, externalId); 

    await _userRepository.AddAsync(user);
    await _unitOfWork.CommitAsync(); 
}

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}