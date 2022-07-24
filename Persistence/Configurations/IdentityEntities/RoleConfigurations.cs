using Application;
using Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            //builder.HasQueryFilter(r => r.Name != StaticDetails.AdminRole);

            builder.Property(r => r.DisplayName)
                .HasMaxLength(ModelConstants.Role.DisplayNameMaxLength)
                .IsRequired();


            builder.HasData(
                // Seed Admin Role
                new Role
                {
                    Id = AppConstants.AdminRoleId,
                    Name = AppConstants.AdminRole,
                    DisplayName = "ادمین کل سیستم",
                    NormalizedName = AppConstants.AdminRole.ToUpper().Normalize()
                },
                // Seed System Operator Role
                new Role
                {
                    Id = "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                    Name = "SystemOperator",
                    DisplayName = "اپراتور سیستم",
                    NormalizedName = "SystemOperator".ToUpper().Normalize()
                }
            );
        }
    }
}
