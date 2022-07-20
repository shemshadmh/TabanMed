using Domain.Entities.Permission;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class Role : IdentityRole<string>
    {
        public string DisplayName { get; set; } = null!;
        public ICollection<RoleClaim>? Claims { get; set; }
        public ICollection<UserRole>? Users { get; set; }

        public ICollection<RolePermission>? RolePermissions { get; set; }

    }
}
