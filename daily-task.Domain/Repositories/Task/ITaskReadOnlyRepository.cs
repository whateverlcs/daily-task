namespace daily_task.Domain.Repositories.Task
{
    public interface ITaskReadOnlyRepository
    {
        Task<Entities.Task?> GetById(long taskId);

        Task<IList<Entities.Task>> GetAllTasksActive();
    }
}