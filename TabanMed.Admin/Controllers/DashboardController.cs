using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// change language and set in cookie, for example: fa-IR
        /// </summary>
        /// <param name="culture">culture code (fa-IR | en-US) </param>
        /// <param name="returnUrl">redirect to this address after changing language, if not present you will receive 200 </param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangeCulture(string culture = "fa-IR")
        {
            try
            {
                var cultureInfo = CultureInfo.GetCultureInfo(culture);
            }
            catch
            {
                return BadRequest();
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(1)
                }
            );

            return RedirectToAction(nameof(Index));
        }
    }
}
