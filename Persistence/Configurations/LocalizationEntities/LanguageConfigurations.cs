
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
            builder.HasKey(language => language.Id);

            builder.Property(language => language.Id)
                .HasColumnType(ModelConstants.Shared.TinyIntColumnType);

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
                    Id = 1,
                    Name = "فارسی",
                    IsoName = "fa"
                },
                new()
                {
                    Id = 2,
                    Name = "English",
                    IsoName = "en"
                },
                new()
                {
                    Id = 3,
                    Name = "العربیة",
                    IsoName = "ar"
                },
                new()
                {
                    Id = 4,
                    Name = "پشتو/دری",
                    IsoName = "ps"
                },
            });
        }
    }
}
