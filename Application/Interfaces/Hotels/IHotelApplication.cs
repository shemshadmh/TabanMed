
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.Hotels.Hotels;

namespace Application.Interfaces.Hotels
{
    public interface IHotelApplication:IAsyncDisposable
    {
        Task<IReadOnlyList<HotelListItemDto>?> GetHotels(int cityId);
        Task<OperationResult> CreateHotel(CreateHotelDto model);
        Task<OperationResult> EditHotel(EditHotelDto model);
        Task<OperationResult> DeleteHotel(DeleteHotelDto model);
        Task<HotelDetailsDto?> GetHotelDetails(int id);
        Task<IEnumerable<HotelToursDto>> GetHotelTours(int hotelId);
        Task<IEnumerable<HotelAlbumDto>> GetHotelAlbum(int hotelId);
        Task<OperationResult> AppendToHotelAlbum(AppendToHotelAlbumDto model);
        Task<OperationResult> RemoveFromHotelAlbum(RemoveFromHotelAlbumDto model);
        Task<EditHotelFacilitiesDto> GetHotelFacilitiesForEdit(int hotelId);
        Task<OperationResult> EditHotelFacilities(EditHotelFacilitiesDto model);
        Task<IEnumerable<HotelFacilityListItem>> GetHotelSelectedFacilities(int hotelId);
        Task<IReadOnlyList<CityWithHotelsCount>?> GetCitiesWithHotels();
        //Task<CityListItemDto?> GetCityInformation(int cityId);
    }
}
