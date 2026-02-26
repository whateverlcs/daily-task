using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories.Task;
using daily_task.Exceptions;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Task.GetById
{
    public class GetTaskByIdUseCase : IGetTaskByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITaskReadOnlyRepository _repository;

        public GetTaskByIdUseCase(
            IMapper mapper,
            ITaskReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TaskDisplayModel> Execute(long taskId)
        {
            var task = await _repository.GetById(taskId);

            if (task is null)
                throw new NotFoundException(ResourceMessagesException.TASK_NOT_FOUND);

            var response = _mapper.Map<TaskDisplayModel>(task);

            return response;
        }
    }
}