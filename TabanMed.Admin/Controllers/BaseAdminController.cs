using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.Controllers;

[Authorize]
public class BaseAdminController : Controller
{
    public void TempDataMessage(string message, bool isSuccessful = false)
    {
        TempData[AppConstants.TempDataMessageTitle] = $"{message}~{isSuccessful}";
    }
}