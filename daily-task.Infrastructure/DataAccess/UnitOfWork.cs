using daily_task.Domain.Repositories;

namespace daily_task.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DailyTaskDbContext _dbContext;

        public UnitOfWork(DailyTaskDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}