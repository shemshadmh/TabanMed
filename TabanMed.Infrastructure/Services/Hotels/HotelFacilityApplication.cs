
using Application;
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.Hotels.Hotels;
using Application.Interfaces.Application;
using Application.Interfaces.Hotels;
using Domain.Entities.Hotels;
using Domain.Entities.Hotels.Translation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;

namespace TabanMed.Infrastructure.Services.Hotels
{
    public class HotelFacilityApplication : IHotelFacilityApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<HotelFacilityApplication> _logger;
        private readonly IMapper _mapper;
        private readonly string _twoLetterISOLanguageName;
        private readonly ICurrentServices _currentServices;
        public HotelFacilityApplication(IApplicationDbContext dbContext,
            ILogger<HotelFacilityApplication> logger,
            IMapper mapper,
            ICurrentServices currentServices)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _twoLetterISOLanguageName = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            _currentServices = currentServices;
        }

        public async Task<IReadOnlyList<HotelFacilityListItem>?> GetFacilityListAsync(int? parentId = null)
        {
            try
            {
                var query = _dbContext.HotelFacilities.AsNoTracking()
                    .Include(hotelFacility => hotelFacility.HotelFacilityTranslations)
                    .Where(hotelFacility => hotelFacility.ParentId == parentId)
                    .Select(hotelFacility => new HotelFacilityListItem()
                    {
                        Id = hotelFacility.Id,
                        ParentId = hotelFacility.ParentId,
                        FaTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.FaLanguageLcid)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),
                        EnTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.EnLanguageLcid)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),
                        ArTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.ArLanguageLcid)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),
                        AfTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.AfLanguageLcid)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single()
                    });

                return await query.ToListAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Get all countries list failed!");
                return null;
            }
        }

        public async Task<OperationResult> CreateFacility(HotelFacilityListItem facilityDto)
        {
            var operation = new OperationResult();
            try
            {
                if(await _dbContext.HotelFacilityTranslations.AnyAsync(facilityTranslation =>
                       facilityTranslation.Title == facilityDto.FaTitle ||
                       facilityTranslation.Title == facilityDto.EnTitle ||
                       facilityTranslation.Title == facilityDto.ArTitle ||
                       facilityTranslation.Title == facilityDto.AfTitle))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(facilityDto).AdaptToTypeAsync<HotelFacility>();

                entity.HotelFacilityTranslations = new List<HotelFacilityTranslation>()
                {
                    new()
                    {
                        LanguageId = AppConstants.FaLanguageLcid,
                        Title = facilityDto.FaTitle
                        
                    },
                    new()
                    {
                        LanguageId = AppConstants.EnLanguageLcid,
                        Title = facilityDto.EnTitle
                    },
                    new()
                    {
                        LanguageId = AppConstants.ArLanguageLcid,
                        Title = facilityDto.ArTitle
                    },
                    new()
                    {
                        LanguageId = AppConstants.AfLanguageLcid,
                        Title = facilityDto.AfTitle
                    }
                };

                await _dbContext.HotelFacilities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New hotel facility created successfully.[facility:{title}]", facilityDto.EnTitle);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullySaved);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Could not create new hotel facility.[facility:{title}]", facilityDto.EnTitle);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public async Task<OperationResult> UpdateFacility(HotelFacilityListItem facilityDto)
        {
            var operation = new OperationResult();
            try
            {
                if(await _dbContext.HotelFacilityTranslations.AnyAsync(facilityTranslation =>
                       (facilityTranslation.Title == facilityDto.FaTitle ||
                       facilityTranslation.Title == facilityDto.EnTitle ||
                       facilityTranslation.Title == facilityDto.ArTitle ||
                       facilityTranslation.Title == facilityDto.AfTitle)
                       && facilityTranslation.FacilityId != facilityDto.Id))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var translationsList = await _dbContext.HotelFacilityTranslations
                    .Where(facilityTranslation => facilityTranslation.FacilityId == facilityDto.Id)
                    .ToListAsync();

                foreach (var facilityTranslation in translationsList)
                {
                    facilityTranslation.Title = facilityTranslation.LanguageId switch
                    {
                        AppConstants.FaLanguageLcid => facilityDto.FaTitle,
                        AppConstants.EnLanguageLcid => facilityDto.EnTitle,
                        AppConstants.ArLanguageLcid => facilityDto.ArTitle,
                        AppConstants.AfLanguageLcid => facilityDto.AfTitle,
                        _ => facilityTranslation.Title
                    };
                }

                _dbContext.HotelFacilityTranslations.UpdateRange(translationsList);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("Hotel facility updated successfully.[facility:{title}]", facilityDto.EnTitle);

                return operation.Succeeded(facilityDto.Id, InformationMessages.SuccessfullyUpdated);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Could not update hotel facility.[facility:{title}]", facilityDto.EnTitle);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public async Task<IEnumerable<HotelFacilityForCheckBox>> GetHotelSelectedFacilities(int hotelId)
        {
            try
            {
                var result = new List<HotelFacilityForCheckBox>();

                var parentFacilities = await _dbContext.HotelFacilities
                    .Where(facility => facility.ParentId == null)
                    .Select(facility => new HotelFacilityForCheckBox()
                    {
                        Title = facility.HotelFacilityTranslations!
                                    .Where(facilityTranslation => facilityTranslation.LanguageId == _currentServices.LanguageId)
                                    .Select(facilityTranslation => facilityTranslation.Title)
                                    .Single(),

                        Value = facility.HotelFacilityTranslations!
                                    .Where(facilityTranslation => facilityTranslation.LanguageId == _currentServices.LanguageId)
                                    .Select(facilityTranslation => facilityTranslation.FacilityId)
                                    .Single(),

                        ParentId = facility.ParentId

                    })
                    .ToListAsync();

                var selectedFacilitiesQuery = await _dbContext.HotelSelectedFacilities
                    .Include(selectedFacility => selectedFacility.HotelFacility)
                    .Where(selectedFacility => selectedFacility.HotelId == hotelId)
                    .Select(facility => new HotelFacilityForCheckBox()
                    {
                        Title = facility.HotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == _currentServices.LanguageId)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),

                        Value = facility.HotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == _currentServices.LanguageId)
                            .Select(facilityTranslation => facilityTranslation.FacilityId)
                            .Single(),

                        ParentId = facility.HotelFacility.ParentId

                    })
                    .ToListAsync();

                result.AddRange(parentFacilities);
                result.AddRange(selectedFacilitiesQuery);
                return result;
            }

            catch (Exception e)
            {
                _logger.LogCritical(e, "Could not GetHotelSelectedFacilities[hotelId={}]", hotelId);
                return null;
            }

        }

        public async Task<EditHotelFacilitiesDto> GetHotelFacilitiesForEdit(int hotelId)
        {
            try
            {
                var allHotelFacilities = await _dbContext.HotelFacilities.AsNoTracking()
                .Include(hotelFacility => hotelFacility.HotelFacilityTranslations)
                .Select(hotelFacilityTranslation => new HotelFacilityForCheckBox()
                {

                    Title = hotelFacilityTranslation.HotelFacilityTranslations!
                        .Where(facilityTranslation => facilityTranslation.LanguageId == _currentServices.LanguageId)
                        .Select(facilityTranslation => facilityTranslation.Title)
                        .Single(),

                    Value = hotelFacilityTranslation.HotelFacilityTranslations!
                        .Where(facilityTranslation => facilityTranslation.LanguageId == _currentServices.LanguageId)
                        .Select(facilityTranslation => facilityTranslation.FacilityId)
                        .Single(),

                    ParentId = hotelFacilityTranslation.ParentId

                })
                .ToListAsync();

                var selectedFacilitiesByHotel = await _dbContext.HotelSelectedFacilities
                    .Include(selectedFacility => selectedFacility.HotelFacility)
                    .Where(selectedFacility => selectedFacility.HotelId == hotelId)
                    .Select(facility => facility.HotelFacilityId)
                    .ToListAsync();

                var facilityListForCheckBox = allHotelFacilities
                    .Select(facility => new HotelFacilityForCheckBox()
                    {
                        Title = facility.Title,
                        Value = facility.Value,
                        IsSelected = selectedFacilitiesByHotel != null && selectedFacilitiesByHotel.Contains(facility.Value),
                        ParentId = facility.ParentId,

                    }).ToList();
                var res = new EditHotelFacilitiesDto
                {
                    AllFacilities = facilityListForCheckBox,
                    HotelId = hotelId
                };
                return res;
            }

            catch (Exception e)
            {
                _logger.LogCritical(e, "Could not GetHotelFacilitiesForEdit [hotelId={}]", hotelId);
                return null;
            }


        }


        public async Task<OperationResult> EditHotelFacilities(EditHotelFacilitiesDto model)
        {

            var operation = new OperationResult();
            try
            {
                var listToRemove = _dbContext.HotelSelectedFacilities.AsNoTracking()
                    .Where(facility => facility.HotelId == model.HotelId);
                if (await listToRemove.AnyAsync())
                {
                    _dbContext.HotelSelectedFacilities.RemoveRange(listToRemove);
                    await _dbContext.SaveChangesAsync();
                }

                foreach (var facilityId in model.SelectedHotelFacilities)
                {
                    await _dbContext.HotelSelectedFacilities
                        .AddAsync(new HotelSelectedFacility()
                        {
                            HotelId = model.HotelId,
                            HotelFacilityId = facilityId
                        });
                }
                await _dbContext.SaveChangesAsync();

                return operation.Succeeded();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "could not edit hotel facilities");
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
