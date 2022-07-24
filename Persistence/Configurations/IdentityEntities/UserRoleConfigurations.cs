
using Application;
using Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(userRole => userRole.UserId)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            builder.HasOne(userRole => userRole.User)
                .WithMany(user => user.Roles)
                .HasForeignKey(userRole => userRole.UserId);

            builder.HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(userRole => userRole.RoleId);

            // Seed Admin with adminRole
            builder.HasData(new UserRole
            {
                RoleId = AppConstants.AdminRoleId,
                UserId = AppConstants.HatefAdminUserId
            },
            // Seed operator with operatorRole
            new UserRole
            {
                RoleId = "6f4024b0-153e-4b8b-a851-5befbdb955f9",
                UserId = "04a76057-948a-4fd1-b9f0-ed36991fcaa5"
            });
        }
    }
}
