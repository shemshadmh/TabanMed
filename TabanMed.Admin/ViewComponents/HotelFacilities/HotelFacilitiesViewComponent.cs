using Application.Interfaces.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.ViewComponents.HotelFacilities
{
    public class HotelFacilitiesViewComponent : ViewComponent
    {
        private readonly IHotelFacilityApplication _hotelFacilityApplication;

        public HotelFacilitiesViewComponent(IHotelFacilityApplication hotelFacilityApplication)
        {
            _hotelFacilityApplication = hotelFacilityApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(int hotelId)
        {
            var data = await _hotelFacilityApplication.GetHotelSelectedFacilities(hotelId);
            return View(data);
        }
    }
}
