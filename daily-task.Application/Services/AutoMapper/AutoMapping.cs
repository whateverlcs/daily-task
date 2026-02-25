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
            CreateMap<Domain.Entities.Task, TaskDisplayModel>()
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src =>
                    src.Priority.ToString().Replace("_", " ")))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src =>
                    $"Criado em {src.CreatedOn:dd/MM}"));
        }
    }
}