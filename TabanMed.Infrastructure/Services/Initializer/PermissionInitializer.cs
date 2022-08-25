using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Application;
using Application.Interfaces.Initializer;
using Application.Interfaces.Permissions;
using Domain.Entities.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TabanMed.Infrastructure.Services.Initializer;

public class PermissionInitializer : IPermissionInitializer
{
    private readonly IPermissionApplication _permissionApplication;
    private readonly IActionDescriptorCollectionProvider _actionDescriptor;
    private readonly ILogger<PermissionInitializer> _logger;

    public PermissionInitializer(IActionDescriptorCollectionProvider actionDescriptor,
        ILogger<PermissionInitializer> logger, IPermissionApplication permissionApplication)
    {
        _actionDescriptor = actionDescriptor;
        _logger = logger;
        _permissionApplication = permissionApplication;
    }

    public async Task Initialize(System.Type baseControllerType)
    {
        List<Permission> permissionList = new();
        foreach (Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor actionDescriptor in _actionDescriptor
                     .ActionDescriptors.Items)
        {
            // if isn't page.
            var isType = actionDescriptor is PageActionDescriptor;

            if (isType == false)
            {
                // known as controller action
                ControllerActionDescriptor descriptor = (ControllerActionDescriptor)actionDescriptor;
                // check if action have authorize attribute with DynamicPermission policy name.
                bool hasPermission =
                    (descriptor.ControllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>()?.Policy ==
                     AppConstants.DynamicPermission
                     || descriptor.MethodInfo.GetCustomAttribute<AuthorizeAttribute>()?.Policy ==
                     AppConstants.DynamicPermission) && descriptor.MethodInfo
                                                         .GetCustomAttribute<ApiControllerAttribute>() == null
                                                     && (descriptor.MethodInfo
                                                             .GetCustomAttribute<HttpGetAttribute>() != null ||
                                                         descriptor.MethodInfo
                                                             .GetCustomAttribute<HttpPostAttribute>() != null)
                                                     && descriptor.ControllerTypeInfo
                                                         .GetCustomAttribute<ApiControllerAttribute>() == null;
                // if has DynamicPermission policy requirement
                if (hasPermission)
                {
                    // get area name from Area Attribute
                    /*
                    var area = descriptor.ControllerTypeInfo.BaseType?.Name.Replace("Controller", "")
                        .Replace("Base", "");
                        */
                    
                    var area = "";


                    if (string.IsNullOrEmpty(area))
                    {
                        area = descriptor.ControllerTypeInfo
                            .GetCustomAttribute<AreaAttribute>()?.RouteValue;
                    }

                    var permission = new Permission();
                    // make permission claim name with combination of area:controller:action
                    permission.Claim = $"{area}:{descriptor.ControllerName}:{descriptor.ActionName}";
                    // set a name for this permission that is used to display in panel
                    permission.DisplayText = descriptor.MethodInfo
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? string.Empty;

                    // if action has display name ([Display Name()] attribute value, add to the list.
                    if (!string.IsNullOrEmpty(permission.DisplayText))
                    {
                        permissionList.Add(permission);
                    }
                }
            }
        }

        if (permissionList.Any())
        {
            // save generated permissions list to database using the service.
            await _permissionApplication.AddPermissionList(permissionList);
            _logger.LogInformation("Requeired Claims inserted");
        }
    }
}