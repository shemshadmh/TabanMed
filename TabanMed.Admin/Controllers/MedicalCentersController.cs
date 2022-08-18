using System.ComponentModel.DataAnnotations;

using Application.Interfaces.MedicalCenters;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.Controllers
{
    public class MedicalCentersController : Controller
    {
        private readonly IMedicalCenterApplication _medicalCenterApplication;

        public MedicalCentersController(IMedicalCenterApplication medicalCenterApplication)
        {
            _medicalCenterApplication = medicalCenterApplication;
        }


        [Display(Name = "لیست مراکز درمانی"), HttpGet]
        public async Task<IActionResult> Index(int? cityId)
        {
            if (cityId == null)
                return View(await _medicalCenterApplication.GetCitiesWithMedicalCenter());

            var cityDto = await _medicalCenterApplication.GetCityInformation(cityId.Value);

            if (cityDto is null)
                return NotFound();

            ViewData["cId"] = cityDto.CityId;
            ViewData["cName"] = cityDto.CityName;
            return View("MedicalCenterByCity");


        }




        [HttpPost]
        public async Task<IActionResult> GetMedicalCenter([DataSourceRequest] DataSourceRequest request, int cId = 1) // 1 => tehran
        {
            var data = await _medicalCenterApplication.GetMedicalCenter(cId);
            return Json(await data.ToDataSourceResultAsync(request));
        }

    }
}
