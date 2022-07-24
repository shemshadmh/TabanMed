
using Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class RoleClaimConfigurations : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.Property(roleClaim => roleClaim.RoleId)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            builder
                .HasOne(roleClaim => roleClaim.Role)
                .WithMany(role => role.Claims)
                .HasForeignKey(roleClaim => roleClaim.RoleId);

            //could be needly in the future
            //builder.HasOne(roleClaim => roleClaim.Permission)
            //    .WithMany(permission => permission.RoleClaims)
            //    .HasForeignKey(roleClaim => roleClaim.PermissionId);
        }
    }
}
