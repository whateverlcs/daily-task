namespace daily_task.Application.UseCases.Task.Delete
{
    public interface IDeleteTaskUseCase
    {
        System.Threading.Tasks.Task Execute(long taskId);
    }
}