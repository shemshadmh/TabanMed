
using Common;
using Domain.Entities.Destination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.DestinationEntities
{
    public class CityConfigurations : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(country => country.Id);

            builder.Property(country => country.Id)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType);

            builder.Property(country => country.EnName)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType) // varchar for just english characters
                .HasMaxLength(ModelConstants.City.CityEnNameMaxLength)
                .IsRequired();

            builder.Property(country => country.FaName)
                .HasMaxLength(ModelConstants.City.CityEnNameMaxLength)
                .IsRequired();

            builder.HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
