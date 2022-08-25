using TabanAgency.Domain.Dtos.SystemUsers;

namespace Application.Interfaces.Users;

public interface IUserApplication
{
    Task<string?> GetUserSecurityStamp(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SystemRoleListItemDto>> GetRolesList();
    Task<IReadOnlyList<SystemRoleListItemDto>> GetRolesListWithIds();
    Task<bool> CreateUserAsync(CreateUserDto userDto);
    Task<SystemUserDetailsDto?> GetUserDetails(string id);
    Task<EditUserRolesDto?> GetUserForEditRoles(string userId);
    Task<bool> EditUserRoles(string userId, List<string> selectedRoles);
    Task<bool> EditUserInfo(EditUserInfoDto userInfoDto);
    Task<bool> EditUserPassword(EditSystemUserPasswordDto userPasswordDto);
}