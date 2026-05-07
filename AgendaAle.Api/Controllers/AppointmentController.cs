using AgendaAle.Application.Commands.Appointments;
using AgendaAle.Application.Queries.Appointments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace AgendaAle.Api.Controllers;

[Authorize]
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
[Authorize]
public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var userEmailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

    if (Guid.TryParse(userIdClaim, out var userId))
    {
        command.UserId = userId;
    }
    
    command.UserEmail = userEmailClaim ?? string.Empty;

    if (string.IsNullOrEmpty(command.UserEmail))
    {
        return BadRequest("O token do usuário não contém um e-mail válido.");
    }

    var id = await _mediator.Send(command);

    return Ok(id);
}

    [HttpGet("my-appointments")]
    public async Task<IActionResult> GetMyAppointments()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var query = new GetAppointmentsByUserQuery { UserId = Guid.Parse(userIdClaim) };

        var appointments = await _mediator.Send(query);

        return Ok(appointments);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentCommand command)
    {
        command.Id = id;

        var success = await _mediator.Send(command);

        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteAppointmentCommand { Id = id };
        var success = await _mediator.Send(command);

        if (!success) return NotFound();

        return NoContent();
    }
}