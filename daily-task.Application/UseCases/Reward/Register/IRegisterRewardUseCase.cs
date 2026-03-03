using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Reward.Register
{
    public interface IRegisterRewardUseCase
    {
        public Task<bool> Execute(RewardDisplayModel request);
    }
}