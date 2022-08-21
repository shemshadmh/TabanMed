
using Common;
using Domain.Entities.MedicalCenters.Translation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.MedicalCenterEntities.TranslationEntities
{
    public class MedicalCenterTranslationConfigurations : IEntityTypeConfiguration<MedicalCenterTranslation>
    {
        public void Configure(EntityTypeBuilder<MedicalCenterTranslation> builder)
        {

            builder.HasKey(translation => new { translation.MedicalCenterId, translation.LanguageId });

            builder.Property(translation => translation.Name)
                .HasColumnType(ModelConstants.Shared.NVarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.NameMaxLength)
                .IsRequired();

            builder.Property(translation => translation.Address)
                .HasColumnType(ModelConstants.Shared.NVarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.AddressMaxLength)
                .IsRequired();

            builder.Property(translation => translation.AgentName)
                .HasColumnType(ModelConstants.Shared.NVarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.AgentNameMaxLength)
                .IsRequired(false);

            //Relation

            builder.HasOne(translation => translation.MedicalCenter)
                .WithMany(medical => medical.MedicalCenterTranslations)
                .HasForeignKey(translation => translation.MedicalCenterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(translation => translation.Language)
                .WithMany(language => language.MedicalCenterTranslations)
                .HasForeignKey(translation => translation.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
