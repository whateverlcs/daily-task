namespace daily_task.Domain.Repositories.Rank
{
    public interface IRankReadOnlyRepository
    {
        Task<IList<Entities.Rank>> GetAllRanks();
    }
}