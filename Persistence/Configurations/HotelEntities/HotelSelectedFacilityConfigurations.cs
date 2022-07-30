
using Domain.Entities.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.HotelEntities
{
    public class HotelSelectedFacilityConfigurations : IEntityTypeConfiguration<HotelSelectedFacility>
    {
        public void Configure(EntityTypeBuilder<HotelSelectedFacility> builder)
        {
            builder.HasKey(hotelSelectedFacility => new { hotelSelectedFacility.HotelId, hotelSelectedFacility.HotelFacilityId });

            builder.HasOne(hotelSelectedFacility => hotelSelectedFacility.Hotel)
                .WithMany(hotel => hotel.HotelSelectedFacilities)
                .HasForeignKey(hotelSelectedFacility => hotelSelectedFacility.HotelId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted


            builder.HasOne(hotelSelectedFacility => hotelSelectedFacility.HotelFacility)
                .WithMany(hotelFacility => hotelFacility.HotelSelectedFacilities)
                .HasForeignKey(hotelSelectedFacility => hotelSelectedFacility.HotelFacilityId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

        }
    }
}
