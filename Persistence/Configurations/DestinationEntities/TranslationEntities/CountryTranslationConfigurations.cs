
using Application;
using Common;
using Domain.Entities.Destination.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.DestinationEntities.TranslationEntities
{
    public class CountryTranslationConfigurations:IEntityTypeConfiguration<CountryTranslation>
    {
        public void Configure(EntityTypeBuilder<CountryTranslation> builder)
        {
            builder.HasKey(translation => new { translation.CountryId, translation.LanguageId });

            builder.Property(translation => translation.Name)
                .HasMaxLength(ModelConstants.Country.NameMaxLength)
                .IsRequired();

            builder.HasOne(translation => translation.Country)
                .WithMany(country => country.CountryTranslations)
                .HasForeignKey(translation => translation.CountryId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted


            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.CountryTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

            builder.HasData(new List<CountryTranslation>()
            {
                new ()
                {
                    CountryId = 1,
                    LanguageId = AppConstants.FaLanguageLcid,
                    Name = "ایران"
                },
                new ()
                {
                    CountryId = 1,
                    LanguageId = AppConstants.EnLanguageLcid,
                    Name = "Iran"
                }
            });
        }
    }
}
