using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class DeleteAppointmentCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}