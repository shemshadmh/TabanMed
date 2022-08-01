
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;

namespace Application.Interfaces.Hotels
{
    public interface IHotelFacilityApplication:IAsyncDisposable
    {
        Task<IReadOnlyList<HotelFacilityListItem>?> GetFacilityListAsync(int? parentId = null);
        Task<OperationResult> CreateFacility(HotelFacilityListItem facilityDto);
        Task<OperationResult> CreateFacility(CreateHotelFacilityDto facilityDto);
        Task<OperationResult> UpdateFacility(HotelFacilityListItem facilityDto);
        Task<OperationResult> UpdateFacility(EditHotelFacilityDto facilityDto);
    }
}
