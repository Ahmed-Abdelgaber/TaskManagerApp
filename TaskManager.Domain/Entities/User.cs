using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Junior;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Guid? ManagerId { get; set; } // Nullable to allow users not assigned to a team
        public User? Manager { get; set; }
        public ICollection<User>? Juniors { get; set; }
        public ICollection<TaskItem>? CreatedTasks { get; set; }
        public ICollection<TaskItem>? AssignedTasks { get; set; }
    }
}