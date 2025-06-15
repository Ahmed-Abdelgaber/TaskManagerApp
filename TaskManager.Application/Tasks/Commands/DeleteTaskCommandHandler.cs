using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tasks.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
                return false;

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
                return false;

            task.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}