using MediatR;

namespace AgendaAle.Application.Events;

public class AppointmentCreatedNotification : INotification
{
    public Guid AppointmentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string UserEmail { get; set; } = string.Empty;

    public AppointmentCreatedNotification(Guid appointmentId, string title, DateTime date, string userEmail)
    {
        AppointmentId = appointmentId;
        Title = title;
        Date = date;
        UserEmail = userEmail;
    }
}