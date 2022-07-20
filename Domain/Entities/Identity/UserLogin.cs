using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public ApplicationUser User { get; set; } = null!;
    }
}
