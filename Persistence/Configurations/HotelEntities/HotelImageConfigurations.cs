
using Common;
using Domain.Entities.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.HotelEntities
{
    public class HotelImageConfigurations : IEntityTypeConfiguration<HotelImage>
    {
        public void Configure(EntityTypeBuilder<HotelImage> builder)
        {
            builder.HasKey(image => image.ImageUrl);

            builder.Property(image => image.ImageAlt)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.HotelImage.ImageAltMaxLength);

            // Relations
            builder.HasOne(image => image.Hotel)
                .WithMany(hotel => hotel.Gallery)
                .HasForeignKey(image => image.HotelId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
