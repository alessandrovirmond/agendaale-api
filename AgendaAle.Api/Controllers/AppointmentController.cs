using AgendaAle.Application.Commands.Appointments;
using AgendaAle.Application.Queries.Appointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAle.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
    {
        var appointmentId = await _mediator.Send(command);
        
        return Created("", new { AppointmentId = appointmentId }); 
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var query = new GetAppointmentsByUserQuery { UserId = userId };
        
        var appointments = await _mediator.Send(query);
        
        return Ok(appointments);
    }
}