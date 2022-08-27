

using Application.Common;
using Application.Dtos.TourServices;

namespace Application.Interfaces.TourServices
{
    public interface ITourServiceApplication
    {
        Task<IReadOnlyList<TourServiceDetailesDto>?> GetTourServicesAsync();
        Task<OperationResult> CreateTourServiceAsync(TourServicesForEditDto createTourServiceDto);
        Task<TourServicesForEditDto> GetTourServiceDetailsAsync(int tourServiceId);
        Task<OperationResult> EditTourServiceAsync(TourServicesForEditDto model);
    }
}
