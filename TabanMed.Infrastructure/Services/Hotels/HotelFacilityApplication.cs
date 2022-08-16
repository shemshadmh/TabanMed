
using Application;
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
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

        public HotelFacilityApplication(IApplicationDbContext dbContext,
            ILogger<HotelFacilityApplication> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _twoLetterISOLanguageName = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
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
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.FaLanguageId)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),
                        EnTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.EnLanguageId)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),
                        ArTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.ArLanguageId)
                            .Select(facilityTranslation => facilityTranslation.Title)
                            .Single(),
                        AfTitle = hotelFacility.HotelFacilityTranslations!
                            .Where(facilityTranslation => facilityTranslation.LanguageId == AppConstants.AfLanguageId)
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
                        LanguageId = AppConstants.FaLanguageId,
                        Title = facilityDto.FaTitle
                        
                    },
                    new()
                    {
                        LanguageId = AppConstants.EnLanguageId,
                        Title = facilityDto.EnTitle
                    },
                    new()
                    {
                        LanguageId = AppConstants.ArLanguageId,
                        Title = facilityDto.ArTitle
                    },
                    new()
                    {
                        LanguageId = AppConstants.AfLanguageId,
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
                        AppConstants.FaLanguageId => facilityDto.FaTitle,
                        AppConstants.EnLanguageId => facilityDto.EnTitle,
                        AppConstants.ArLanguageId => facilityDto.ArTitle,
                        AppConstants.AfLanguageId => facilityDto.AfTitle,
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

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
