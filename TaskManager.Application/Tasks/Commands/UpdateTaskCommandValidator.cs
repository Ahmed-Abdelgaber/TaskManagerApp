using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tasks.Commands
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateTaskCommandValidator(IAppDbContext context)
        {
            _context = context;

            // Only validate DueDate if it's being updated
            // RuleFor(x => x)
            //     .MustAsync(DueDateAfterCreatedAt)
            //     .WithMessage("Due date cannot be earlier than the created date.");
        }

        private async Task<bool> DueDateAfterCreatedAt(UpdateTaskCommand command, CancellationToken token)
        {
            Console.WriteLine("Hello From Validator ✌️🖐️");
            // Don't validate if no due date is provided
            if (!command.DueDate.HasValue)
                return true;

            var task = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == command.Id, token);

            if (task == null)
                return false; // Let handler return NotFound
            Console.WriteLine($"‼️${command.DueDate} : ${task.CreatedAt} ======> ${command.DueDate >= task.CreatedAt}");
            return command.DueDate >= task.CreatedAt;
        }
    }
}
