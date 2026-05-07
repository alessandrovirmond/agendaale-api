using System.Text;
using System.Text.Json;
using AgendaAle.Worker.Models;
using AgendaAle.Worker.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AgendaAle.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private IConnection? _connection;
    private IChannel? _channel;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "admin", Password = "admin" };
        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(queue: "emails_queue", durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);
        
        _logger.LogInformation("Worker conectado e aguardando tarefas na fila...");
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_channel == null) return;

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(body);

            var agendamento = JsonSerializer.Deserialize<AppointmentCreatedEvent>(messageString);

            if (agendamento != null)
            {
                _logger.LogInformation($"[PROCESSANDO] Preparando e-mail para: {agendamento.UserEmail}");

                using var scope = _serviceProvider.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var assunto = "Nova Tarefa Criada: " + agendamento.Title;
                var corpo = $@"
                    <h2>Olá!</h2>
                    <p>Uma nova tarefa foi agendada no sistema.</p>
                    <p><strong>Título:</strong> {agendamento.Title}</p>
                    <p><strong>Data:</strong> {agendamento.Date:dd/MM/yyyy HH:mm}</p>
                    <br>
                    <p>Abraços,<br>Equipe AgendaAle</p>";

                try
                {
                    await emailService.SendEmailAsync(agendamento.UserEmail, assunto, corpo);
                    _logger.LogInformation($"[SUCESSO] E-mail enviado com sucesso para {agendamento.UserEmail}!");
                    
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[ERRO] Falha ao enviar o e-mail: {ex.Message}");
                    await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
                }
            }
        };

        await _channel.BasicConsumeAsync(queue: "emails_queue", autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) await _channel.CloseAsync(cancellationToken);
        if (_connection != null) await _connection.CloseAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}