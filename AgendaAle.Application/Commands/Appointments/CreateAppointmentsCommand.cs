using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class CreateAppointmentCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; } 

    public string UserEmail { get; set; } = string.Empty;
    
}