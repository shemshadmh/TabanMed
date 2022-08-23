using System.ComponentModel.DataAnnotations;
using Application.Common;
using Application.Dtos.Hotels.Hotels;
using Application.Extensions;
using Application.Interfaces.Hotels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Resources.ErrorMessages;
using TabanMed.Admin.Attributes;
using TabanMed.Admin.Extensions;

namespace TabanMed.Admin.Controllers
{
    public class HotelsController : BaseAdminController
    {
        private readonly IHotelApplication _hotelApplication;

        public HotelsController(IHotelApplication hotelApplication)
        {
            _hotelApplication = hotelApplication;
        }

        #region queries

        [Display(Name = "لیست هتل‌ها"), HttpGet]
        public async Task<IActionResult> Index(int? cityId)
        {
            if(cityId == null)
                return View(await _hotelApplication.GetCitiesWithHotels());

            var cityDto = await _hotelApplication.GetCityInformation(cityId.Value);

            if(cityDto is null)
                return NotFound();

            ViewData["cId"] = cityDto.CityId;
            ViewData["cName"] = cityDto.CityName;
            return View("HotelsByCity");
        }

        [HttpPost]
        public async Task<IActionResult> GetHotels([DataSourceRequest] DataSourceRequest request, int cId = 1) // 1 => tehran
        {
            var data = await _hotelApplication.GetHotels(cId);
            return Json(await data.ToDataSourceResultAsync(request));
        }

        [HttpGet]
        public IActionResult Create(int id) 
            => View(new CreateHotelDto(){CityId = id});

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[FromForm] CreateHotelDto model)
        {
            if (id != model.CityId)
                return NotFound();
            
            if(!ModelState.IsValid)
                return View(model);

            if(!model.HotelPic.IsValidImage())
            {
                ModelState.AddModelError(String.Empty, ErrorMessages.IsNotAValidImageFile);
                return View(model);
            }

            var res = await _hotelApplication.CreateHotel(model);

            if (!res.IsSucceeded)
            {
                ModelState.AddModelError(String.Empty, res.Message!);
                return View(model);
            }

            TempDataMessage(res.Message!, res.IsSucceeded);
            return RedirectToAction(nameof(Index),new { cityId = model.CityId});
        }

        [HttpGet, Display(Name = "جزییات هتل")]
        public async Task<IActionResult> Details(int id)
        {
            var data = await _hotelApplication.GetHotelDetails(id);
            if(data is null)
                return NotFound();

            return View(data);
        }

        [Display(Name = "ویرایش هتل"), HttpGet]
        public async Task<IActionResult> Edit(int hotelId, int languageId)
        {
            var hotel = await _hotelApplication.GetHotelForEditAsync(hotelId, languageId);

            if (hotel is null)
                return NotFound();

            return View(model: hotel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HotelTranslationForEditDto hotelTranslationDto)
        {
            if (!ModelState.IsValid)
                return View(hotelTranslationDto);

            var result = await _hotelApplication.EditHotelAsync(hotelTranslationDto);

            if (!result.IsSucceeded)
            {
                ModelState.AddModelError(String.Empty, result.Message!);
                return View(hotelTranslationDto);
            }

            TempDataMessage(result.Message!, result.IsSucceeded);
            return RedirectToAction(nameof(Details), new { id = hotelTranslationDto.HotelId });

        }


        [HttpGet]
        public async Task<IActionResult> EditBasics(int id)
        {
            if (id <= 0)
                return BadRequest();

            var medicalCenterDto = await _hotelApplication.GetHotelBasicsDetails(id);

            if (medicalCenterDto is null)
                return NotFound();

            return View(model: medicalCenterDto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBasics(int id, [FromForm] HotelDetailsBasicsDto model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            if (model.HotelPic is not null)
            {
                if (!model.HotelPic.IsValidImage())
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.IsNotAValidImageFile);
                    return View(model);
                }
            }

            var res = await _hotelApplication.EditHotelBasicsAsync(model);

            if (!res.IsSucceeded)
            {
                ModelState.AddModelError(string.Empty, res.Message!);
                return View(model);
            }

            TempDataMessage(res.Message!, res.IsSucceeded);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }






        #endregion


    }
}
