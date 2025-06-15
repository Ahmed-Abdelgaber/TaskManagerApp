namespace TaskManager.Application.Tasks.Commands
{
    using FluentValidation;

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
