using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Tasks.Models;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Queries
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetTasksQueryHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            }
        }
        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var role = _currentUserService.Role;
            var userId = _currentUserService.UserId;

            var query = _context.Tasks
              .Include(t => t.AssignedToUser)
              .AsNoTracking();

            if (role == nameof(UserRole.Junior))
            {
                query = query.Where(t => t.AssignedToUserId == userId);
            }
            else if (role == nameof(UserRole.Senior))
            {
                query = query.Where(t => t.AssignedToUser.ManagerId == userId);
            }
            return await query
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    AssignedToUsername = t.AssignedToUser.Username
                })
                .ToListAsync(cancellationToken);
        }
    }
}
