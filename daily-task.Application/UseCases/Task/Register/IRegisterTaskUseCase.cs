using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Task.Register
{
    public interface IRegisterTaskUseCase
    {
        public Task<bool> Execute(NewTask request);
    }
}