
using Application;
using Application.Common;
using Application.Dtos.MedicalCenters;
using Application.Dtos.TourServices;
using Application.Interfaces.Application;
using Application.Interfaces.TourServices;
using Domain.Entities.Hotels.Translation;
using Domain.Entities.MedicalCenters.Translation;
using Domain.Entities.TourServices;
using Domain.Entities.TourServices.Translation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;
using TabanMed.Infrastructure.Services.Hotels;

namespace TabanMed.Infrastructure.Services.TourServices
{
    public class TourServiceApplication: ITourServiceApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<TourServiceApplication> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentServices _currentServices;
        public TourServiceApplication(IApplicationDbContext dbContext,
            ILogger<TourServiceApplication> logger,
            IMapper mapper,
            ICurrentServices currentServices)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _currentServices = currentServices;
        }
        public async Task<IReadOnlyList<TourServiceDetailesDto>?> GetTourServices()
        {
            try
            {
                return await _dbContext.TourServiceTranslations.AsNoTracking()
                    .Include(tourServiceTranslation=> tourServiceTranslation.TourService)
                    .Where(tourServiceTranslation =>
                        tourServiceTranslation.LanguageId == _currentServices.LanguageId)
                    .Select(tourServiceTranslation => new TourServiceDetailesDto()
                    {
                        Id = tourServiceTranslation.TourServiceId,
                        Title = tourServiceTranslation.Title,
                        Price = tourServiceTranslation.TourService.Price,
                        Description = tourServiceTranslation.Description
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get Tour Service list failed![tourServiceId={}]", 0);
                return null;
            }
        }

        public async Task<OperationResult> CreateMedicalService(TourServicesForEditDto createTourServiceDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.TourServiceTranslations.AnyAsync(tourServiceTranslation =>
                        tourServiceTranslation.Title == createTourServiceDto.FaTitle))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(createTourServiceDto).AdaptToTypeAsync<TourService>();

                entity.TourServiceTranslation = new List<TourServiceTranslation>()
                {
                    new()
                    {
                        LanguageId = AppConstants.FaLanguageLcid,
                        Title = createTourServiceDto.FaTitle,
                        Description = createTourServiceDto.FaDescription,
                    },
                    new()
                    {
                        LanguageId = AppConstants.EnLanguageLcid,
                        Title = createTourServiceDto.EnTitle,
                        Description = createTourServiceDto.EnDescription,
                    },
                    new()
                    {
                        LanguageId = AppConstants.ArLanguageLcid,
                        Title = createTourServiceDto.ArTitle,
                        Description = createTourServiceDto.ArDescription,
                    },
                    new()
                    {
                        LanguageId = AppConstants.AfLanguageLcid,
                        Title = createTourServiceDto.AfTitle,
                        Description = createTourServiceDto.AfDescription,
                    }
                };
                entity.Price = createTourServiceDto.Price;

                await _dbContext.TourServices.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New tour service created successfully.[tourService:{title}]", createTourServiceDto.FaTitle);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullySaved);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not create newtour service.[tourService:{title}]", createTourServiceDto.FaTitle);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public async Task<TourServicesForEditDto> GetTourServiceDetails(int tourServiceId)
        {
            try
            {
                if (await _dbContext.TourServices.AnyAsync(tourService =>
                        tourService.Id == tourServiceId)==false)
                    return  new TourServicesForEditDto() { };

                return await _dbContext.TourServices.AsNoTracking()
                    .Include(tourService => tourService.TourServiceTranslation)
                    .Where(tourService => tourService.Id == tourServiceId)
                    .Select(tourService => new TourServicesForEditDto()
                    {
                        Id = tourService.Id,
                        Price = tourService.Price,
                        FaTitle = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.FaLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Title)
                            .Single(),
                        FaDescription = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.FaLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Description)
                            .Single(),
                        EnTitle = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.EnLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Title)
                            .Single(),
                        EnDescription = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.EnLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Description)
                            .Single(),
                        ArTitle = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.ArLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Title)
                            .Single(),
                        ArDescription = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.ArLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Description)
                            .Single(),
                        AfTitle = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.AfLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Title)
                            .Single(),
                        AfDescription = tourService.TourServiceTranslation!
                            .Where(tourServiceTranslation => tourServiceTranslation.LanguageId == AppConstants.AfLanguageLcid)
                            .Select(tourServiceTranslation => tourServiceTranslation.Description)
                            .Single()

                    }).SingleAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get Tour Service list failed![tourServiceId={}]", 0);
                return new TourServicesForEditDto() { };
            }
        }

        public async Task<OperationResult> EditTourServiceAsync(TourServicesForEditDto model)
        {
            var operation = new OperationResult();
            try
            {
                var tourService = await _dbContext.TourServices.AsNoTracking()
                    .Include(tourServices=> tourServices.TourServiceTranslation)
                    .FirstOrDefaultAsync(tourService =>
                        tourService.Id == model.Id);

                // check for exist
                if (tourService is null)
                    return operation.Failed(ErrorMessages.ItemNotFound);

                #region Update prop
              
                tourService.Price = model.Price;
                
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.FaLanguageLcid)!.Title = model.FaTitle;
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.FaLanguageLcid)!.Description = model.FaDescription;
               
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.EnLanguageLcid)!.Title = model.EnTitle;
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.EnLanguageLcid)!.Description = model.EnDescription;
                 
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.ArLanguageLcid)!.Title = model.ArTitle;
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.ArLanguageLcid)!.Description = model.ArDescription;
                
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.AfLanguageLcid)!.Title = model.AfTitle;
                tourService.TourServiceTranslation!
                    .FirstOrDefault(x =>
                        x.LanguageId == AppConstants.AfLanguageLcid)!.Description = model.AfDescription;
                #endregion

                _dbContext.TourServices.Update(tourService);
                
                await _dbContext.SaveChangesAsync();

                return operation.Succeeded(InformationMessages.SuccessfullyUpdated);
            }
            catch
            {
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }

        }

    }
}
