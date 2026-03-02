namespace daily_task.Domain.Repositories.Profile
{
    public interface IProfileWriteOnlyRepository
    {
        System.Threading.Tasks.Task Add(Entities.Profile profile);
    }
}