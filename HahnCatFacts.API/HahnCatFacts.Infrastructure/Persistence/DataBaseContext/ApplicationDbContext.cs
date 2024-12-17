using HahnCatFacts.Domain.Entities;
using HahnCatFacts.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace HahnCatFacts.Infrastructure.Persistence.DataBaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<CatFact> CatFacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CatFactMap());
        }

    }
}
