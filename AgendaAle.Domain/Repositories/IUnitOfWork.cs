namespace AgendaAle.Domain.Interfaces; // Ou .Persistence

public interface IUnitOfWork
{    Task<bool> CommitAsync();
}