using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Tasks.Models;

namespace TaskManager.Application.Tasks.Queries
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly IAppDbContext _context;

        public GetTasksQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    IsDeleted = t.IsDeleted
                })
                .ToListAsync(cancellationToken);
        }
    }
}
