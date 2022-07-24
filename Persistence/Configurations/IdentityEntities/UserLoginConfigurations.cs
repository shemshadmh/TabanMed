
using Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class UserLoginConfigurations : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.Property(userLogin => userLogin.UserId)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            builder
                .HasOne(userLogin => userLogin.User)
                .WithMany(user => user.Logins)
                .HasForeignKey(userLogin => userLogin.UserId);
        }
    }
}
