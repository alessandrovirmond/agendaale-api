using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class CreateAppointmentCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    
    // Obrigatório: De quem é essa tarefa?
    public Guid UserId { get; set; } 
}