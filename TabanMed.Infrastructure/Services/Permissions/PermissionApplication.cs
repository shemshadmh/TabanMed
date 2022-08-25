using Application.Interfaces.Application;
using Application.Interfaces.Permissions;
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

    public Task<bool> HasAccess(string claim, List<string> roles)
    {
        throw new NotImplementedException();
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