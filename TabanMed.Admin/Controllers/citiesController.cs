using Application.Dtos.Cities;
using Application.Interfaces.Destination;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TabanMed.Admin.Extensions;

namespace TabanMed.Admin.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityApplication _cityApplication;
        public CitiesController(ICityApplication cityApplication)
        {
            _cityApplication = cityApplication;
        }

        [HttpPost]
        public async Task<IActionResult> GetCitiesList([DataSourceRequest] DataSourceRequest request,
            int CountryId)
        {
            var data = await _cityApplication.GetCitiesListAsync(CountryId);
            return Json(await data.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([DataSourceRequest] DataSourceRequest request,
           CityListItem city, int cId )
        {
            if (!ModelState.IsValid)
                return Json(await new[] { city }.ToDataSourceResultAsync(request, ModelState));

            city.CountryId= cId;
            var res = await _cityApplication.CreateCity(city);

            if (res.IsSucceeded)
            {
                city.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { city }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFacility([DataSourceRequest] DataSourceRequest request,
            CityListItem city)
        {
            if (!ModelState.IsValid)
                return Json(await new[] { city }.ToDataSourceResultAsync(request, ModelState));

            var res = await _cityApplication.UpdateCity(city);

            if (res.IsSucceeded)
            {
                city.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { city }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);
        }

    }
}
