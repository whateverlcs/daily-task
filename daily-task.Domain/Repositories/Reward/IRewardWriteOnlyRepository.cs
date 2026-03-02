namespace daily_task.Domain.Repositories.Reward
{
    public interface IRewardWriteOnlyRepository
    {
        System.Threading.Tasks.Task Add(Entities.Reward reward);
    }
}