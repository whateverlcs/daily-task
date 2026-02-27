namespace daily_task.Domain.Repositories.Task
{
    public interface ITaskWriteOnlyRepository
    {
        System.Threading.Tasks.Task Add(Entities.Task task);
    }
}