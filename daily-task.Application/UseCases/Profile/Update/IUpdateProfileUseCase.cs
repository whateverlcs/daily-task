using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Profile.Update
{
    public interface IUpdateProfileUseCase
    {
        System.Threading.Tasks.Task<bool> Execute(ProfileDisplayModel request);
    }
}