using AutoMapper;
using daily_task.Application.Models;
using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Profile;
using daily_task.Exceptions;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Profile.Update
{
    public class UpdateProfileUseCase : IUpdateProfileUseCase
    {
        private readonly IProfileUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProfileUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IProfileUpdateOnlyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task<bool> Execute(ProfileDisplayModel request)
        {
            var profile = await _repository.GetById(request.Id);

            if (profile is null)
                throw new NotFoundException(ResourceMessagesException.PROFILE_NOT_FOUND);

            _mapper.Map(request, profile);

            _repository.Update(profile);

            await _unitOfWork.Commit();

            return true;
        }
    }
}