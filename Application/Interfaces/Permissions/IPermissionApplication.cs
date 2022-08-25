using Domain.Entities.Permission;

namespace Application.Interfaces.Permissions;

public interface IPermissionApplication
{
    /// <summary>
    /// این متد چک میکند که آیا کاربر دسترسی مورد نظر را دارد یا خیر
    /// </summary>
    /// <param name="claim">دسترسی مورد نظر</param>
    /// <param name="roles">نقش های کاربر</param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    Task<bool> HasAccess(string claim, List<string> roles);

    Task<bool> IsOperator(string username);

    Task AddPermissionList(List<Permission> model);
}