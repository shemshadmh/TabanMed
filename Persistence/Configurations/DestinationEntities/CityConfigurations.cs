
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
            builder.HasKey(city => city.Id);

            builder.Property(city => city.Id)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType);

            builder.Property(city => city.EnName)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType) // varchar for just english characters
                .HasMaxLength(ModelConstants.City.EnNameMaxLength)
                .IsRequired();

            builder.Property(city => city.FaName)
                .HasMaxLength(ModelConstants.City.FaNameMaxLength)
                .IsRequired();

            builder.HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

            builder.HasData(new List<City>()
            {
                new ()
                {
                    Id = 1,
                    EnName = "Tehran",
                    FaName = "تهران",
                    CountryId = 1
                },
                new ()
                {
                    Id = 2,
                    EnName = "Mashhad",
                    FaName = "مشهد",
                    CountryId = 1
                }
            });
        }
    }
}
