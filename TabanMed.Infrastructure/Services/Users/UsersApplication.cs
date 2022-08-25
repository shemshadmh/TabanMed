using Application.Interfaces.Application;
using Application.Interfaces.Users;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace TabanMed.Infrastructure.Services.Users;

public class UsersApplication : IUserApplication
{
    private readonly IApplicationDbContext _dbContext;

    public UsersApplication(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string?> GetUserSecurityStamp(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>().Where(applicationUser => applicationUser.Id == userId)
            .Select(applicationUser => applicationUser.SecurityStamp).FirstOrDefaultAsync(cancellationToken);
    }
}