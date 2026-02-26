namespace daily_task.Application.UseCases.Task.Delete
{
    public interface IDeleteTaskUseCase
    {
        System.Threading.Tasks.Task<bool> Execute(long taskId);
    }
}