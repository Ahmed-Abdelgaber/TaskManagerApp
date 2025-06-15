namespace TaskManager.Domain.Entities
{
    public class TaskProgress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Comments { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;
    }
}