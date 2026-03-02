using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Profile;
using daily_task.Domain.Repositories.Rank;

namespace daily_task.Application.UseCases.Profile.GetProfile
{
    public class GetProfileUseCase : IGetProfileUseCase
    {
        private readonly IMapper _mapper;
        private readonly IProfileReadOnlyRepository _repositoryReadProfile;
        private readonly IProfileWriteOnlyRepository _repositoryWriteProfile;
        private readonly IRankReadOnlyRepository _repositoryRank;
        private readonly IUnitOfWork _unitOfWork;

        public GetProfileUseCase(
            IMapper mapper,
            IProfileReadOnlyRepository repositoryReadProfile,
            IProfileWriteOnlyRepository repositoryWriteProfile,
            IRankReadOnlyRepository repositoryRank,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repositoryReadProfile = repositoryReadProfile;
            _repositoryWriteProfile = repositoryWriteProfile;
            _repositoryRank = repositoryRank;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProfileDisplayModel> Execute()
        {
            var ranks = await _repositoryRank.GetAllRanks();
            var profile = await _repositoryReadProfile.GetProfile();

            if (profile is null)
            {
                profile = new Domain.Entities.Profile
                {
                    TasksCreated = 0,
                    TasksCompleted = 0,
                    GoldEarned = 0,
                    GoldSpent = 0,
                    GoldBalance = 0,
                    ClaimedRewards = 0,
                    RankId = ranks.First().Id
                };

                await _repositoryWriteProfile.Add(profile);

                await _unitOfWork.Commit();
            }

            var response = _mapper.Map<ProfileDisplayModel>(profile);

            response.Rank = ranks.First(r => r.Id == profile.RankId).Name;

            return response;
        }
    }
}