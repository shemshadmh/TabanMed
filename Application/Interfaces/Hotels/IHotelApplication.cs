
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.Hotels.Hotels;

namespace Application.Interfaces.Hotels
{
    public interface IHotelApplication:IAsyncDisposable
    {
        Task<IReadOnlyList<HotelListItemDto>?> GetHotels(int cityId);
        Task<OperationResult> CreateHotel(CreateHotelDto model);
        Task<IReadOnlyList<CityWithHotelsCount>?> GetCitiesWithHotels();
        Task<CityWithHotelsCount?> GetCityInformation(int cityId);
        Task<HotelDetailsDto?> GetHotelDetails(int id);
        Task<HotelTranslationForEditDto?> GetHotelForEditAsync(int hotelId, int languageId);
        Task<OperationResult> EditHotelAsync(HotelTranslationForEditDto hotelTranslationDto);

        Task<HotelDetailsBasicsDto?> GetHotelBasicsDetails(int id);

        Task<OperationResult> EditHotelBasicsAsync(HotelDetailsBasicsDto model);

    }
}
