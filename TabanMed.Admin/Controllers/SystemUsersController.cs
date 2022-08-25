using System.ComponentModel.DataAnnotations;
using Application;
using Application.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Resources.ErrorMessages;
using TabanAgency.Domain.Dtos.SystemUsers;

namespace TabanMed.Admin.Controllers;

public class SystemUsersController : BaseAdminController
{
    private readonly IUserApplication _userServices;

    public SystemUsersController(IUserApplication userServices)
    {
        _userServices = userServices;
    }

    [Display(Name = "لیست کاربران سیستمی")]
    [HttpGet]
    public IActionResult Index() => View();


    [Display(Name = "افزودن کاربر سیستمی جدید")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Roles"] = await _userServices.GetRolesList()
                            ?? new List<SystemRoleListItemDto>();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty,
                " ErrorMessages.DataEntryError");
            ViewData["Roles"] = await _userServices.GetRolesList()
                                ?? new List<SystemRoleListItemDto>();
            return View(userDto);
        }

        var res = await _userServices.CreateUserAsync(userDto);

        if (!res)
        {
            ModelState.AddModelError(string.Empty,
                ErrorMessages.ErrorOccurred);
            ViewData["Roles"] = await _userServices.GetRolesList()
                                ?? new List<SystemRoleListItemDto>();
            return View(userDto);
        }

        TempDataMessage("عملیات با موقیت انجام شد", true);
        return RedirectToAction(nameof(Index));
    }


    [Display(Name = "جزئیات کاربر سیستمی")]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        var user = await _userServices.GetUserDetails(id);
        if (user is null)
            return NotFound();

        return View(model: user);
    }

    [Display(Name = "ویرایش نقش‌های کاربر سیستمی")]
    [HttpGet]
    public async Task<IActionResult> EditUserRoles(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest();

        var user = await _userServices.GetUserForEditRoles(id);
        if (user is null)
            return NotFound();

        return View(model: user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .Where(v => v.ValidationState == ModelValidationState.Invalid)
                .Select(v => v.Errors.Select(e => e.ErrorMessage).First())
                .First();
            TempDataMessage(errorMessage);
            return RedirectToAction("EditUserRoles", new { id = userId });
        }

        var res = await _userServices.EditUserRoles(userId, roles);

        TempDataMessage("res.Message", res);
        return RedirectToAction("EditUserRoles", new { id = userId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUserInfo(EditUserInfoDto userInfoDto)
    {
        if (!ModelState.IsValid ||
            userInfoDto.Username.ToLower() == AppConstants.HatefAdminUsername)
        {
            var errorMessage = ModelState.Values
                .Where(v => v.ValidationState == ModelValidationState.Invalid)
                .Select(v => v.Errors.Select(e => e.ErrorMessage).First())
                .First();
            TempDataMessage(errorMessage);
            return RedirectToAction(nameof(Details), new { id = userInfoDto.Id });
        }

        var res = await _userServices.EditUserInfo(userInfoDto);

        TempDataMessage("res.Message", res);
        return RedirectToAction(nameof(Details), new { id = userInfoDto.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUserPassword(EditSystemUserPasswordDto userPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.Values
                .Where(v => v.ValidationState == ModelValidationState.Invalid)
                .Select(v => v.Errors.Select(e => e.ErrorMessage).First())
                .First();
            TempDataMessage(errorMessage);
            return RedirectToAction(nameof(Details), new { id = userPasswordDto.Id });
        }

        var res = await _userServices.EditUserPassword(userPasswordDto);

        TempDataMessage("res.Message", res);
        return RedirectToAction(nameof(Details), new { id = userPasswordDto.Id });
    }

    //TODO Write Soft Delete Action
}