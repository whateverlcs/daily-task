namespace daily_task.Domain.Repositories.Profile
{
    public interface IProfileReadOnlyRepository
    {
        Task<Entities.Profile?> GetProfile();
    }
}