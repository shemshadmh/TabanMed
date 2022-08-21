using Common;
using Domain.Entities.MedicalCenters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.MedicalCenterEntities
{
    public class MedicalCenterConfigurations : IEntityTypeConfiguration<MedicalCenter>
    {
        public void Configure(EntityTypeBuilder<MedicalCenter> builder)
        {
            builder.HasKey(medicalCenter => medicalCenter.Id);

            builder.Property(medicalCenter => medicalCenter.ImageUrl)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.ImageUrlMaxLength)
                .IsRequired();

            builder.Property(medicalCenter => medicalCenter.PhoneNumber)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.PhoneNumberMaxLength)
                .IsRequired();

            builder.Property(medicalCenter => medicalCenter.AgentPhoneNumber)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.AgentPhoneNumberMaxLength)
                .IsRequired(false);


            // Relations
            builder.HasOne(medicalCenter => medicalCenter.City)
                .WithMany(city => city.MedicalCenters)
                .HasForeignKey(medicalCenter => medicalCenter.CityId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
