using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Profile.Register
{
    public interface IRegisterProfileUseCase
    {
        public Task<bool> Execute(ProfileDisplayModel request);
    }
}