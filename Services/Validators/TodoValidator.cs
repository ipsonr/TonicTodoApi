using FluentValidation;
using TonicTodoApi.Models;

namespace TonicTodoApi.Services.Validators
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(todo => todo.Name)
                .NotEmpty()
                .WithMessage("Name cannot be null")
                .MaximumLength(50)
                .WithMessage("Name cannot be greater than 50 characters")
                .Must(x => x.Contains("cat") == false)
                .WithMessage("no cats allowed");

            RuleFor(todo => todo.IsComplete)
                .Must(isComplete => isComplete == true || isComplete == false)
                .NotNull()
                .WithMessage("IsComplete must be True or False");

        }
    }
}
