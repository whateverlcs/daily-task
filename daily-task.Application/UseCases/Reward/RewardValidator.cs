using daily_task.Application.Models;
using daily_task.Exceptions;
using FluentValidation;

namespace daily_task.Application.UseCases.Reward
{
    public class RewardValidator : AbstractValidator<RewardDisplayModel>
    {
        public RewardValidator()
        {
            RuleFor(reward => reward.Name).NotEmpty().WithMessage(ResourceMessagesException.REWARD_NAME_EMPTY);
            RuleFor(reward => reward.Gold).NotEmpty().WithMessage(ResourceMessagesException.REWARD_GOLD_EMPTY);
        }
    }
}