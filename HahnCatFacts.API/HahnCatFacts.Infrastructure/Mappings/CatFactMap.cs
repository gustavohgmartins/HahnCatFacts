using HahnCatFacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnCatFacts.Infrastructure.Mappings
{
    public class CatFactMap : IEntityTypeConfiguration<CatFact>
    {
        public void Configure(EntityTypeBuilder<CatFact> builder)
        {
            builder.ToTable("CatFact");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

            builder.Property(p => p.Description)
                   .IsRequired();
        }
    }
}
