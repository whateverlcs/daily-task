using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Reward.GetAllRewards
{
    public interface IGetAllRewardsUseCase
    {
        Task<IList<RewardDisplayModel>> Execute();
    }
}