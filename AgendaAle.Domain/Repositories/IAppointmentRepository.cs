using AgendaAle.Domain.Entities;

namespace AgendaAle.Domain.Repositories;

public interface IAppointmentRepository
{
    void Add(Appointment appointment);
    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<List<Appointment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}