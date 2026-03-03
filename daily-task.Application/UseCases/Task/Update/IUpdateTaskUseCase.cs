using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Task.Update
{
    public interface IUpdateTaskUseCase
    {
        System.Threading.Tasks.Task<bool> Execute(long taskId, NewTask request);
    }
}