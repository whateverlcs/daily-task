using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Extensions;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Task;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Task.Register
{
    public class RegisterTaskUseCase : IRegisterTaskUseCase
    {
        private readonly ITaskWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterTaskUseCase(
            ITaskWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Execute(NewTask request)
        {
            Validate(request);

            var task = _mapper.Map<Domain.Entities.Task>(request);

            await _repository.Add(task);

            await _unitOfWork.Commit();

            return true;
        }

        private static void Validate(NewTask request)
        {
            var result = new TaskValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}