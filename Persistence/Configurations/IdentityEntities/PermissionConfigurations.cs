
using Common;
using Domain.Entities.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class PermissionConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(permission => permission.Id);

            builder.Property(permission => permission.Claim)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Permission.ClaimMaxLength)
                .IsRequired();

            builder.Property(permission => permission.DisplayText)
                .HasColumnType(ModelConstants.Shared.NVarCharColumnType)
                .HasMaxLength(ModelConstants.Permission.DisplayTextMaxLength)
                .IsRequired();
        }
    }
}
