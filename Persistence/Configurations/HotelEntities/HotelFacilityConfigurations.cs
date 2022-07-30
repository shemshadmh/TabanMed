using Common;
using Domain.Entities.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.HotelEntities
{
    public class HotelFacilityConfigurations : IEntityTypeConfiguration<HotelFacility>
    {
        public void Configure(EntityTypeBuilder<HotelFacility> builder)
        {
            builder.HasKey(facility => facility.Id);

            builder.Property(facility => facility.ParentId)
                .IsRequired(false);

            builder.Property(facility => facility.Title)
                .HasMaxLength(ModelConstants.HotelFacility.TitleMaxLength)
                .IsRequired();

            //Relations
            builder.HasOne(facility => facility.Parent)
                .WithMany(facility => facility.Children)
                .HasForeignKey(facility => facility.ParentId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
