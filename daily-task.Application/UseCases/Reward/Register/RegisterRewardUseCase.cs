using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Extensions;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Reward;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Reward.Register
{
    public class RegisterRewardUseCase : IRegisterRewardUseCase
    {
        private readonly IRewardWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterRewardUseCase(
            IRewardWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Execute(RewardDisplayModel request)
        {
            Validate(request);

            var reward = _mapper.Map<Domain.Entities.Reward>(request);

            await _repository.Add(reward);

            await _unitOfWork.Commit();

            return true;
        }

        private static void Validate(RewardDisplayModel request)
        {
            var result = new RewardValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}