using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Task.GetById
{
    public interface IGetTaskByIdUseCase
    {
        System.Threading.Tasks.Task<TaskDisplayModel> Execute(long taskId);
    }
}