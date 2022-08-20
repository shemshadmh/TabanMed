using System.ComponentModel.DataAnnotations;
using Application.Dtos.Hotels.Hotels;
using Application.Dtos.MedicalCenters;
using Application.Extensions;
using Application.Interfaces.MedicalCenters;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Resources.ErrorMessages;

namespace TabanMed.Admin.Controllers
{
    public class MedicalCentersController : BaseAdminController
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

        [HttpGet]
        public IActionResult Create(int id)
            => View(new CreateMedicalCenterDto() { CityId = id });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [FromForm] CreateMedicalCenterDto model)
        {
            if (id != model.CityId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            if (!model.MedicalCenterPic.IsValidImage())
            {
                ModelState.AddModelError(String.Empty, ErrorMessages.IsNotAValidImageFile);
                return View(model);
            }

            var res = await _medicalCenterApplication.CreateMedicalCenter(model);

            if (!res.IsSucceeded)
            {
                ModelState.AddModelError(String.Empty, res.Message!);
                return View(model);
            }

            TempDataMessage(res.Message!, res.IsSucceeded);
            return RedirectToAction(nameof(Index), new { cityId = model.CityId });
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();

            var medicalCenterDto = await _medicalCenterApplication.GetMedicalCenterDetails(id);

            if (medicalCenterDto is null)
                return NotFound();

            return View(model: medicalCenterDto);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int medicalCenterId,int langId)
        {
            var medicalCenter = await _medicalCenterApplication.GetMedicalCenterForEditAsync(medicalCenterId, langId);

            if (medicalCenter is null)
                return NotFound();

            return View(model: medicalCenter);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicalCenterForEditDetailsDto medicalCenterDto)
        {
            if (!ModelState.IsValid)
                return View(medicalCenterDto);

            var result = await _medicalCenterApplication.EditMedicalCenterAsync(medicalCenterDto);

            if (!result.IsSucceeded)
            {
                ModelState.AddModelError(String.Empty, result.Message!);
                return View(medicalCenterDto);
            }

            TempDataMessage(result.Message!, result.IsSucceeded);
            return RedirectToAction(nameof(Details), new { id = medicalCenterDto.MedicalCenterId });

        }

    }
}
