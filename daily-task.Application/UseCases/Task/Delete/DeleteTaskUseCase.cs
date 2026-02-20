using daily_task.Domain.Repositories;
using daily_task.Domain.Repositories.Task;
using daily_task.Exceptions;
using daily_task.Exceptions.ExceptionsBase;

namespace daily_task.Application.UseCases.Task.Delete
{
    public class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskReadOnlyRepository _repositoryRead;
        private readonly ITaskWriteOnlyRepository _repositoryWrite;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskUseCase(
            ITaskReadOnlyRepository repositoryRead,
            ITaskWriteOnlyRepository repositoryWrite,
            IUnitOfWork unitOfWork)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task Execute(long taskId)
        {
            var task = await _repositoryRead.GetById(taskId);

            if (task is null)
                throw new NotFoundException(ResourceMessagesException.TASK_NOT_FOUND);

            await _repositoryWrite.Delete(taskId);

            await _unitOfWork.Commit();
        }
    }
}