using AgendaAle.Domain.Repositories;
using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public UpdateAppointmentCommandHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (appointment == null) return false;

        appointment.Update(request.Title, request.Description, request.Date);

        _repository.Update(appointment);
        await _repository.SaveChangesAsync(cancellationToken);

        return true; 
    }
}