
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
                .HasMaxLength(ModelConstants.City.EnNameMaxLength)
                .IsRequired();

            builder.Property(country => country.FaName)
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
