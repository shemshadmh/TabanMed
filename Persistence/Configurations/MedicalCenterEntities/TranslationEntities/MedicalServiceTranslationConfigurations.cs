

using Common;
using Domain.Entities.MedicalCenters.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.MedicalCenterEntities.TranslationEntities
{
    public class MedicalServiceTranslationConfigurations: IEntityTypeConfiguration<MedicalServiceTranslation>
    {
        public void Configure(EntityTypeBuilder<MedicalServiceTranslation> builder)
        {
            builder.HasKey(translation => new { translation.MedicalServiceId, translation.LanguageId });

            builder.Property(translation => translation.Title)
                .HasMaxLength(ModelConstants.MedicalService.TitleMaxLength)
                .IsRequired();

            builder.HasOne(translation => translation.MedicalService)
                .WithMany(medicalService => medicalService.MedicalServiceTranslations)
                .HasForeignKey(medicalService => medicalService.MedicalServiceId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted


            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.MedicalServiceTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
