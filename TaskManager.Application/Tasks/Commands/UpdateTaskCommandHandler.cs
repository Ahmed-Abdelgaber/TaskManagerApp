using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskCommandHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != nameof(UserRole.Senior))
                throw new UnauthorizedAccessException("Only Senior users can update tasks.");

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
                return false;

            if (task.CreatedByUserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You can only update tasks you created.");

            if (request.Description is not null)
                task.Description = request.Description;

            if (request.DueDate.HasValue)
                task.DueDate = request.DueDate;

            if (request.IsCompleted.HasValue)
                task.IsCompleted = request.IsCompleted.Value;

            if (request.Status.HasValue)
            {
                if (request.Status.Value == TaskState.NotAssigned && task.AssignedToUserId != Guid.Empty)
                    throw new InvalidOperationException("Cannot set status to NotAssigned while the task is assigned to a user.");

                task.Status = request.Status.Value;
            }

            if (request.AssignedToUserId.HasValue)
            {
                var assignedUser = await _context.Users.FirstOrDefaultAsync(t => t.Id == request.AssignedToUserId.Value && t.ManagerId == _currentUserService.UserId, cancellationToken);
                if (assignedUser == null)
                    throw new ArgumentException("Unauthorized assign operation.");

                task.AssignedToUserId = assignedUser.Id;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
