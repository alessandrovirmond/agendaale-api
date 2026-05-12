using AgendaAle.Domain.Entities;

namespace AgendaAle.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<User?> GetByExternalIdAsync(string externalId);

    Task AddAsync(User user);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}