using HahnCatFacts.Domain.Entities;
using HahnCatFacts.Domain.Interfaces.Repositories;
using HahnCatFacts.Infrastructure.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace HahnCatFacts.Infrastructure.Persistence.Repositories
{
    public class CatFactRepository : ICatFactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatFactRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<CatFact> GetAllNoTracking()
        {
            return _dbContext.CatFacts.AsNoTracking();
        }

        public async Task AddAsync(CatFact entity)
        {
            await _dbContext.CatFacts.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
