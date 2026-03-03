namespace daily_task.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public System.Threading.Tasks.Task Commit();
    }
}