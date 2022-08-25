using Application;
using Application.Interfaces.Application;
using Application.Interfaces.Users;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;
using TabanAgency.Domain.Dtos.SystemUsers;

namespace TabanMed.Infrastructure.Services.Users;

public class UsersApplication : IUserApplication
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UsersApplication> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentServices _currentUser;

    public UsersApplication(IApplicationDbContext dbContext, ICurrentServices currentUser,
        UserManager<ApplicationUser> userManager, ILogger<UsersApplication> logger)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<string?> GetUserSecurityStamp(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ApplicationUser>().Where(applicationUser => applicationUser.Id == userId)
            .Select(applicationUser => applicationUser.SecurityStamp).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SystemRoleListItemDto>> GetRolesList()
    {
        try
        {
            return await _dbContext.Set<Role>()
                .Where(r => r.Name != AppConstants.AdminRole)
                .Select(r => new SystemRoleListItemDto()
                {
                    RoleId = r.Name,
                    DisplayName = r.DisplayName
                }).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Couldn't Get System Roles List");
            throw;
        }
    }

    public async Task<IReadOnlyList<SystemRoleListItemDto>> GetRolesListWithIds()
    {
        try
        {
            return await _dbContext.Set<Role>()
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
            throw;
        }
    }


    public async Task<bool> CreateUserAsync(CreateUserDto userDto)
    {
        try
        {
            if (userDto.Username.ToLower() == AppConstants.HatefAdminUsername.ToLower())
            {
                _logger.LogWarning(
                    $"{_currentUser.Username} Wants to create new system user with admin word in username");
                return false;
            }

            var exist = await _userManager.FindByNameAsync(userDto.Username);
            if (exist != null)
            {
                return false;
            }

            var user = new ApplicationUser
            {
                UserName = userDto.Username,
                NormalizedUserName = userDto.Username.Normalize(),
                EmailConfirmed = true,
                Family = userDto.Family,
                Name = userDto.Name,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("N"),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                IsDeleted = false,
                IsOperator = true
            };

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userDto.Password);
            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                _logger.LogError("Couldn't create user!. due to {errors}", createResult.Errors);
                return false;
            }

            foreach (var role in userDto.Roles)
            {
                createResult = await _userManager.AddToRoleAsync(user, role);

                if (!createResult.Succeeded)
                {
                    _logger.LogError("Couldn't add role for user!. due to {errors}", createResult.Errors);
                    return false;
                }
            }

            _logger.LogWarning($"{user.UserName} created successfully => by {_currentUser.Username}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Create User => by {_currentUser.Username}");
            return false;
        }
    }

    public async Task<SystemUserDetailsDto?> GetUserDetails(string id)
    {
        try
        {
            return await _dbContext.Set<ApplicationUser>().AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => new SystemUserDetailsDto()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Family = u.Family,
                    Username = u.UserName,
                    Roles = u.Roles.Select(r => new SystemRoleListItemDto
                    {
                        DisplayName = r.Role.DisplayName,
                        RoleId = r.RoleId
                    }).ToList()
                }).SingleAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Get User Details For id : {id}");
            return null;
        }
    }

    public async Task<EditUserRolesDto?> GetUserForEditRoles(string userId)
    {
        try
        {
            return await _dbContext.Set<ApplicationUser>().AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new EditUserRolesDto()
                {
                    Id = u.Id,
                    Fullname = u.FullName,
                    Username = u.UserName,
                    UserRoles = u.Roles.Select(r => r.RoleId).ToList(),
                    Roles = _dbContext.Set<Role>()
                        .Where(r => r.Name != AppConstants.AdminRole)
                        .Select(r => new SystemRoleListItemDto
                        {
                            DisplayName = r.DisplayName,
                            RoleId = r.Id
                        }).ToList()
                }).SingleAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Get User Roles Details For id : {userId}");
            return null;
        }
    }

    public async Task<bool> EditUserRoles(string userId, List<string> selectedRoles)
    {
        try
        {
            var user = await _dbContext.Set<ApplicationUser>().Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;

            var currentUserRoleIds = user.Roles.Select(x => x.RoleId).ToList();
            var newRolesToAdd = selectedRoles.Except(currentUserRoleIds).ToList();
            foreach (var roleId in newRolesToAdd)
            {
                _dbContext.Set<UserRole>().Add(new UserRole { RoleId = roleId, UserId = userId });
            }

            var removedRoles = currentUserRoleIds.Except(selectedRoles).ToList();
            foreach (var roleId in removedRoles)
            {
                var userRole = _dbContext.Set<UserRole>()
                    .SingleOrDefault(ur => ur.RoleId == roleId && ur.UserId == userId);
                if (userRole != null)
                {
                    _dbContext.Set<UserRole>().Remove(userRole);
                }
            }

            user.LastModified = DateTime.UtcNow;

            var result = await _userManager.UpdateSecurityStampAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("Couldn't update user roles!. due to {errors}", result.Errors);
                return false;
            }

            _logger.LogWarning($"{user.UserName} roles updated successfully => by {_currentUser.Username}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Couldn't Update User Roles for Id:{userId} => by {_currentUser.Username}");
            return false;
        }
    }

    public async Task<bool> EditUserInfo(EditUserInfoDto userInfoDto)
    {
        try
        {
            var exist = await _dbContext.Set<ApplicationUser>().AnyAsync(u =>
                u.UserName.ToLower() == userInfoDto.Username.ToLower()
                && u.Id != userInfoDto.Id);
            if (exist)
                return false;

            var user = await _dbContext.Set<ApplicationUser>().Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == userInfoDto.Id);
            if (user == null)
                return false;

            user.Name = userInfoDto.Name;
            user.Family = userInfoDto.Family;
            user.UserName = userInfoDto.Username;
            user.LastModified = DateTime.UtcNow;

            var result = await _userManager.UpdateSecurityStampAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("Couldn't update user Information!. due to {errors}", result.Errors);
                return false;
            }

            _logger.LogWarning($"{user.UserName} Information updated successfully => by {_currentUser.Username}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                $"Couldn't Update User Information for Id:{userInfoDto.Id} => by {_currentUser.Username}");
            return false;
        }
    }

    public async Task<bool> EditUserPassword(EditSystemUserPasswordDto userPasswordDto)
    {
        try
        {
            var user = await _dbContext.Set<ApplicationUser>().Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == userPasswordDto.Id);
            if (user == null)
                return false;

            string newHashPassword = _userManager.PasswordHasher
                .HashPassword(user, userPasswordDto.Password);

            if (string.Equals(newHashPassword, user.PasswordHash))
            {
                _logger.LogInformation("user {name} ({id}) new account password is same as old password!",
                    user.UserName, user.Id);
                return true;
            }

            user.PasswordHash = newHashPassword;
            user.LastModified = DateTime.UtcNow;

            var result = await _userManager.UpdateSecurityStampAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("Couldn't update user password!. due to {errors}", result.Errors);
                return false;
            }

            _logger.LogWarning($"{user.UserName} password updated successfully => by {_currentUser.Username}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                $"Couldn't Update User password for Id:{userPasswordDto.Id} => by {_currentUser.Username}");
            return false;
        }
    }
}