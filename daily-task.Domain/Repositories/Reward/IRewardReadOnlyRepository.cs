namespace daily_task.Domain.Repositories.Reward
{
    public interface IRewardReadOnlyRepository
    {
        Task<Entities.Reward?> GetById(long rewardId);

        Task<IList<Entities.Reward>> GetAllRewardsActive();
    }
}