using System;
using System.Collections.Generic;
using System.Linq;

using Common;
using Domain.Entities.MedicalCenters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.MedicalCenterEntities
{
    public class MedicalServiceConfigurations : IEntityTypeConfiguration<MedicalService>
    {
        public void Configure(EntityTypeBuilder<MedicalService> builder)
        {
            builder.HasKey(medicalService => medicalService.Id);

            builder.Property(medicalService => medicalService.ParentId)
                .IsRequired(false);
            builder.Property(tourService => tourService.Price)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType)
                .IsRequired();

            //Relations
            builder.HasOne(medicalService => medicalService.Parent)
                .WithMany(medicalService => medicalService.Children)
                .HasForeignKey(medicalService => medicalService.ParentId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
