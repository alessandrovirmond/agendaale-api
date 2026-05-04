using AgendaAle.Domain.Entities;

namespace AgendaAle.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}