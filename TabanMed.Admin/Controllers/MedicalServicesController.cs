using System.ComponentModel.DataAnnotations;
using Application.Dtos.MedicalCenters.MedicalServices;
using Application.Interfaces.MedicalCenters;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TabanMed.Admin.Extensions;

namespace TabanMed.Admin.Controllers
{
    public class MedicalServicesController : BaseAdminController
    {
        private readonly IMedicalServiceApplication _medicalServiceApplication;
        public MedicalServicesController(IMedicalServiceApplication medicalServiceApplication)
        {
            _medicalServiceApplication = medicalServiceApplication;
        }

        [HttpGet, Display(Name = "لیست خدمات درمانی")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetMedicalServiceTitlesList([DataSourceRequest] DataSourceRequest request,
            int? pId = null)
        {
            var data = await _medicalServiceApplication.GetMedicalServiceListAsync(pId);
            return Json(await data.ToDataSourceResultAsync(request));
        }


        [HttpPost]
        public async Task<IActionResult> CreateMedicalService([DataSourceRequest] DataSourceRequest request,
            MedicalServiceListItemDto medicalServiceDto, int? pId = null)
        {
            if (!ModelState.IsValid)
                return Json(await new[] { medicalServiceDto }.ToDataSourceResultAsync(request, ModelState));

            medicalServiceDto.ParentId = pId;
            var res = await _medicalServiceApplication.CreateMedicalService(medicalServiceDto);

            if (res.IsSucceeded)
            {
                medicalServiceDto.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { medicalServiceDto }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMedicalService([DataSourceRequest] DataSourceRequest request,
            MedicalServiceListItemDto medicalServiceDto)
        {
            if (!ModelState.IsValid)
                return Json(await new[] { medicalServiceDto }.ToDataSourceResultAsync(request, ModelState));

            var res = await _medicalServiceApplication.UpdateMedicalService(medicalServiceDto);

            if (res.IsSucceeded)
            {
                medicalServiceDto.Id = (int)res.ReturnValue!;
            }
            else
                ModelState.AddModelError("", res.Message!);

            KendoDataSourceResult returnResult =
                new(await new[] { medicalServiceDto }.ToDataSourceResultAsync(request, ModelState))
                {
                    UserMessage = res.Message
                };

            return Json(returnResult);
        }
    }
}
