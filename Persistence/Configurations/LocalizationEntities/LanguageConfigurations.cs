
using Application;
using Common;
using Domain.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.LocalizationEntities
{
    public class LanguageConfigurations : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(language => language.Lcid);

            builder.Property(language => language.Lcid)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType);

            builder.Property(language => language.Name)
                .HasColumnType(ModelConstants.Shared.NVarCharColumnType)
                .HasMaxLength(ModelConstants.Language.NameMaxLength)
                .IsRequired();

            builder.Property(language => language.IsoName)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Language.IsoNameMaxLength)
                .IsRequired();

            builder.HasData(new List<Language>()
            {
                new()
                {
                    Lcid = AppConstants.FaLanguageLcid,
                    Name = "فارسی",
                    IsoName = AppConstants.FaIrCulture
                },
                new()
                {
                    Lcid = AppConstants.EnLanguageLcid,
                    Name = "English",
                    IsoName = AppConstants.EnUsCulture
                },
                new()
                {
                    Lcid = AppConstants.ArLanguageLcid,
                    Name = "العربیة",
                    IsoName = AppConstants.ArIqCulture
                },
                new()
                {
                    Lcid = AppConstants.AfLanguageLcid,
                    Name = "پشتو/دری",
                    IsoName = AppConstants.PrsAfCulture
                },
            });
        }
    }
}
