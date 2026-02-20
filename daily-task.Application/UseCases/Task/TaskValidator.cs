using daily_task.Application.Models;
using daily_task.Exceptions;
using FluentValidation;

namespace daily_task.Application.UseCases.Task
{
    public class TaskValidator : AbstractValidator<NewTask>
    {
        public TaskValidator()
        {
            RuleFor(task => task.Name).NotEmpty().WithMessage(ResourceMessagesException.TASK_NAME_EMPTY);
            RuleFor(task => task.Priority).IsInEnum().WithMessage(ResourceMessagesException.TASK_PRIORITY_NOT_SUPPORTED);
            RuleFor(task => task.Description).NotEmpty().WithMessage(ResourceMessagesException.TASK_DESCRIPTION_EMPTY);
        }
    }
}