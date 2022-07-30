
using Common;
using Domain.Entities.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.HotelEntities
{
    public class HotelConfigurations : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(hotel => hotel.Id);

            builder.Property(hotel => hotel.FaName)
                .HasMaxLength(ModelConstants.Hotel.FaNameMaxLength)
                .IsRequired();

            builder.Property(hotel => hotel.EnName)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType) // varchar for just english characters
                .HasMaxLength(ModelConstants.Hotel.EnNameMaxLength)
                .IsRequired();

            builder.Property(hotel => hotel.ImageUrl)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType) // varchar for just english characters
                .HasMaxLength(ModelConstants.Hotel.ImageUrlMaxLength)
                .IsRequired();

            builder.Property(hotel => hotel.About)
                .HasMaxLength(ModelConstants.Hotel.AboutMaxLength)
                .IsRequired(false);

            builder.Property(hotel => hotel.Stars)
                .HasColumnType(ModelConstants.Shared.TinyIntColumnType)
                .HasDefaultValue(ModelConstants.Shared.ZeroValue)
                .IsRequired();

            builder.Property(hotel => hotel.Address)
                .HasMaxLength(ModelConstants.Hotel.AddressMaxLength)
                .IsRequired(false);

            builder.Property(hotel => hotel.CallInformation)
                .HasMaxLength(ModelConstants.Hotel.CallInformationMaxLength)
                .IsRequired(false);

            builder.Property(hotel => hotel.WebsiteAddress)
                .HasMaxLength(ModelConstants.Hotel.WebsiteAddressMaxLength)
                .IsRequired(false);

            // Relations
            builder.HasOne(hotel => hotel.City)
                .WithMany(city => city.Hotels)
                .HasForeignKey(hotel => hotel.CityId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

        }
    }
}
