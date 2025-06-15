using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tasks.Commands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
                return false;

            if (request.Description is not null)
                task.Description = request.Description;

            if (request.DueDate.HasValue)
                task.DueDate = request.DueDate;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
