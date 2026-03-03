using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Extensions;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Reward;
using daily_task.Exceptions;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Reward.Update
{
    public class UpdateRewardUseCase : IUpdateRewardUseCase
    {
        private readonly IRewardUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRewardUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRewardUpdateOnlyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task<bool> Execute(long rewardId, RewardDisplayModel request)
        {
            Validate(request);

            var reward = await _repository.GetById(rewardId);

            if (reward is null)
                throw new NotFoundException(ResourceMessagesException.REWARD_NOT_FOUND);

            _mapper.Map(request, reward);

            _repository.Update(reward);

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