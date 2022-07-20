using Domain.Entities.Identity;

namespace Domain.Entities.Permission
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string Permission { get; set; } = null!;

        #region Relations

        public string RoleId { get; set; } = null!;
        public Role Role { get; set; } = null!;

        #endregion

    }
}
