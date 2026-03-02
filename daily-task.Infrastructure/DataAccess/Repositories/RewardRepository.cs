using daily_task.Domain.Repositories.Reward;
using Microsoft.EntityFrameworkCore;
using Entities = daily_task.Domain.Entities;

namespace daily_task.Infrastructure.DataAccess.Repositories
{
    public class RewardRepository : IRewardWriteOnlyRepository, IRewardReadOnlyRepository, IRewardUpdateOnlyRepository
    {
        private readonly DailyTaskDbContext _dbContext;

        public RewardRepository(DailyTaskDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Entities.Reward reward) => await _dbContext.Rewards.AddAsync(reward);

        async Task<Entities.Reward?> IRewardReadOnlyRepository.GetById(long rewardId)
        {
            return await _dbContext
                .Rewards
                .AsNoTracking()
                .FirstOrDefaultAsync(reward => reward.Active && reward.Id == rewardId);
        }

        async Task<IList<Entities.Reward>> IRewardReadOnlyRepository.GetAllRewardsActive()
        {
            return await _dbContext
                .Rewards
                .AsNoTracking()
                .Where(reward => reward.Active)
                .ToListAsync();
        }

        async Task<Entities.Reward?> IRewardUpdateOnlyRepository.GetById(long rewardId)
        {
            return await _dbContext
                .Rewards
                .FirstOrDefaultAsync(reward => reward.Active && reward.Id == rewardId);
        }

        public void Update(Entities.Reward reward) => _dbContext.Rewards.Update(reward);
    }
}