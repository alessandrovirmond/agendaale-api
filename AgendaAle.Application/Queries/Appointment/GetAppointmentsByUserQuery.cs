using MediatR;

namespace AgendaAle.Application.Queries.Appointments;

public class GetAppointmentsByUserQuery : IRequest<List<AppointmentViewModel>>
{
    public Guid UserId { get; set; }
}