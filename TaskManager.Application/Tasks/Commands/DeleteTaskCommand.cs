using MediatR;
using System.Text.Json.Serialization;

namespace TaskManager.Application.Tasks.Commands
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}