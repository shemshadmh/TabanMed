﻿using Application;
using Application.Common;
using Application.Dtos.Cities;
using Application.Interfaces.Application;
using Application.Interfaces.Destination;
using Domain.Entities.Destination;
using Domain.Entities.Destination.Translation;
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
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.FaLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        EnName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.EnLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        ArName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.ArLanguageLcid)
                            .Select(countryTranslation => countryTranslation.Name)
                            .Single(),
                        AfName = cities.CityTranslations!
                            .Where(countryTranslation => countryTranslation.LanguageId == AppConstants.AfLanguageLcid)
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



        public async Task<OperationResult> CreateCity(CityListItem cityDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.CityTranslations.AnyAsync(cityTranslation =>
                       cityTranslation.Name == cityDto.FaName ||
                       cityTranslation.Name == cityDto.EnName ||
                       cityTranslation.Name == cityDto.ArName ||
                       cityTranslation.Name == cityDto.AfName))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(cityDto).AdaptToTypeAsync<City>();
                entity.CountryId = cityDto.CountryId;                    
                entity.CityTranslations= new List<CityTranslation>()
                {
                    new()
                    {
                        LanguageId = AppConstants.FaLanguageLcid,
                        Name = cityDto.FaName
                    },
                    new()
                    {
                        LanguageId = AppConstants.EnLanguageLcid,
                        Name = cityDto.EnName
                    },
                    new()
                    {
                        LanguageId = AppConstants.ArLanguageLcid,
                        Name = cityDto.ArName
                    },
                    new()
                    {
                        LanguageId = AppConstants.AfLanguageLcid,
                        Name = cityDto.AfName
                    }
                };

                await _dbContext.Cities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New city created successfully.[city:{title}]", cityDto.EnName);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullySaved);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not create new city .[city:{title}]", cityDto.EnName);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }


        public async Task<OperationResult> UpdateCity(CityListItem cityDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.CityTranslations.AnyAsync(cityTranslation =>
                       (cityTranslation.Name == cityDto.FaName ||
                       cityTranslation.Name == cityDto.EnName ||
                       cityTranslation.Name == cityDto.ArName ||
                       cityTranslation.Name == cityDto.AfName)
                       && cityTranslation.CityId != cityDto.Id))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var translationsList = await _dbContext.CityTranslations
                    .Where(cityTranslation => cityTranslation.CityId == cityDto.Id)
                    .ToListAsync();

                foreach (var cityTranslation in translationsList)
                {
                    cityTranslation.Name = cityTranslation.LanguageId switch
                    {
                        AppConstants.FaLanguageLcid => cityDto.FaName,
                        AppConstants.EnLanguageLcid => cityDto.EnName,
                        AppConstants.ArLanguageLcid => cityDto.ArName,
                        AppConstants.AfLanguageLcid => cityDto.AfName,
                        _ => cityTranslation.Name
                    };
                }

                _dbContext.CityTranslations.UpdateRange(translationsList);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("City updated successfully.[city:{title}]", cityDto.EnName);

                return operation.Succeeded(cityDto.Id, InformationMessages.SuccessfullyUpdated);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update city.[city:{title}]", cityDto.EnName);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }
    }
}
