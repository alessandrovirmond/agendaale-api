using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaAle.Infrastructure.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Appointment>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.Date)
            .ToListAsync(cancellationToken);
    }
}