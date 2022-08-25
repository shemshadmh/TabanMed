

namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class RolePermissionsForEdit
    {
        public List<PermissionListItemDto> PermissionListItems { get; set; } = null!;
        public List<string> Selected { get; set; } = null!;
        public SystemRoleListItemDto SystemRoleListItemDto { get; set; } = null!;
    }
}
