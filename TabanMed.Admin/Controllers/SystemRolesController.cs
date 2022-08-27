using System.ComponentModel.DataAnnotations;
using Application.Interfaces.SystemRoles;
using TabanAgency.Domain.Dtos.SystemUsers;
using Microsoft.AspNetCore.Mvc;
using TabanMed.Admin.Controllers;

namespace TabanAgency.Web.Areas.Admin.Controllers;

public class SystemRolesController : BaseAdminController
{
    private readonly ISystemRoleServices _roleServices;

    public SystemRolesController(ISystemRoleServices roleServices)
    {
        _roleServices = roleServices;
    }

    [Display(Name = "لیست نقش‌های سیستمی")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _roleServices.GetRolesInList();
        return View(list);
    }

    [Display(Name = "افزودن نقش سیستمی جدید")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Permissions"] = await _roleServices.GetPermissionsList()
                                  ?? new List<PermissionListItemDto>();
        return View();
    }

    [ValidateAntiForgeryToken,
     HttpPost]
    public async Task<IActionResult> Create(CreateRoleDto role)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(role.Name))
        {
            /*ModelState.AddModelError(string.Empty,
                ErrorMessages.DataEntryError);*/
            ViewData["Permissions"] = await _roleServices.GetPermissionsList()
                                      ?? new List<PermissionListItemDto>();
            return View();
        }

        var res = await _roleServices.CreateRoleAsync(role);

        if (!res)
        {
            /*ModelState.AddModelError(string.Empty,
                res.Message);*/
            ViewData["Permissions"] = await _roleServices.GetPermissionsList()
                                      ?? new List<PermissionListItemDto>();
            return View(role);
        }

        // TempDataMessage(res.Message, res.Succeed);
        return RedirectToAction(nameof(Index));

    }

    [Display(Name = "جزئیات نقش سیستمی")]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();
            

        var role = await _roleServices.GetRoleDetails(id);

        if (role is null)
            return NotFound();

        return View(model: role);
    }

    [Display(Name = "ویرایش دسترسی‌های نقش سیسمی")]
    [HttpGet]
    public async Task<IActionResult> EditRolePermissions(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();


        var rolePermissions = await _roleServices.GetRolePermissionsForEdit(id);

        if (rolePermissions is null)
            return NotFound();
            

        return View(model: rolePermissions);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRolePermissions(string id, List<string> Permissions)
    {
            
        if (!ModelState.IsValid || string.IsNullOrEmpty(id))
        {
            /*ModelState.AddModelError(string.Empty,
                ErrorMessages.DataEntryError);*/

            var rolePermissions = await _roleServices.GetRolePermissionsForEdit(id);

            if (rolePermissions is null)
                return NotFound();

            return View(model: rolePermissions);
        }

        var res = await _roleServices.EditRolePermissions(id, Permissions);
        // TempDataMessage(res.Message, res.Succeed);

        return RedirectToAction(nameof(Details), new { id = id });

    }

    [Display(Name = "ویرایش عنوان نمایشی نقش سیسمی")]
    [HttpGet]
    public async Task<IActionResult> EditRoleDisplayName(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var roleDisplayName = await _roleServices.GetRoleDisplayNameForEdit(id);

        if (roleDisplayName is null)
            return Problem();

        return View(model: roleDisplayName);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> EditRoleDisplayName(EditRoleDto roleDto)
    {
        if (!ModelState.IsValid)
        {
            /*ModelState.AddModelError(string.Empty,
                ErrorMessages.DataEntryError);*/

            return View(roleDto);
        }

        var res = await _roleServices.EditRoleDisplayName(roleDto);

        if (!res)
        {
            /*ModelState.AddModelError(string.Empty,
                res.Message);*/
            return View(roleDto);
        }

        return StatusCode(252, "res.Message");
    }
}