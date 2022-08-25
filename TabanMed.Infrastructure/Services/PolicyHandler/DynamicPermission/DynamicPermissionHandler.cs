using System.Security.Claims;
using Application;
using Application.Interfaces.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TabanMed.Infrastructure.Services.PolicyHandler.DynamicPermission;

public class DynamicPermissionHandler : AuthorizationHandler<DynamicPermissionRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPermissionApplication _permissionService;


    public DynamicPermissionHandler(IHttpContextAccessor httpContextAccessor,
        IPermissionApplication permissionService)
    {
        _httpContextAccessor = httpContextAccessor;
        _permissionService = permissionService;
    }

    /// <inheritdoc />
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        DynamicPermissionRequirement requirement)
    {
        try
        {
            var user = _httpContextAccessor.HttpContext?.User;

            /*
            if (!await _permissionService.IsOperator(user!.Identity!.Name!))
            {
                context.Fail();
                return;
            }
            */

            //if(!await _permissionService.Is2FAEnabled(user!.Identity!.Name!))
            //{
            //    context.Fail();
            //    return;
            //}

            if (user!.IsInRole(AppConstants.AdminRole))
            {
                context.Succeed(requirement);
                return;
            }

            //اطلاعات مربوط به یو ار ال درخواستی کاربر را بدست می اوریم
            var routeData = _httpContextAccessor.HttpContext?.GetRouteData().Values;

            var area = routeData?["area"]?.ToString();
            var controller = routeData?["controller"]?.ToString();
            var action = routeData?["action"]?.ToString();

            var requiredClaim = $"{area}:{controller}:{action}";

            //نقش کاربر را بدست میاوریم
            var userRole = user.FindAll(ClaimTypes.Role).Select(item => item.Value).ToList();


            if (await _permissionService.HasAccess(requiredClaim, userRole))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
        catch
        {
            //TODO Log Error
            context.Fail();
        }
    }
}