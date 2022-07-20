
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public ApplicationUser User { get; set; } = null!;
    }
}
