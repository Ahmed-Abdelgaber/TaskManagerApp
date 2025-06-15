using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Tasks.Models;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetTaskByIdQueryHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var role = _currentUserService.Role;
            var userId = _currentUserService.UserId;

            var task = await _context.Tasks.Include(t => t.AssignedToUser).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
            {
                return null;
            }

            if (role == nameof(UserRole.Junior) && task.AssignedToUserId != userId)
                throw new UnauthorizedAccessException("You can only access the assigned tasks.");
            if (role == nameof(UserRole.Senior) && task.AssignedToUser.ManagerId != userId)
                throw new UnauthorizedAccessException("You can only access tasks assigned to your team.");

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                AssignedToUsername = task.AssignedToUser.Username,
                Status = task.Status
            };
        }
    }
}
