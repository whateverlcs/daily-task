using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = daily_task.Domain.Entities;

namespace daily_task.Infrastructure.DataAccess
{
    public class DailyTaskDbContext : DbContext
    {
        public DailyTaskDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Entities.Rank> Ranks { get; set; }
        public DbSet<Entities.Profile> Profile { get; set; }
        public DbSet<Entities.Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DailyTaskDbContext).Assembly);
        }
    }
}
