using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Task.Update
{
    public interface IUpdateTaskUseCase
    {
        System.Threading.Tasks.Task Execute(long taskId, NewTask request);
    }
}