using Application.Interfaces.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace TabanMed.Admin.ViewComponents.HotelEditFacilities
{
    public class EditHotelFacilitiesViewComponent:ViewComponent
    {
        private readonly IHotelApplication _hotelApplication;

        public EditHotelFacilitiesViewComponent(IHotelApplication hotelApplication)
        {
            _hotelApplication = hotelApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync(int hotelId)
        {
            var data = await _hotelApplication.GetHotelFacilitiesForEdit(hotelId);
            return View(data);
        }
    }
}
