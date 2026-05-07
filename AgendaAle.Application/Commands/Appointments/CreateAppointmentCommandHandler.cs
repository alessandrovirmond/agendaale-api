using AgendaAle.Domain.Entities;
using AgendaAle.Domain.Repositories;
using MediatR;
using AgendaAle.Application.Events;

namespace AgendaAle.Application.Commands.Appointments;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _repository;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository repository,
        IMediator mediator,
        IUserRepository userRepository)
    {
        _repository = repository;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment(request.Title, request.Description, request.Date, request.UserId);

        _repository.Add(appointment);
        await _repository.SaveChangesAsync(cancellationToken);


        var notification = new AppointmentCreatedNotification(
            appointment.Id,
            appointment.Title,
            appointment.Date,
            request.UserEmail);

        await _mediator.Publish(notification, cancellationToken);

        return appointment.Id;
    }

}