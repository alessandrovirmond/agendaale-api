using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaAle.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByExternalIdAsync(string externalId)
{
    return await _context.Users
        .FirstOrDefaultAsync(u => u.ExternalAuthId == externalId);
}

public async Task AddAsync(User user)
{
    await _context.Users.AddAsync(user);
}


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}