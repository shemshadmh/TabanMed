using System;

using Application.Dtos.MedicalCenters;

namespace Application.Interfaces.MedicalCenters
{
    public interface IMedicalCenterApplication
    {
        Task<IReadOnlyList<CityWithMedicalCenterCount>?> GetCitiesWithMedicalCenter();
        Task<CityWithMedicalCenterCount?> GetCityInformation(int cityId);
        Task<IReadOnlyList<MedicalCenterListItemDto>?> GetMedicalCenter(int cityId);
    }
}
