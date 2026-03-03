using daily_task.Domain.Enums;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Profile;
using daily_task.Domain.Repositories.Rank;
using daily_task.Domain.Repositories.Reward;
using daily_task.Domain.Repositories.Task;
using daily_task.Infrastructure.DataAccess;
using daily_task.Infrastructure.DataAccess.Repositories;
using daily_task.Infrastructure.Extensions;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace daily_task.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);

            if (configuration.IsUnitTestEnviroment())
                return;

            var databaseType = configuration.DatabaseType();

            if (databaseType == DatabaseType.SqlServer)
            {
                AddDbContext_SqlServer(services, configuration);
                AddFluentMigrator_SqlServer(services, configuration);
            }
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnetionString();

            services.AddDbContext<DailyTaskDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITaskWriteOnlyRepository, TaskRepository>();
            services.AddScoped<ITaskReadOnlyRepository, TaskRepository>();
            services.AddScoped<ITaskUpdateOnlyRepository, TaskRepository>();
            services.AddScoped<IRankReadOnlyRepository, RankRepository>();
            services.AddScoped<IProfileWriteOnlyRepository, ProfileRepository>();
            services.AddScoped<IProfileReadOnlyRepository, ProfileRepository>();
            services.AddScoped<IProfileUpdateOnlyRepository, ProfileRepository>();
            services.AddScoped<IRewardWriteOnlyRepository, RewardRepository>();
            services.AddScoped<IRewardReadOnlyRepository, RewardRepository>();
            services.AddScoped<IRewardUpdateOnlyRepository, RewardRepository>();
        }

        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnetionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("daily-task.Infrastructure")).For.All();
            });
        }
    }
}