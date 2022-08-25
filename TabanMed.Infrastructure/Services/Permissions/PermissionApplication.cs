using Application.Interfaces.Application;
using Application.Interfaces.Permissions;
using Domain.Entities.Identity;
using Domain.Entities.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TabanMed.Infrastructure.Services.Permissions;

public class PermissionApplication : IPermissionApplication
{
    private readonly ILogger<PermissionApplication> _logger;
    private readonly IApplicationDbContext _dbContext;

    public PermissionApplication(ILogger<PermissionApplication> logger, IApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<bool> HasAccess(string claim, List<string> roles)
    {
        try
        {
            if (string.IsNullOrEmpty(claim) || roles == null || !roles.Any())
                return false;

            var roleIds = await _dbContext.Set<Role>()
                .AsNoTracking()
                .Where(x => roles.Any(u => u == x.Name))
                .Select(x => x.Id)
                .ToListAsync();
            var hasAccess = await _dbContext.Set<RoleClaim>().AsNoTracking()
                .AnyAsync(roleClaim => roleIds.Contains(roleClaim.RoleId) && roleClaim.ClaimValue == claim);
            return hasAccess;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "couldn't check Has Permission Access.");
            return false;
        }
    }

    public Task<bool> IsOperator(string username)
    {
        throw new NotImplementedException();
    }

    public async Task AddPermissionList(List<Permission> model)
    {
        foreach (var item in model)
        {
            if (!await _dbContext.Permission.AnyAsync(x => x.Claim == item.Claim))
            {
                await _dbContext.Permission.AddAsync(item);
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}