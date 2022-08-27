

using Application.Common;
using Application.Dtos.TourServices;

namespace Application.Interfaces.TourServices
{
    public interface ITourServiceApplication
    {
        Task<IReadOnlyList<TourServiceListItemDto>?> GetTourServicesAsync();
        Task<OperationResult> CreateTourServiceAsync(CreateTourServicesDto createTourServiceDto);
        Task<EditTourServicesDto?> GetTourServiceDetailsAsync(int tourServiceId);
        Task<OperationResult> EditTourServiceAsync(EditTourServicesDto model);
    }
}
