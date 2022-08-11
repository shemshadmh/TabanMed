
using Common;
using Domain.Entities.Hotels.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.HotelEntities.TranslationEntities
{
    public class HotelTranslationConfigurations: IEntityTypeConfiguration<HotelTranslation>
    {
        public void Configure(EntityTypeBuilder<HotelTranslation> builder)
        {

            builder.HasKey(translation => new { translation.HotelId, translation.LanguageId });

            builder.Property(hotel => hotel.Name)
                .HasMaxLength(ModelConstants.Hotel.NameMaxLength)
                .IsRequired();

            builder.Property(hotel => hotel.About)
                .HasMaxLength(ModelConstants.Hotel.AboutMaxLength)
                .IsRequired(false);

            builder.Property(hotel => hotel.Address)
                .HasMaxLength(ModelConstants.Hotel.AddressMaxLength)
                .IsRequired(false);

            builder.HasOne(translation => translation.Hotel)
                .WithMany(hotel => hotel.HotelTranslations)
                .HasForeignKey(translation => translation.HotelId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.HotelTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
