using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Profile;

namespace daily_task.Application.UseCases.Profile.Register
{
    public class RegisterProfileUseCase : IRegisterProfileUseCase
    {
        private readonly IProfileWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterProfileUseCase(
            IProfileWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Execute(ProfileDisplayModel request)
        {
            var profile = _mapper.Map<Domain.Entities.Profile>(request);

            await _repository.Add(profile);

            await _unitOfWork.Commit();

            return true;
        }
    }
}