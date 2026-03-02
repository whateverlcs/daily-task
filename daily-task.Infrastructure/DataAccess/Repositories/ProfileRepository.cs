using daily_task.Domain.Repositories.Profile;
using Microsoft.EntityFrameworkCore;
using Entities = daily_task.Domain.Entities;

namespace daily_task.Infrastructure.DataAccess.Repositories
{
    public class ProfileRepository : IProfileWriteOnlyRepository, IProfileReadOnlyRepository, IProfileUpdateOnlyRepository
    {
        private readonly DailyTaskDbContext _dbContext;

        public ProfileRepository(DailyTaskDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Entities.Profile profile) => await _dbContext.Profile.AddAsync(profile);

        async Task<Entities.Profile?> IProfileReadOnlyRepository.GetProfile()
        {
            return await _dbContext
                .Profile
                .AsNoTracking()
                .FirstOrDefaultAsync(profile => profile.Active);
        }

        async Task<Entities.Profile?> IProfileUpdateOnlyRepository.GetById(long profileId)
        {
            return await _dbContext
                .Profile
                .FirstOrDefaultAsync(profile => profile.Active && profile.Id == profileId);
        }

        public void Update(Entities.Profile profile) => _dbContext.Profile.Update(profile);
    }
}