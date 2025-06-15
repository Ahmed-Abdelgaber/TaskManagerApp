using TaskManager.Domain.Entities;
namespace TaskManager.Application.Tasks.Models
{
    public class TaskDto : BaseTaskModel
    {
        public Guid Id { get; set; }
        public string AssignedToUsername { get; set; } = string.Empty;
    }
}
