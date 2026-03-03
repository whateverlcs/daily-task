using daily_task.Domain.Repositories.Rank;
using Microsoft.EntityFrameworkCore;
using Entities = daily_task.Domain.Entities;

namespace daily_task.Infrastructure.DataAccess.Repositories
{
    public class RankRepository : IRankReadOnlyRepository
    {
        private readonly DailyTaskDbContext _dbContext;

        public RankRepository(DailyTaskDbContext dbContext) => _dbContext = dbContext;

        async Task<IList<Entities.Rank>> IRankReadOnlyRepository.GetAllRanks()
        {
            return await _dbContext
                .Ranks
                .AsNoTracking()
                .Where(rank => rank.Active)
                .ToListAsync();
        }
    }
}