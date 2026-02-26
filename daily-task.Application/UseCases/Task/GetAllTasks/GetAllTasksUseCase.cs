using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories.Task;

namespace daily_task.Application.UseCases.Task.GetAllTasks
{
    public class GetAllTasksUseCase : IGetAllTasksUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITaskReadOnlyRepository _repository;

        public GetAllTasksUseCase(
            IMapper mapper,
            ITaskReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<TaskDisplayModel>> Execute()
        {
            var tasks = await _repository.GetAllTasksActive();

            var response = _mapper.Map<IList<TaskDisplayModel>>(tasks.OrderByDescending(x => x.CreatedOn));

            return response;
        }
    }
}