namespace daily_task.Domain.Repositories.Reward
{
    public interface IRewardUpdateOnlyRepository
    {
        public Task<Entities.Reward?> GetById(long id);

        public void Update(Entities.Reward reward);
    }
}