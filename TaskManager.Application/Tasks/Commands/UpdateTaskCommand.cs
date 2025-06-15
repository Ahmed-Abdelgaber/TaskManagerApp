using MediatR;
using System.Text.Json.Serialization;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DueDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsCompleted { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? AssignedToUserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TaskState? Status { get; set; }
    }

}
