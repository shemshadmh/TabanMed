
namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class RoleDetailsDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDisplayName { get; set; }
        public IList<string>? RolePermissions { get; set; }

    }
}
