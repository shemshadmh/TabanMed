
using Application;
using Common;
using Domain.Entities.Destination;
using Domain.Entities.Destination.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.DestinationEntities
{
    public class CityConfigurations : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(city => city.Id);

            builder.Property(city => city.Id)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType);

            builder.HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

            builder.HasData(new List<City>()
            {
                new ()
                {
                    Id = 1,
                    CountryId = 1,
                },
                new ()
                {
                    Id = 2,
                    CountryId = 1,
                }
            });
        }
    }
}
