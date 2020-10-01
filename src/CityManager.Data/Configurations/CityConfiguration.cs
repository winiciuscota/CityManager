using CityManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityManager.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Ibge).IsUnique(true);

            builder.HasIndex(x => new { x.Name, x.Ibge }).IsUnique(true);

            builder.Property(x => x.UF).HasMaxLength(2);
        }
    }
}