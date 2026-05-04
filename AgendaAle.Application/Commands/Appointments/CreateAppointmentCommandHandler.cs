using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository;

    public CreateAppointmentCommandHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment(request.Title, request.Description, request.Date, request.UserId);

        _repository.Add(appointment);
        await _repository.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}