namespace AgendaAle.Worker.Models;

public record AppointmentCreatedEvent(
    Guid AppointmentId, 
    string Title, 
    DateTime Date, 
    string UserEmail);