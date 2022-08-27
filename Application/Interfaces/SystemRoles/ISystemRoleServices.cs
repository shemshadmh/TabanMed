using TabanAgency.Domain.Dtos.SystemUsers;

namespace Application.Interfaces.SystemRoles;

public interface ISystemRoleServices:IDisposable
{
    Task<IReadOnlyList<SystemRoleListItemDto>> GetRolesInList();
    Task<IReadOnlyList<PermissionListItemDto>?> GetPermissionsList();

    Task<bool> CreateRoleAsync(CreateRoleDto role);
    Task<RoleDetailsDto?> GetRoleDetails(string roleId);

    Task<RolePermissionsForEdit?> GetRolePermissionsForEdit(string roleId);

    Task<bool> EditRolePermissions(string roleId, List<string> permissions);

    Task<EditRoleDto?> GetRoleDisplayNameForEdit(string roleId);
    Task<bool> EditRoleDisplayName(EditRoleDto roleDto);
}