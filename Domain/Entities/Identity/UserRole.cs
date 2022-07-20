using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class UserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; } = null!;

        public Role Role { get; set; } = null!;
    }
}
