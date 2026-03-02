using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories.Reward;

namespace daily_task.Application.UseCases.Reward.GetAllRewards
{
    public class GetAllRewardsUseCase : IGetAllRewardsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IRewardReadOnlyRepository _repository;

        public GetAllRewardsUseCase(
            IMapper mapper,
            IRewardReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<RewardDisplayModel>> Execute()
        {
            var rewards = await _repository.GetAllRewardsActive();

            var response = _mapper.Map<IList<RewardDisplayModel>>(rewards.OrderByDescending(x => x.CreatedOn));

            return response;
        }
    }
}