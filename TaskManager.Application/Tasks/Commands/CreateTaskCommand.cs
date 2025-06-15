using MediatR;
using TaskManager.Application.Tasks.Models;

namespace TaskManager.Application.Tasks.Commands
{
    public class CreateTaskCommand : BaseTaskModel, IRequest<Guid>
    {
    }
}