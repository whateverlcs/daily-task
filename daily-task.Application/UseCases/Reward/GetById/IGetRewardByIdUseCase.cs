using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Reward.GetById
{
    public interface IGetRewardByIdUseCase
    {
        System.Threading.Tasks.Task<RewardDisplayModel> Execute(long rewardId);
    }
}