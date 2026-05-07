using AgendaAle.Domain.Repositories;
using AgendaAle.Infrastructure.Persistence;
using AgendaAle.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgendaAle.Application.Services;
using AgendaAle.Infrastructure.Auth;
using AgendaAle.Infrastructure.Messaging;


namespace AgendaAle.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IMessageBus, RabbitMqMessageBus>();
        return services;
    }
}