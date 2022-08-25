using Application.Interfaces.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.ViewComponents.HotelEditFacilities
{
    public class EditHotelFacilitiesViewComponent:ViewComponent
    {
        private readonly IHotelFacilityApplication _hotelFacilityApplication;

        public EditHotelFacilitiesViewComponent(IHotelFacilityApplication hotelFacilityApplication)
        {
            _hotelFacilityApplication = hotelFacilityApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(int hotelId)
        {
            var data = await _hotelFacilityApplication.GetHotelFacilitiesForEdit(hotelId);
            return View(data);
        }
    }
}
