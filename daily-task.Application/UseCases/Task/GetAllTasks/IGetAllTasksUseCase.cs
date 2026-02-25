using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Task.GetAllTasks
{
    public interface IGetAllTasksUseCase
    {
        Task<IList<TaskDisplayModel>> Execute();
    }
}