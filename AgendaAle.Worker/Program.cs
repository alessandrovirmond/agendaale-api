using AgendaAle.Worker;
using AgendaAle.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
