using AgendaAle.Application.Services;
using MediatR;

namespace AgendaAle.Application.Events;

public class PublishToQueueOnAppointmentCreatedHandler : INotificationHandler<AppointmentCreatedNotification>
{
    private readonly IMessageBus _messageBus;

    public PublishToQueueOnAppointmentCreatedHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(AppointmentCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine($"[RABBITMQ] Preparando para enviar e-mail para: {notification.UserEmail}");
        Console.WriteLine("==================================================");
        await _messageBus.PublishAsync("emails_queue", notification);
    }
}