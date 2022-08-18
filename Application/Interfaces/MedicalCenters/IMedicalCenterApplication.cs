using System;
using Application.Common;
using Application.Dtos.MedicalCenters;

namespace Application.Interfaces.MedicalCenters
{
    public interface IMedicalCenterApplication
    {
        Task<IReadOnlyList<CityWithMedicalCenterCount>?> GetCitiesWithMedicalCenter();
        Task<CityWithMedicalCenterCount?> GetCityInformation(int cityId);
        Task<IReadOnlyList<MedicalCenterListItemDto>?> GetMedicalCenter(int cityId);
        Task<OperationResult> CreateMedicalCenter(CreateMedicalCenterDto model);
        Task<MedicalCenterDetailsDto?> GetMedicalCenterDetails(int id);
    }
}
