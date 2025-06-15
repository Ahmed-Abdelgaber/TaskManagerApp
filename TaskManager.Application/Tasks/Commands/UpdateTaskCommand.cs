using MediatR;
using System.Text.Json.Serialization;

namespace TaskManager.Application.Tasks.Commands
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DueDate { get; set; }
    }
}
