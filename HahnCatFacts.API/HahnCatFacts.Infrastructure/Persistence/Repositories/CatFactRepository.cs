using HahnCatFacts.Domain.Entities;
using HahnCatFacts.Domain.Interfaces;
using HahnCatFacts.Infrastructure.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace HahnCatFacts.Infrastructure.Persistence.Repositories
{
    public class CatFactRepository : ICatFactRepository
    {
        private readonly ApplicationDbContext _dbContex;

        public CatFactRepository(ApplicationDbContext context)
        {
            _dbContex = context;
        }

        public IQueryable<CatFact> GetAllNoTracking()
        {
            return _dbContex.CatFacts.AsNoTracking();
        }

        public async Task AddAsync(CatFact entity)
        {
            await _dbContex.CatFacts.AddAsync(entity);
            await _dbContex.SaveChangesAsync();
        }
    }
}
