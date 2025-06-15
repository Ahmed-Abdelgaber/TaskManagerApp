using MediatR;
using TaskManager.Application.Tasks.Models;

namespace TaskManager.Application.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskDto?>
    {
        public Guid Id { get; set; }

        public GetTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
