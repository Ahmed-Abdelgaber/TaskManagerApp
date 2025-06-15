using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Models
{
    public abstract class BaseTaskModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public Guid AssignedToUserId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public TaskState Status { get; set; }
    }
}
