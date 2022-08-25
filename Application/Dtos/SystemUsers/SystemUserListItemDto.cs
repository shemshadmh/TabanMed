namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class SystemUserListItemDto
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public IReadOnlyList<RolesAndLabels> RolesAndLabels { get; set; }
        public string CreatedOn { get; set; }
    }

    public class RolesAndLabels
    {
        public string RoleDisplayName { get; set; }
        public string RoleLabelColor { get; set; }
    }
}
