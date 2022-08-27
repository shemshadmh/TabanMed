using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Entities.TourServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.TourServicesEntities
{
    public class TourServiceConfigurations : IEntityTypeConfiguration<TourService>
    {
        public void Configure(EntityTypeBuilder<TourService> builder)
        {
            builder.HasKey(tourService => tourService.Id);

            builder.Property(tourService => tourService.Price)
                .HasColumnType(ModelConstants.Shared.SmallIntColumnType)
                .IsRequired(true);
        }
    }
}
