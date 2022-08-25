using Application.Interfaces.MedicalCenters;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.ViewComponents.MedicalServices
{
    public class MedicalServicesViewComponent : ViewComponent
    {
        private readonly IMedicalServiceApplication _medicalServiceApplication;

        public MedicalServicesViewComponent(IMedicalServiceApplication medicalServiceApplication)
        {
            _medicalServiceApplication = medicalServiceApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(int medicalCenterId)
        {
            var data = await _medicalServiceApplication.GetSelectedMedicalService(medicalCenterId);
            return View(data);
        }
    }
}
