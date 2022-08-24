using Application.Interfaces.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.ViewComponents.HotelFacilities
{
    public class HotelFacilitiesViewComponent : ViewComponent
    {
        private readonly IHotelApplication _hotelApplication;

        public HotelFacilitiesViewComponent(IHotelApplication hotelApplication)
        {
            _hotelApplication = hotelApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(int hotelId)
        {
            var data = await _hotelApplication.GetHotelSelectedFacilities(hotelId);
            return View(data);
        }
    }
}
