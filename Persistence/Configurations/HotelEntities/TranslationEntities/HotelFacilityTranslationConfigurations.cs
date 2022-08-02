
using Common;
using Domain.Entities.Hotels.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.HotelEntities.TranslationEntities
{
    public class HotelFacilityTranslationConfigurations : IEntityTypeConfiguration<HotelFacilityTranslation>
    {
        public void Configure(EntityTypeBuilder<HotelFacilityTranslation> builder)
        {
            builder.HasKey(translation => new { translation.FacilityId, translation.LanguageId });

            builder.Property(translation => translation.Title)
                .HasMaxLength(ModelConstants.HotelFacility.TitleMaxLength)
                .IsRequired();

            builder.HasOne(translation => translation.HotelFacility)
                .WithMany(hotelFacility => hotelFacility.HotelFacilityTranslations)
                .HasForeignKey(translation => translation.FacilityId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted


            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.HotelFacilityTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
