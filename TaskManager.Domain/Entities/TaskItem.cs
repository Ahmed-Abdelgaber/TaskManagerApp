using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public Guid AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; } = null!;
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public TaskState Status { get; set; } = TaskState.NotAssigned;

    }
}


