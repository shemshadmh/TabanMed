using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.Controllers;

[Authorize(Policy = nameof(AppConstants.DynamicPermission))]
public class BaseAdminController : Controller
{
    public void TempDataMessage(string message, bool isSuccessful = false)
    {
        TempData[AppConstants.TempDataMessageTitle] = $"{message}~{isSuccessful}";
    }
}