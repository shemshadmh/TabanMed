using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<string>
    {

        public ApplicationUser()
        {
            Roles = new HashSet<UserRole>();
            Claims = new HashSet<UserClaim>();
            Tokens = new HashSet<UserToken>();
            Logins = new HashSet<UserLogin>();
        }

        // Customized
        public string Name { get; set; } = null!;
        public string Family { get; set; } = null!;
        /// <summary>
        /// true=>male
        /// false=>female
        /// </summary>
        public bool? Gender { get; set; }
        public DateTime? BirthDay { get; set; }

        public bool IsOperator { get; set; }
        public string ProfilePicture { get; set; } = null!;
        public string IpAddress { get; set; } = null!;

        /// <summary>
        /// Join user <see cref="Name"/> and <see cref="Family"/>
        /// </summary>
        public string FullName => $"{this.Name} {this.Family}";


        #region Interface

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        #endregion

        #region Relations

        // Identity
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserToken> Tokens { get; set; }

        #endregion
    }
}
