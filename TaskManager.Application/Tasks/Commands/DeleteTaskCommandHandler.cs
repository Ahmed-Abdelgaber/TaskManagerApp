using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeleteTaskCommandHandler(IAppDbContext context, ICurrentUserService currentUser)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.Role != nameof(UserRole.Senior))
                throw new UnauthorizedAccessException("Only Senior users can delete tasks.");

            if (request.Id == null)
                return false;

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
                return false;

            if (task.CreatedByUserId != _currentUser.UserId)
                throw new UnauthorizedAccessException("You can only delete tasks you created.");

            task.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}