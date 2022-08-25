using System.ComponentModel.DataAnnotations;
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.Hotels.Hotels;
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
            HotelFacilityListItem facility, int? pId = null)
        {
            if (!ModelState.IsValid)
                return Json(await new[] { facility }.ToDataSourceResultAsync(request, ModelState));

            facility.ParentId = pId;
            var res = await _hotelFacilityApplication.CreateFacility(facility);

            if (res.IsSucceeded)
            {
                facility.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult = 
                new(await new[] { facility }.ToDataSourceResultAsync(request, ModelState))
            {
                UserMessage = res.Message
            };

            return Json(returnResult);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFacility([DataSourceRequest] DataSourceRequest request,
            HotelFacilityListItem facility)
        {
            if(!ModelState.IsValid)
                return Json(await new[] { facility }.ToDataSourceResultAsync(request, ModelState));

            var res = await _hotelFacilityApplication.UpdateFacility(facility);

            if(res.IsSucceeded)
            {
                facility.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { facility }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);
        }


        [HttpPost("edit-hotel-facilities"), ValidateAntiForgeryToken, AjaxOnly]
        public async Task<IActionResult> EditHotelFacilities([FromForm] EditHotelFacilitiesDto model)
        {
            var response = new Response<bool>();
            if (!ModelState.IsValid)
                return Json(response.Failed(ModelState.GetErrorMessages(), ErrorMessages.ModelValidationError));

            var operation = await _hotelFacilityApplication.EditHotelFacilities(model);
            return Json(operation.IsSucceeded
                ? response.Succeeded()
                : response.Failed(operation.Message!));
        }

    }
}
