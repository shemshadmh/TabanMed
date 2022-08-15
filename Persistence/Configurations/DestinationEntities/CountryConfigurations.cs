
using Application;
using Common;
using Domain.Entities.Destination;
using Domain.Entities.Destination.Translation;
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


            builder.HasData(new List<Country>()
            {
                new ()
                {
                   Id = 1,
                }
            });
        }
    }
}
