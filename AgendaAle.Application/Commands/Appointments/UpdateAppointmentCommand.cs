using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class UpdateAppointmentCommand : IRequest<bool> 
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}