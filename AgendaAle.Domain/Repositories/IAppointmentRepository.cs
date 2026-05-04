using AgendaAle.Domain.Entities;

namespace AgendaAle.Domain.Repositories;

public interface IAppointmentRepository
{
    void Add(Appointment appointment);
    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<List<Appointment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Update(Appointment appointment);
    void Delete(Appointment appointment);
}