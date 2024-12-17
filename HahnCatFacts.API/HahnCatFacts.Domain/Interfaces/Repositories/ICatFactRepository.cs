using HahnCatFacts.Domain.Entities;

namespace HahnCatFacts.Domain.Interfaces.Repositories
{
    public interface ICatFactRepository
    {
        public IQueryable<CatFact> GetAllNoTracking();
        public Task AddAsync(CatFact entity);
        public Task SaveChangesAsync();
    }
}
