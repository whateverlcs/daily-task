using daily_task.Domain.Repositories.Task;
using Microsoft.EntityFrameworkCore;
using Entities = daily_task.Domain.Entities;

namespace daily_task.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : ITaskWriteOnlyRepository, ITaskReadOnlyRepository, ITaskUpdateOnlyRepository
    {
        private readonly DailyTaskDbContext _dbContext;

        public TaskRepository(DailyTaskDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Entities.Task task) => await _dbContext.Tasks.AddAsync(task);

        async Task<Entities.Task?> ITaskReadOnlyRepository.GetById(long taskId)
        {
            return await _dbContext
                .Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(task => task.Active && task.Id == taskId);
        }

        async Task<IList<Entities.Task>> ITaskReadOnlyRepository.GetAllTasksActive()
        {
            return await _dbContext
                .Tasks
                .AsNoTracking()
                .Where(task => task.Active)
                .ToListAsync();
        }

        async Task<Entities.Task?> ITaskUpdateOnlyRepository.GetById(long taskId)
        {
            return await _dbContext
                .Tasks
                .FirstOrDefaultAsync(task => task.Active && task.Id == taskId);
        }

        public void Update(Entities.Task task) => _dbContext.Tasks.Update(task);
    }
}