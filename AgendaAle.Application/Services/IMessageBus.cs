namespace AgendaAle.Application.Services;

public interface IMessageBus
{
    Task PublishAsync(string queue, object message); 
}