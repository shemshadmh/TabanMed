using Application.Dtos.Countries;
using Application.Interfaces.Destination;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TabanMed.Admin.Extensions;

namespace TabanMed.Admin.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryApplication _countryApplication;
        public CountriesController(ICountryApplication countryApplication)
        {
            _countryApplication = countryApplication;
        }

        [HttpGet, Display(Name = "لیست کشورها")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetCountriesList([DataSourceRequest] DataSourceRequest request)
        {
            var data = await _countryApplication.GetCountriesListAsync();
            return Json(await data.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([DataSourceRequest] DataSourceRequest request,
            CountryListItem country)
        {
            if (!ModelState.IsValid)
                return Json(await new[] { country }.ToDataSourceResultAsync(request, ModelState));

            var res = await _countryApplication.CreateCountry(country);

            if (res.IsSucceeded)
            {
                country.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { country }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);



        }



        [HttpPost]
        public async Task<IActionResult> UpdateCountry([DataSourceRequest] DataSourceRequest request,
            CountryListItem country)
        {
            if (!ModelState.IsValid)
                return Json(await new[] { country }.ToDataSourceResultAsync(request, ModelState));

            var res = await _countryApplication.UpdateCountry(country);

            if (res.IsSucceeded)
            {
                country.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { country }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);




        }
    }
}
