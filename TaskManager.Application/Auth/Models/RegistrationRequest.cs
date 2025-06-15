using TaskManager.Domain.Enums;

namespace TaskManager.Application.Auth.Models
{
    public class RegistrationRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Junior; // Default role is Junior
        public Guid? ManagerId { get; set; } // Nullable to allow users not assigned to a team
    }
}