using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Extensions;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Task;
using daily_task.Exceptions;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Task.Update
{
    public class UpdateTaskUseCase : IUpdateTaskUseCase
    {
        private readonly ITaskUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITaskUpdateOnlyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Execute(long taskId, NewTask request)
        {
            Validate(request);

            var task = await _repository.GetById(taskId);

            if (task is null)
                throw new NotFoundException(ResourceMessagesException.TASK_NOT_FOUND);

            _mapper.Map(request, task);

            _repository.Update(task);

            await _unitOfWork.Commit();
        }

        private static void Validate(NewTask request)
        {
            var result = new TaskValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}