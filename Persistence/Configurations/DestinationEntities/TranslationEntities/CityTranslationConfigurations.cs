
using Application;
using Common;
using Domain.Entities.Destination.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.DestinationEntities.TranslationEntities
{
    public class CityTranslationConfigurations:IEntityTypeConfiguration<CityTranslation>
    {
        public void Configure(EntityTypeBuilder<CityTranslation> builder)
        {
            builder.HasKey(translation => new { translation.CityId, translation.LanguageId });

            builder.Property(translation => translation.Name)
                .HasMaxLength(ModelConstants.City.NameMaxLength)
                .IsRequired();

            builder.HasOne(translation => translation.City)
                .WithMany(city => city.CityTranslations)
                .HasForeignKey(translation => translation.CityId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted


            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.CityTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

            builder.HasData(new List<CityTranslation>()
            {
                new ()
                {
                    CityId = 1,
                    LanguageId = AppConstants.FaLanguageId,
                    Name = "تهران"
                },
                new ()
                {
                    CityId = 1,
                    LanguageId = AppConstants.EnLanguageId,
                    Name = "Tehran"
                },
                new ()
                {
                    CityId = 2,
                    LanguageId = AppConstants.FaLanguageId,
                    Name = "مشهد"
                },
                new ()
                {
                    CityId = 2,
                    LanguageId = AppConstants.EnLanguageId,
                    Name = "Mashhad"
                }
            });
        }
    }
}
