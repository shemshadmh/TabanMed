using Application;
using Application.Common;
using Application.Dtos.Countries;
using Application.Interfaces.Application;
using Application.Interfaces.Destination;
using Domain.Entities.Destination;
using Domain.Entities.Destination.Translation;
using Domain.Entities.Hotels.Translation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TabanMed.Infrastructure.Services.Destination
{
    public class CountryApplication : ICountryApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CountryApplication> _logger;
        private readonly IMapper _mapper;
        public CountryApplication(IApplicationDbContext dbContext,
            ILogger<CountryApplication> logger,
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

        public async Task<IReadOnlyList<CountryListItem>?> GetCountriesListAsync()
        {
            try
            {
                var query = _dbContext.Countries.AsNoTracking()
                    .Include(countries => countries.CountryTranslations)
                    .Select(countries => new CountryListItem()
                    {
                        Id = countries.Id,
                        FaName = countries.CountryTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.FaLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        EnName = countries.CountryTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.EnLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        ArName = countries.CountryTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.ArLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        AfName = countries.CountryTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.AfLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single()
                    });
                return await query.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get all countries list failed!");
                return null;
            }
        }

        public async Task<OperationResult> CreateCountry(CountryListItem countryDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.CountriesTranslation.AnyAsync(countryTranslation =>
                       countryTranslation.Name == countryDto.FaName ||
                       countryTranslation.Name == countryDto.EnName ||
                       countryTranslation.Name == countryDto.ArName ||
                       countryTranslation.Name == countryDto.AfName))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);


                var entity = await _mapper.From(countryDto).AdaptToTypeAsync<Country>();
                entity.CountryTranslations = new List<CountryTranslation>()
                {
                    new()
                    {
                        LanguageId = AppConstants.FaLanguageLcid,
                        Name = countryDto.FaName
                    },
                    new()
                    {
                        LanguageId = AppConstants.EnLanguageLcid,
                        Name = countryDto.EnName
                    },
                    new()
                    {
                        LanguageId = AppConstants.ArLanguageLcid,
                        Name = countryDto.ArName
                    },
                    new()
                    {
                        LanguageId = AppConstants.AfLanguageLcid,
                        Name = countryDto.AfName
                    },
                };

                await _dbContext.Countries.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New  country created successfully.[country:{title}]", countryDto.EnName);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullySaved);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not create new country.[country:{title}]", countryDto.EnName);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }


        public async Task<OperationResult> UpdateCountry(CountryListItem countryDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.CountriesTranslation.AnyAsync(countryTranslation =>
                       (countryTranslation.Name == countryDto.FaName ||
                       countryTranslation.Name == countryDto.EnName ||
                       countryTranslation.Name == countryDto.ArName ||
                       countryTranslation.Name == countryDto.AfName)
                        && countryTranslation.CountryId != countryDto.Id))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);
                var translationsList = await _dbContext.CountriesTranslation
                    .Where(countryTranslation => countryTranslation.CountryId == countryDto.Id)
                    .ToListAsync();

                foreach (var countryTranslation in translationsList)
                {
                    countryTranslation.Name= countryTranslation.LanguageId switch
                    {
                        AppConstants.FaLanguageLcid => countryDto.FaName,
                        AppConstants.EnLanguageLcid => countryDto.EnName,
                        AppConstants.ArLanguageLcid => countryDto.ArName,
                        AppConstants.AfLanguageLcid => countryDto.AfName,
                        _ => countryTranslation.Name
                    };
                }

                _dbContext.CountriesTranslation.UpdateRange(translationsList);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("country updated successfully.[country:{title}]", countryDto.EnName);

                return operation.Succeeded(countryDto.Id, InformationMessages.SuccessfullyUpdated);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update country.[country:{title}]", countryDto.EnName);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

    }
}
