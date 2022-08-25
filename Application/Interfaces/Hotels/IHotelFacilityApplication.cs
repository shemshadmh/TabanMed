
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.Hotels.Hotels;

namespace Application.Interfaces.Hotels
{
    public interface IHotelFacilityApplication:IAsyncDisposable
    {
        Task<IReadOnlyList<HotelFacilityListItem>?> GetFacilityListAsync(int? parentId = null);
        Task<OperationResult> CreateFacility(HotelFacilityListItem facilityDto);
        Task<OperationResult> UpdateFacility(HotelFacilityListItem facilityDto);
        Task<IEnumerable<HotelFacilityForCheckBox>> GetHotelSelectedFacilities(int hotelId);
        Task<EditHotelFacilitiesDto> GetHotelFacilitiesForEdit(int hotelId);
        Task<OperationResult> EditHotelFacilities(EditHotelFacilitiesDto model);
    }
}
