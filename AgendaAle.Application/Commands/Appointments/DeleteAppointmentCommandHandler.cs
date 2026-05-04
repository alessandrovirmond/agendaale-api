using AgendaAle.Domain.Repositories;
using MediatR;

namespace AgendaAle.Application.Commands.Appointments;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public DeleteAppointmentCommandHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (appointment == null) return false;

        _repository.Delete(appointment);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}