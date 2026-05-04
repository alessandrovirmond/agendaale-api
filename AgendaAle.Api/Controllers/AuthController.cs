using AgendaAle.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAle.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var userId = await _mediator.Send(command);
        
        return Ok(new { UserId = userId }); 
    }
}