using System.ComponentModel.DataAnnotations;
using Application.Dtos.TourServices;
using Application.Interfaces.TourServices;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Resources.ErrorMessages;

namespace TabanMed.Admin.Controllers
{
    public class TourServicesController : BaseAdminController
    {

        private readonly ITourServiceApplication _tourServiceApplication;

        public TourServicesController(ITourServiceApplication tourServiceApplication)
        {
            _tourServiceApplication = tourServiceApplication;
        }

        [HttpGet, Display(Name = "لیست خدمات تور")]
        public IActionResult Index() => View();
        
        [HttpPost]
        public async Task<IActionResult> GetTourServices([DataSourceRequest] DataSourceRequest request) // 1 => tehran
        {
            var data = await _tourServiceApplication.GetTourServicesAsync();
            return Json(await data.ToDataSourceResultAsync(request));
        }

        [HttpGet, Display(Name = "افزودن خدمات تور")]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TourServicesForEditDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var res = await _tourServiceApplication.CreateTourServiceAsync(model);

            if (!res.IsSucceeded)
            {
                ModelState.AddModelError(String.Empty, res.Message!);
                return View(model);
            }

            TempDataMessage(res.Message!, res.IsSucceeded);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var tourService = await _tourServiceApplication.GetTourServiceDetailsAsync(Id);
            
            if (tourService is null)
                return NotFound();

            return View(model: tourService);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(TourServicesForEditDto hotelServicesForEditDtoDto)
        {
            if (!ModelState.IsValid)
                return View(hotelServicesForEditDtoDto);

            var result = await _tourServiceApplication.EditTourServiceAsync(hotelServicesForEditDtoDto);

            if (!result.IsSucceeded)
            {
                ModelState.AddModelError(String.Empty, result.Message!);
                return View(hotelServicesForEditDtoDto);
            }

            TempDataMessage(result.Message!, result.IsSucceeded);
            return RedirectToAction(nameof(Index));

        }



    }
}
