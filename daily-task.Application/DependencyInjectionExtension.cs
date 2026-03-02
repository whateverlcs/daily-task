using daily_task.Application.Services.AutoMapper;
using daily_task.Application.UseCases.Profile.GetProfile;
using daily_task.Application.UseCases.Profile.Register;
using daily_task.Application.UseCases.Profile.Update;
using daily_task.Application.UseCases.Rank.GetAllRanks;
using daily_task.Application.UseCases.Reward.GetAllRewards;
using daily_task.Application.UseCases.Task.GetAllTasks;
using daily_task.Application.UseCases.Task.GetById;
using daily_task.Application.UseCases.Task.Register;
using daily_task.Application.UseCases.Task.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
            {
                autoMapperOptions.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterTaskUseCase, RegisterTaskUseCase>();
            services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
            services.AddScoped<IGetAllTasksUseCase, GetAllTasksUseCase>();
            services.AddScoped<IGetTaskByIdUseCase, GetTaskByIdUseCase>();
            services.AddScoped<IRegisterProfileUseCase, RegisterProfileUseCase>();
            services.AddScoped<IUpdateProfileUseCase, UpdateProfileUseCase>();
            services.AddScoped<IGetAllRanksUseCase, GetAllRanksUseCase>();
            services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
            services.AddScoped<IGetAllRewardsUseCase, GetAllRewardsUseCase>();
        }
    }
}
