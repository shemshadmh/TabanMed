using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //Relations
            builder.HasOne(medicalService => medicalService.Parent)
                .WithMany(medicalService => medicalService.Children)
                .HasForeignKey(medicalService => medicalService.ParentId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted
        }
    }
}
