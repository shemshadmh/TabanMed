using Application;
using Application.Interfaces.Application;
using Application.Interfaces.SystemRoles;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;
using TabanAgency.Domain.Dtos.SystemUsers;

namespace TabanMed.Infrastructure.Services.SystemRoles;

public class SystemRoleServices : ISystemRoleServices
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<SystemRoleServices> _logger;
    private readonly ICurrentServices _currentServices;

    public SystemRoleServices(IApplicationDbContext dbContext,
        ILogger<SystemRoleServices> logger, ICurrentServices currentServices)
    {
        _dbContext = dbContext;
        _logger = logger;
        _currentServices = currentServices;
    }

    public async Task<IReadOnlyList<SystemRoleListItemDto>> GetRolesInList()
    {
        try
        {
            return await _dbContext.Set<Role>().AsNoTracking()
                .Where(r => r.Name != AppConstants.AdminRole)
                .Select(r => new SystemRoleListItemDto()
                {
                    RoleId = r.Id,
                    DisplayName = r.DisplayName
                }).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Couldn't Get System Roles List");
            return new List<SystemRoleListItemDto>();
        }
    }

    public async Task<IReadOnlyList<PermissionListItemDto>?> GetPermissionsList()
    {
        try
        {
            return await _dbContext.Permission.AsNoTracking()
                .Select(p => new PermissionListItemDto()
                {
                    DisplayName = p.DisplayText,
                    Claim = p.Claim
                }).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Couldn't Get Permission List");
            return null;
        }
    }

    public async Task<bool> CreateRoleAsync(CreateRoleDto roleDto)
    {
        try
        {
            if (await _dbContext.Set<Role>().AnyAsync(r => r.DisplayName == roleDto.DisplayName
                                                           || r.Name == roleDto.Name))
                return false;

            var role = await _dbContext.Set<Role>().AddAsync(new Role()
            {
                DisplayName = roleDto.DisplayName,
                Name = roleDto.Name.Replace(" ", ""),
                NormalizedName = roleDto.Name.Replace(" ", "").Normalize()
            });

            foreach (string permission in roleDto.Permissions)
            {
                await _dbContext.Set<RoleClaim>().AddAsync(new RoleClaim()
                {
                    RoleId = role.Entity.Id,
                    ClaimValue = permission,
                    ClaimType = "Permission"
                });
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogWarning($"{roleDto.Name} Role Created Successfully => by {_currentServices.Username}");

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Create Role {roleDto.Name} => by {_currentServices.Username}");
            return false;
        }
    }

    public async Task<RoleDetailsDto?> GetRoleDetails(string roleId)
    {
        try
        {
            return await _dbContext.Set<Role>().AsNoTracking()
                .Where(r => r.Id == roleId)
                .Include(r => r.Claims)
                .Select(r => new RoleDetailsDto()
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName,
                    RolePermissions = GetRolePermissions(roleId).Result
                }).SingleAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Get Role Details for role id : {roleId}");
            return null;
        }
    }

    public async Task<RolePermissionsForEdit?> GetRolePermissionsForEdit(string roleId)
    {
        try
        {
            var dto = new RolePermissionsForEdit
            {
                PermissionListItems = await _dbContext.Permission
                    .AsNoTracking().Select(p => new PermissionListItemDto()
                    {
                        Claim = p.Claim,
                        DisplayName = p.DisplayText
                    }).ToListAsync(),
                Selected = await _dbContext.Set<RoleClaim>()
                    .AsNoTracking().Where(rp => rp.RoleId == roleId)
                    .Select(rp => rp.ClaimValue).ToListAsync(),
                SystemRoleListItemDto = await _dbContext.Set<Role>()
                    .AsNoTracking().Where(r => r.Id == roleId)
                    .Select(r => new SystemRoleListItemDto()
                    {
                        RoleId = roleId,
                        DisplayName = r.DisplayName
                    }).SingleAsync()
            };

            return dto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Get Role Permissions for Edit for role id : {roleId}");
            return null;
        }
    }

    public async Task<bool> EditRolePermissions(string roleId, List<string> permissions)
    {
        try
        {
            // Remove All Old Permissions
            await _dbContext.Set<RoleClaim>().Where(rp => rp.RoleId == roleId)
                .ForEachAsync(rp => _dbContext.Set<RoleClaim>().Remove(rp));

            foreach (var permission in permissions)
            {
                await _dbContext.Set<RoleClaim>().AddAsync(new RoleClaim()
                {
                    ClaimValue = permission,
                    RoleId = roleId,
                    ClaimType = "Permission"
                });
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogWarning(
                $"Successful Modify Role Permissions for role id : {roleId} => by {_currentServices.Username}");

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                $"Couldn't Modify Role Permissions for role id : {roleId} => by {_currentServices.Username}");
            return false;
        }
    }

    public async Task<EditRoleDto?> GetRoleDisplayNameForEdit(string roleId)
    {
        try
        {
            return await _dbContext.Set<Role>().Where(r => r.Id == roleId)
                .Select(r => new EditRoleDto()
                {
                    RoleId = r.Id,
                    DisplayName = r.DisplayName
                }).SingleAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Modify Role Permissions for role id : {roleId}");
            return null!;
        }
    }

    public async Task<bool> EditRoleDisplayName(EditRoleDto roleDto)
    {
        try
        {
            if (await _dbContext.Set<Role>()
                    .AnyAsync(r => r.DisplayName == roleDto.DisplayName && r.Id != roleDto.RoleId))
            {
                return false;
            }

            var role = await _dbContext.Set<Role>().FindAsync(roleDto.RoleId);
            role!.DisplayName = roleDto.DisplayName;

            _dbContext.Set<Role>().Update(role);
            await _dbContext.SaveChangesAsync();

            _logger.LogWarning(
                $"Successful Modify Role Display Name for role id : {roleDto.RoleId} => by {_currentServices.Username}");

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                $"Couldn't Modify Role Display Name for role id : {roleDto.RoleId} => by {_currentServices.Username}");
            return false;
        }
    }

    private async Task<IList<string>?> GetRolePermissions(string roleId)
    {
        try
        {
            List<string>? names = new List<string>();
            await _dbContext.Set<RoleClaim>()
                .AsNoTracking().Where(r => r.RoleId == roleId)
                .Select(rp => rp.ClaimValue).ForEachAsync(c =>
                    names.AddRange(_dbContext.Permission.AsNoTracking().Where(p => p.Claim == c)
                        .Select(p => p.DisplayText)));

            return names;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Get Role Permissions for role id : {roleId}");
            return null;
        }
    }

    public void Dispose()
    {
        _dbContext.DisposeAsync();
    }
}