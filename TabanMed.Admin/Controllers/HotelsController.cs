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

        [HttpPost("hotel-tours/{id:int}")]
        public async Task<IActionResult> GetHotelTours([DataSourceRequest] DataSourceRequest request, int id)
        {
            var data = await _hotelApplication.GetHotelTours(id);
            return Json(await data.ToDataSourceResultAsync(request));
        }

        [HttpGet, ValidateAntiForgeryToken, AjaxOnly]
        public Task<IActionResult> GetHotelAlbums(int hotelId) =>
            Task.FromResult<IActionResult>(ViewComponent("HotelAlbum", new { hotelId }));

        [HttpPost("hotels-comments")]
        public async Task<IActionResult> GetHotelsComments([DataSourceRequest] DataSourceRequest request, int id)
        {
            //var data = await _hotelApplication.GetAllHotelComments(id);
            //return Json(await data.ToDataSourceResultAsync(request));
            return Ok();
        }

        #endregion

        #region Commands

        

        [HttpPost("edit-hotel"), ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> Edit([FromForm] EditHotelDto model)
        {
            var response = new Response<bool>();
            if(!ModelState.IsValid)
                return Json(response.Failed(ModelState.GetErrorMessages(), ErrorMessages.ModelValidationError));

            var operationRes = await _hotelApplication.EditHotel(model);
            return Json(operationRes.IsSucceeded ? response.Succeeded() : response.Failed(operationRes.Message!));
        }

        [HttpPost("delete-hotel"), ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> Delete([FromBody] DeleteHotelDto model)
        {
            var response = new Response<bool>();
            if(!ModelState.IsValid)
                return Json(response.Failed(ModelState.GetErrorMessages(),
                    ErrorMessages.ModelValidationError));

            var operationRes = await _hotelApplication.DeleteHotel(model);
            return Json(operationRes.IsSucceeded ? response.Succeeded() : response.Failed(ErrorMessages.ErrorOccurred));
        }

        [HttpPost("append-image"), ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> AppendToAlbum([FromForm] AppendToHotelAlbumDto model)
        {
            var response = new Response<string>();
            if(!ModelState.IsValid)
                return Json(response.Failed(ModelState.GetErrorMessages(),
                    ErrorMessages.ModelValidationError));

            var operation = await _hotelApplication.AppendToHotelAlbum(model);
            return Json(operation.IsSucceeded
                ? response.Succeeded((string)operation.ReturnValue!, "")
                : response.Failed(ErrorMessages.ErrorOccurred));
        }


        [HttpPost, ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> RemoveHotelAlbum([FromBody] RemoveFromHotelAlbumDto model)
        {
            var response = new Response<bool>();
            if(!ModelState.IsValid)
                return Json(response.Failed(ErrorMessages.ErrorOccurred));

            var operation = await _hotelApplication.RemoveFromHotelAlbum(model);

            return Json(operation.IsSucceeded
                ? response.Succeeded()
                : response.Failed(operation.Message!));
        }

        [HttpPost("edit-hotel-facilities"), ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> EditHotelFacilities([FromForm] EditHotelFacilitiesDto model)
        {
            var response = new Response<bool>();
            if(!ModelState.IsValid)
                return Json(response.Failed(ModelState.GetErrorMessages(), ErrorMessages.ModelValidationError));

            var operation = await _hotelApplication.EditHotelFacilities(model);
            return Json(operation.IsSucceeded
                ? response.Succeeded()
                : response.Failed(operation.Message!));
        }

        #endregion
    }
}
