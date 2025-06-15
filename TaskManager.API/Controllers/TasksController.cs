using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskManager.Application.Tasks.Commands;
using TaskManager.Application.Tasks.Queries;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TasksController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }

            var id = await _mediator.Send(command, cancellationToken);
            return Ok(new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var tasks = await _mediator.Send(new GetTasksQuery(), cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var task = await _mediator.Send(new GetTaskByIdQuery(id), cancellationToken);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost("update")]
        public async Task<IActionResult> PostUpdate([FromBody] UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result ? NoContent() : NotFound();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> PostDelete([FromBody] DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result ? NoContent() : NotFound();
        }
    }
}