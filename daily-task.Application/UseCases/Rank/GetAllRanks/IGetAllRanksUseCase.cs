using daily_task.Application.Models;

namespace daily_task.Application.UseCases.Rank.GetAllRanks
{
    public interface IGetAllRanksUseCase
    {
        Task<IList<RankDisplayModel>> Execute();
    }
}