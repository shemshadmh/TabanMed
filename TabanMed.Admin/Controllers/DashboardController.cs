using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
