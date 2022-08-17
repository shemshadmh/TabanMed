using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Entities.Hotels;
using Domain.Entities.MedicalCenters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.MedicalCenterEntities
{
    public class MedicalCenterConfigurations : IEntityTypeConfiguration<MedicalCenter>
    {
        public void Configure(EntityTypeBuilder<MedicalCenter> builder)
        {
            builder.HasKey(medicalcenter => medicalcenter.Id);

            builder.Property(medicalcenter => medicalcenter.ImageUrl)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.ImageUrlMaxLength)
                .IsRequired();

            builder.Property(medicalcenter => medicalcenter.PhoneNumber)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.PhoneNumberMaxLength)
                .IsRequired();

            builder.Property(medicalcenter => medicalcenter.AgentPhoneNumber)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.MedicalCenter.AgentPhoneNumberMaxLength)
                .IsRequired(false);


            // Relations
            builder.HasOne(medicalcenter => medicalcenter.City)
                .WithMany(city => city.MedicalCenters)
                .HasForeignKey(Medicalcenter => Medicalcenter.CityId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
