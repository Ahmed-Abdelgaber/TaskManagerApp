using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Tasks.Models;

namespace TaskManager.Application.Tasks.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
    {
        private readonly IAppDbContext _context;

        public GetTaskByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Where(t => t.Id == request.Id)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
