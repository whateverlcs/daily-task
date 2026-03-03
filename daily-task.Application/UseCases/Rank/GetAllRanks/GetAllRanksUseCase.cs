using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories.Rank;

namespace daily_task.Application.UseCases.Rank.GetAllRanks
{
    public class GetAllRanksUseCase : IGetAllRanksUseCase
    {
        private readonly IMapper _mapper;
        private readonly IRankReadOnlyRepository _repository;

        public GetAllRanksUseCase(
            IMapper mapper,
            IRankReadOnlyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<RankDisplayModel>> Execute()
        {
            var ranks = await _repository.GetAllRanks();

            var response = _mapper.Map<IList<RankDisplayModel>>(ranks);

            return response;
        }
    }
}