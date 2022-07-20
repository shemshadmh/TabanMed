using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<string>
    {
        public ApplicationUser User { get; set; } = null!;
    }
}
