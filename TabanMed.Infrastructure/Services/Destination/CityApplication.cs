using Application;
using Application.Dtos.Cities;
using Application.Interfaces.Application;
using Application.Interfaces.Destination;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabanMed.Infrastructure.Services.Destination
{
    public class CityApplication : ICityApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CityApplication> _logger;
        private readonly IMapper _mapper;
        public CityApplication(IApplicationDbContext dbContext,
            ILogger<CityApplication> logger,
             IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }


        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }

        public async Task<IReadOnlyList<CityListItem>?> GetCitiesListAsync(int CountryId)
        {
            try
            {
                var query = _dbContext.Cities.AsNoTracking()
                    .Include(cities => cities.CityTranslations)
                    .Where(cities => cities.CountryId == CountryId)
                    .Select(cities => new CityListItem()
                    {
                        Id = cities.Id,
                        CountryId = cities.CountryId,
                        FaName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.FaLanguageId)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        EnName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.EnLanguageId)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        ArName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.ArLanguageId)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        AfName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.AfLanguageId)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single()
                    });
                return await query.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get all cities list failed!");
                return null;
            }
        }









    }
}
