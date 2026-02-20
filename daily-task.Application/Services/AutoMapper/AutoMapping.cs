using AutoMapper;
using daily_task.Application.Models;

namespace daily_task.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<NewTask, Domain.Entities.Task>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.Task, TaskDisplayModel>();
        }
    }
}