

using Application.Common;
using Application.Dtos.TourServices;

namespace Application.Interfaces.TourServices
{
    public interface ITourServiceApplication
    {
        Task<IReadOnlyList<TourServiceDetailesDto>?> GetTourServices();
        Task<OperationResult> CreateMedicalService(TourServicesForEditDto createTourServiceDto);
        Task<TourServicesForEditDto> GetTourServiceDetails(int tourServiceId);
        Task<OperationResult> EditTourServiceAsync(TourServicesForEditDto model);
    }
}
