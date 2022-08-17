using Application.Interfaces.Destination;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TabanMed.Admin.Controllers
{
    public class citiesController : Controller
    {
        private readonly ICityApplication _cityApplication;
        public citiesController(ICityApplication cityApplication)
        {
            _cityApplication = cityApplication;
        }


        [HttpGet, Display(Name = "لیست شهرها")]
        public IActionResult Index() => View();


        [HttpPost]
        public async Task<IActionResult> GetCitiesList([DataSourceRequest] DataSourceRequest request,
            int CountryId)
        {
            var data = await _cityApplication.GetCitiesListAsync(CountryId);
            return Json(await data.ToDataSourceResultAsync(request));
        }





    }
}
