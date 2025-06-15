using MediatR;
using TaskManager.Application.Auth.Models;
using TaskManager.Domain.Enums;


namespace TaskManager.Application.Auth.Commands
{
    public class RegistrationCommand : IRequest<RegistrationResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Junior; // Default role is Junior
        public Guid? ManagerId { get; set; } // Nullable to allow users not assigned to a team
    }
}
