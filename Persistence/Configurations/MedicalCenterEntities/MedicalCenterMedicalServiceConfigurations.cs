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
    public class MedicalCenterMedicalServiceConfigurations : IEntityTypeConfiguration<MedicalCenterMedicalService>
    {
        public void Configure(EntityTypeBuilder<MedicalCenterMedicalService> builder)
        {
            builder.HasKey(medcialCenterMedicalService => new { medcialCenterMedicalService.MedicalCenterId, medcialCenterMedicalService.MedicalServiceId });

            builder.HasOne(medcialCenterMedicalService => medcialCenterMedicalService.MedicalCenter)
                .WithMany(medcialCenterMedicalService => medcialCenterMedicalService.MedicalCenterMedicalServices)
                .HasForeignKey(medcialCenterMedicalService => medcialCenterMedicalService.MedicalCenterId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted


            builder.HasOne(medcialCenterMedicalService => medcialCenterMedicalService.MedicalService)
                .WithMany(medcialCenterMedicalService => medcialCenterMedicalService.MedicalCenterMedicalServices)
                .HasForeignKey(medcialCenterMedicalService => medcialCenterMedicalService.MedicalServiceId)
                .OnDelete(DeleteBehavior.NoAction); // do not delete children when parent is deleted

        }
    }

}
