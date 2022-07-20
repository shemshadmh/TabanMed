
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public Role Role { get; set; } = null!;
    }
}
