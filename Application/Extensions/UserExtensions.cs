using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Application.Extensions;

public static class UserExtensions
{
    /// <summary>
    /// Get UserId
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    /// <summary>
    /// find user name in claims
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    /// <summary>
    /// find user timezone in claims
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string? GetTimeZone(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.StateOrProvince)?.Value;
    }

    public static TimeSpan GetTimeZoneOffset(this ClaimsPrincipal user)
    {
        var tz = user.FindFirst(ClaimTypes.StateOrProvince)?.Value;
        if (tz is null)
            return TimeSpan.Zero;
        return TimeZoneInfo.FindSystemTimeZoneById(tz).BaseUtcOffset;
    }

    //public static string GetEnableStatus(this ClaimsPrincipal user)
    //{
    //    return user.FindFirst(StaticDetails.EnableStatus).Value;
    //}

    public static string? GetSecurityStamp(this ClaimsPrincipal user)
    {
        var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

        return user.FindFirst(securityStampClaimType)?.Value;
    }

    public static List<string> GetUserRoles(this ClaimsPrincipal user)
    {
        var list = new List<string>();

        var roles = user.FindAll(ClaimTypes.Role);
        list.AddRange(roles.Select(item => item.Value));

        return list;
    }
}