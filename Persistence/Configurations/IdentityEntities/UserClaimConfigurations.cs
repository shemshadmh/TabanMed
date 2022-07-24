
using Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class UserClaimConfigurations : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.Property(userClaim => userClaim.UserId)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            builder.HasOne(userClaim => userClaim.User)
                .WithMany(user => user.Claims)
                .HasForeignKey(userClaim => userClaim.UserId);
        }
    }
}
