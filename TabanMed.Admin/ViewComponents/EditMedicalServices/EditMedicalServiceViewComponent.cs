using Application.Interfaces.MedicalCenters;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.ViewComponents.EditMedicalServices
{
    public class EditMedicalServiceViewComponent : ViewComponent
    {
        private readonly IMedicalServiceApplication _medicalServiceApplication;

        public EditMedicalServiceViewComponent(IMedicalServiceApplication medicalServiceApplication)
        {
            _medicalServiceApplication = medicalServiceApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(int medicalCenterId)
        {
            var data = await _medicalServiceApplication.GetMedicalServiceForEdit(medicalCenterId);
            return View(data);
        }
    }
}
