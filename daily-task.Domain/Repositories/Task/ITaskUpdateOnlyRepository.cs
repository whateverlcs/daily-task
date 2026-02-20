namespace daily_task.Domain.Repositories.Task
{
    public interface ITaskUpdateOnlyRepository
    {
        public Task<Entities.Task?> GetById(long id);

        public void Update(Entities.Task task);
    }
}