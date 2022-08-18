using System;

using Application.Dtos.MedicalCenters;
using Application.Interfaces.Application;
using Application.Interfaces.MedicalCenters;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TabanMed.Infrastructure.Services.Hotels;

namespace TabanMed.Infrastructure.Services.MedicalCenter
{
    public  class MedicalCenterApplication: IMedicalCenterApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<HotelFacilityApplication> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentServices _currentServices;
        public MedicalCenterApplication(IApplicationDbContext dbContext,
            ILogger<HotelFacilityApplication> logger,
            IMapper mapper,
            ICurrentServices currentServices)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _currentServices = currentServices;
        }


        public async Task<IReadOnlyList<CityWithMedicalCenterCount>?> GetCitiesWithMedicalCenter()
        {

            try
            {
                return await _dbContext.Cities.AsNoTracking()
                    .Select(city => new CityWithMedicalCenterCount()
                    {
                        CityId = city.Id,
                        MedicalCenterCount = city.MedicalCenters!.Count,
                        CityName = city.CityTranslations!
                            .First(cityTranslation
                                => cityTranslation.LanguageId == _currentServices.LanguageId).Name

                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Could not get cities with Medical Center count!!");
                return null;
            }
        }

        public async Task<CityWithMedicalCenterCount?> GetCityInformation(int cityId)
        {
            try
            {
                return await _dbContext.Cities.AsNoTracking()
                    .Where(city => city.Id == cityId)
                    .Select(city => new CityWithMedicalCenterCount()
                    {
                        CityId = city.Id,
                        MedicalCenterCount = city.MedicalCenters!.Count,
                        CityName = city.CityTranslations!
                            .First(cityTranslation
                                => cityTranslation.LanguageId == _currentServices.LanguageId).Name

                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Could not get city informations.[cityId={cityId}]", cityId);
                return null;
            }
        }



        public async Task<IReadOnlyList<MedicalCenterListItemDto>?> GetMedicalCenter(int cityId)
        {
            try
            {
                var query = _dbContext.MedicalCenters
                    .Where(medicalcenter => medicalcenter.CityId == cityId);

                return await _mapper.From(query).AdaptToTypeAsync<List<MedicalCenterListItemDto>>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get medical center list failed![cityId={CityId}]", cityId.ToString());
                return null;
            }
        }
        
    }
}
