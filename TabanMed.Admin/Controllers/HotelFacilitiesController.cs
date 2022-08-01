using System.ComponentModel.DataAnnotations;
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Interfaces.Hotels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Resources.ErrorMessages;
using TabanMed.Admin.Attributes;
using TabanMed.Admin.Extensions;

namespace TabanMed.Admin.Controllers
{
    public class HotelFacilitiesController : BaseAdminController
    {
        private readonly IHotelFacilityApplication _hotelFacilityApplication;

        public HotelFacilitiesController(IHotelFacilityApplication hotelFacilityApplication)
        {
            _hotelFacilityApplication = hotelFacilityApplication;
        }

        [HttpGet, Display(Name = "لیست امکانت هتل")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetFacilityTitlesList([DataSourceRequest] DataSourceRequest request,
            int? pId = null)
        {
            var data = await _hotelFacilityApplication.GetFacilityListAsync(pId);
            return Json(await data.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacility([DataSourceRequest] DataSourceRequest request,
            HotelFacilityListItem facility)
        {
            if(ModelState.IsValid)
            {
                var res = await _hotelFacilityApplication.CreateFacility(facility);

                if(res.IsSucceeded)
                    facility.Id = (int)res.ReturnValue!;
                else
                    ModelState.AddModelError("", res.Message!);
            }

            return Json(await new[] { facility }.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFacility([DataSourceRequest] DataSourceRequest request,
            HotelFacilityListItem facility)
        {
            if(ModelState.IsValid)
            {
                var res = await _hotelFacilityApplication.UpdateFacility(facility);

                if(res.IsSucceeded)
                    facility.Id = (int)res.ReturnValue!;
                else
                    ModelState.AddModelError("", res.Message!);
            }

            return Json(await new[] { facility }.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost, ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> CreateChildFacility([FromForm] CreateHotelFacilityDto facilityDto)
        {
            var result = new Response<bool>();
            if(!ModelState.IsValid)
                return Json(result.Failed(ModelState.GetErrorMessages(), ErrorMessages.ErrorOccurred));

            var operationResult = await _hotelFacilityApplication.CreateFacility(facilityDto);

            return Json(operationResult.IsSucceeded
                ? result.Succeeded(operationResult.Message ?? "")
                : result.Failed(operationResult.Message!));
        }

        [HttpPost, ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> EditChildFacility([FromForm] EditHotelFacilityDto facilityDto)
        {
            var result = new Response<bool>();
            if(!ModelState.IsValid)
                return Json(result.Failed(ModelState.GetErrorMessages(), ErrorMessages.ErrorOccurred));

            var operationResult = await _hotelFacilityApplication.UpdateFacility(facilityDto);

            return Json(operationResult.IsSucceeded
                ? result.Succeeded(operationResult.Message ?? "")
                : result.Failed(operationResult.Message!));
        }
    }
}
