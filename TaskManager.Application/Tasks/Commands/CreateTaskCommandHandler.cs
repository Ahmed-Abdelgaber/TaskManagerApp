using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Enums;


namespace TaskManager.Application.Tasks.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateTaskCommandHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != nameof(UserRole.Senior))
                throw new UnauthorizedAccessException("Only seniors can create tasks.");

            var assigned = await _context.Users
                            .FirstOrDefaultAsync(u => u.Id == request.AssignedToUserId, cancellationToken);

            if (assigned == null || assigned.ManagerId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You can assign only to your juniors.");

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                CreatedByUserId = _currentUserService.UserId,
                AssignedToUserId = request.AssignedToUserId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }

}