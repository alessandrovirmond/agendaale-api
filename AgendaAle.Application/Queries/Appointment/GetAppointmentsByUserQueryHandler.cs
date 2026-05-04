using AgendaAle.Domain.Repositories;
using MediatR;

namespace AgendaAle.Application.Queries.Appointments;

public class GetAppointmentsByUserQueryHandler : IRequestHandler<GetAppointmentsByUserQuery, List<AppointmentViewModel>>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentsByUserQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AppointmentViewModel>> Handle(GetAppointmentsByUserQuery request, CancellationToken cancellationToken)
    {
        // 1. Busca no banco de dados as entidades puras
        var appointments = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);

        // 2. Transforma as entidades no modelo de visualização (ViewModel)
        var viewModels = appointments.Select(a => new AppointmentViewModel
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            Date = a.Date
        }).ToList();

        return viewModels;
    }
}