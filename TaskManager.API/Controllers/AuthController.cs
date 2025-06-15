using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskManager.Application.Auth.Commands;

namespace TaskManager.API.Controllers
{
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
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            if (command == null) return BadRequest();

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationCommand command, CancellationToken cancellationToken)
        {
            if (command == null) return BadRequest();

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
