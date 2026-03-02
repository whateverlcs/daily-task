namespace daily_task.Domain.Repositories.Profile
{
    public interface IProfileUpdateOnlyRepository
    {
        public Task<Entities.Profile?> GetById(long id);

        public void Update(Entities.Profile profile);
    }
}