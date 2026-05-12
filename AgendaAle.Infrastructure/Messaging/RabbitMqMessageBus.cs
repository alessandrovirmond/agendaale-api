using System.Text;
using System.Text.Json;
using AgendaAle.Application.Services;
using RabbitMQ.Client;

namespace AgendaAle.Infrastructure.Messaging;

public class RabbitMqMessageBus : IMessageBus
{
    public async Task PublishAsync(string queue, object message)
    {
        var factory = new ConnectionFactory() 
        { 
            HostName = "localhost",
            UserName = "admin",
            Password = "admin"
        };
        
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queue,
                                        durable: true, 
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: string.Empty,
                                        routingKey: queue,
                                        body: body);
    }
}