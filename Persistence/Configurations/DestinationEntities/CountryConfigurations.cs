
using Common;
using Domain.Entities.Destination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.DestinationEntities
{
    public class CountryConfigurations : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(country => country.Id);

            builder.Property(country => country.Id)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType);

            builder.Property(country => country.EnName)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType) // varchar for just english characters
                .HasMaxLength(ModelConstants.Country.CountryEnNameMaxLength)
                .IsRequired();

            builder.Property(country => country.FaName)
                .HasMaxLength(ModelConstants.Country.CountryEnNameMaxLength)
                .IsRequired();

            builder.HasData(new List<Country>()
            {
                new ()
                {
                   Id = 1,
                   EnName = "Iran",
                   FaName = "ایران"
                }
            });
        }
    }
}
