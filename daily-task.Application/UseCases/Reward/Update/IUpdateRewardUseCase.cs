using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Reward.Update
{
    public interface IUpdateRewardUseCase
    {
        System.Threading.Tasks.Task<bool> Execute(long rewardId, RewardDisplayModel request);
    }
}