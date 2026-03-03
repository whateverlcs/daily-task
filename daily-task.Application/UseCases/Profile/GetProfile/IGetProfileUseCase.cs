using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Profile.GetProfile
{
    public interface IGetProfileUseCase
    {
        System.Threading.Tasks.Task<ProfileDisplayModel> Execute();
    }
}