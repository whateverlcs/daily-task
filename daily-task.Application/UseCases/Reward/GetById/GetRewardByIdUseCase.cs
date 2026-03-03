using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories.Reward;
using daily_task.Exceptions;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Reward.GetById
{
    public class GetRewardByIdUseCase : IGetRewardByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IRewardReadOnlyRepository _repository;

        public GetRewardByIdUseCase(
            IMapper mapper,
            IRewardReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RewardDisplayModel> Execute(long rewardId)
        {
            var reward = await _repository.GetById(rewardId);

            if (reward is null)
                throw new NotFoundException(ResourceMessagesException.REWARD_NOT_FOUND);

            var response = _mapper.Map<RewardDisplayModel>(reward);

            return response;
        }
    }
}