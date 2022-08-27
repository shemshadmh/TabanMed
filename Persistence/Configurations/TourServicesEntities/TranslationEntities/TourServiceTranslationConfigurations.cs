using System;

using Common;
using Domain.Entities.TourServices.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.TourServicesEntities.TranslationEntities
{
    public class TourServiceTranslationConfigurations : IEntityTypeConfiguration<TourServiceTranslation>
    {
        public void Configure(EntityTypeBuilder<TourServiceTranslation> builder)
        {
            builder.HasKey(translation => new { translation.TourServiceId, translation.LanguageId });

            builder.Property(tourService => tourService.Title)
                .HasMaxLength(ModelConstants.TourService.TitleMaxLength)
                .IsRequired();

            builder.Property(tourService => tourService.Description)
                .HasMaxLength(ModelConstants.TourService.DescriptionMaxLength)
                .IsRequired(false);

            builder.HasOne(translation => translation.TourService)
                .WithMany(tourService => tourService.TourServiceTranslation)
                .HasForeignKey(translation => translation.TourServiceId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.TourServiceTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
