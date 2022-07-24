
using Application;
using Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.IdentityEntities
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength)
                .ValueGeneratedOnAdd();

            builder.Property(user => user.UserName)
                .HasMaxLength(ModelConstants.ApplicationUser.UsernameMaxLength);

            builder.Property(user => user.NormalizedUserName)
                .HasMaxLength(ModelConstants.ApplicationUser.UsernameMaxLength);

            builder.Property(user => user.Email)
                .HasMaxLength(ModelConstants.ApplicationUser.EmailMaxLength);

            builder.Property(user => user.NormalizedEmail)
                .HasMaxLength(ModelConstants.ApplicationUser.EmailMaxLength);

            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(ModelConstants.ApplicationUser.PhoneMaxLength);

            builder.Property(user => user.AccessFailedCount)
                .HasColumnType(ModelConstants.Shared.TinyIntColumnType);

            builder.Property(user => user.PasswordHash)
                .HasMaxLength(ModelConstants.ApplicationUser.PasswordHashMaxLength);

            builder.Property(user => user.SecurityStamp)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            builder.Property(user => user.ConcurrencyStamp)
                .HasMaxLength(ModelConstants.Shared.GUIDMaxLength);

            builder.Ignore(user => user.FullName);

            builder.Property(user => user.Name)
                .HasMaxLength(ModelConstants.ApplicationUser.NameMaxLength)
                .IsRequired();

            builder.Property(user => user.Family)
                .HasMaxLength(ModelConstants.ApplicationUser.FamilyMaxLength)
                .IsRequired();

            builder.Property(user => user.Gender)
                .IsRequired(false);

            builder.Property(user => user.BirthDay)
                .IsRequired(false);

            builder.Property(user => user.IsOperator)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(user => user.ProfilePicture)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.ApplicationUser.ProfilePictureMaxLength)
                .IsRequired(false);

            builder.Property(user => user.IpAddress)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(ModelConstants.ApplicationUser.IpAddressMaxLength)
                .IsRequired(false);

            builder.Property(user => user.Created)
                .HasColumnType(ModelConstants.Shared.SmallDatetimeColumnType) // smalldatetime: From January 1, 1900 to June 6, 2079 with an accuracy of 1 minutes
                .IsRequired();

            builder.Property(user => user.DeletedOn)
                .HasColumnType(ModelConstants.Shared.SmallDatetimeColumnType) // smalldatetime: From January 1, 1900 to June 6, 2079 with an accuracy of 1 minutes
                .IsRequired(false);

            builder.Property(user => user.LastModified)
                .HasColumnType(ModelConstants.Shared.SmallDatetimeColumnType) // smalldatetime: From January 1, 1900 to June 6, 2079 with an accuracy of 1 minutes
                .IsRequired(false);

            builder.Property(user => user.CreatedBy)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(50);

            builder.Property(user => user.DeletedBy)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(50);

            builder.Property(user => user.LastModifiedBy)
                .HasColumnType(ModelConstants.Shared.VarCharColumnType)
                .HasMaxLength(50);

            builder.HasQueryFilter(u => !u.IsDeleted);

            // Admin User
            builder.HasData(new ApplicationUser
            {
                Id = AppConstants.HatefAdminUserId,
                UserName = AppConstants.HatefAdminUsername,
                Email = "hatef@tabanmed.com",
                PasswordHash = "AQAAAAEAACcQAAAAEDCEjgFnVqs3jS+KYwhsCsNHoR7mV7tQ7/NUHc2bxUc9HjMuXSNCax/I5jPdFBGsVg==",
                Name = "محمد هاتف",
                Family = "شمشاد",
                Gender = true,
                IsOperator = true,
                LockoutEnabled = true,
                Created = DateTime.UtcNow,
                CreatedBy = "Seed",
                NormalizedUserName = AppConstants.HatefAdminUsername.ToUpper().Normalize(),
                NormalizedEmail = "hatef@tabanmed.com".ToUpper().Normalize(),
                SecurityStamp = Guid.NewGuid().ToString()
            },
            // System Operator User
            new ApplicationUser
            {
                Id = "04a76057-948a-4fd1-b9f0-ed36991fcaa5",
                UserName = "tabanmedOperator",
                Email = "operator@tabanmed.com",
                PasswordHash = "AQAAAAEAACcQAAAAEGO2+kmYpAenNWk5p1UYgYOMbU3/pUOoc4yRkUma3Zq2Hsc8g9HSWpztF3MozgJdig==",
                Name = "اپراتور",
                Family = "فیباتو",
                IsOperator = true,
                LockoutEnabled = true,
                Created = DateTime.UtcNow,
                CreatedBy = "Seed",
                NormalizedEmail = "operator@tabanmed.com".ToUpper().Normalize(),
                NormalizedUserName = "tabanmedOperator".ToUpper().Normalize(),
                SecurityStamp = Guid.NewGuid().ToString()
            });
        }
    }
}
