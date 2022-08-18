
using Application;
using Application.Common;
using Application.Dtos.MedicalCenters;
using Application.Interfaces.Application;
using Application.Interfaces.MedicalCenters;
using Domain.Entities.MedicalCenters;
using Domain.Entities.MedicalCenters.Translation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;
using TabanMed.Infrastructure.Services.Hotels;


namespace TabanMed.Infrastructure.Services.MedicalCenters
{
    public  class MedicalCenterApplication: IMedicalCenterApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<HotelFacilityApplication> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentServices _currentServices;
        private readonly IFileManagerService _fileManagerService;
        public MedicalCenterApplication(IApplicationDbContext dbContext,
            ILogger<HotelFacilityApplication> logger,
            IMapper mapper,
            ICurrentServices currentServices,
            IFileManagerService fileManagerService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _currentServices = currentServices;
            _fileManagerService = fileManagerService;
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
                return await _dbContext.MedicalCenterTranslations.AsNoTracking()
                    .Where(medicalcenterTranslation => medicalcenterTranslation.LanguageId == _currentServices.LanguageId)
                    .Select(medicalcenterTranslation => new MedicalCenterListItemDto()
                    {
                        Id = medicalcenterTranslation.MedicalCenterId,
                        Name = medicalcenterTranslation.Name,
                        
                        ImageUrl = medicalcenterTranslation.MedicalCenter.ImageUrl
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get medical center list failed![cityId={CityId}]", cityId.ToString());
                return null;
            }
        }

        public async Task<OperationResult> CreateMedicalCenter(CreateMedicalCenterDto model)
        {
            var operation = new OperationResult();
            var savedPhotoPath = string.Empty;
            try
            {
                if (await _dbContext.MedicalCenterTranslations.AnyAsync(medicalCenterTranslation =>
                        medicalCenterTranslation.Name == model.Name
                        && medicalCenterTranslation.MedicalCenter.CityId == model.CityId))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                savedPhotoPath = await _fileManagerService
                    .SaveFileAsync(model.MedicalCenterPic, AppConstants.MedicalCentersPhotoPath, true, true);

                if (string.IsNullOrEmpty(savedPhotoPath))
                    return operation.Failed(ErrorMessages.CouldNotSavePic);

                var entity = await _mapper.From(model).AdaptToTypeAsync<MedicalCenter>();
                entity.ImageUrl = savedPhotoPath;

                entity.MedicalCenterTranslations = new List<MedicalCenterTranslation>()
                {
                    new ()
                    {
                        LanguageId = _currentServices.LanguageId,
                        Name = model.Name,
                        AgentName = model.AgentName,
                        Address = model.Address
                    }
                };

                await _dbContext.MedicalCenters.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New medical center created!");

                return operation.Succeeded(InformationMessages.SuccessfullySaved);
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(savedPhotoPath))
                    await _fileManagerService.DeleteFileAsync(savedPhotoPath);

                _logger.LogCritical(e, "Could not create medical center");
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }

        }

        public async Task<MedicalCenterDetailsDto?> GetMedicalCenterDetails(int id)
        {

            try
            {
                var query = await _dbContext.MedicalCenters.AsNoTracking()
                    .Include(medicalCenter => medicalCenter.MedicalCenterTranslations)
                    .Include((medicalCenter => medicalCenter.City))
                    .Where(medicalCenter => medicalCenter.Id == id)
                    .Select(medicalCenter => new MedicalCenterDetailsDto()
                    {
                        Id = medicalCenter.Id,
                        ImageUrl = medicalCenter.ImageUrl,
                        PhoneNumber = medicalCenter.PhoneNumber,
                        AgentPhoneNumber = medicalCenter.AgentPhoneNumber,
                        MedicalCenterTranslations = medicalCenter.MedicalCenterTranslations!
                            .Select(medicalCenterTranslation => new MedicalCenterTranslationsDto()
                            {
                                AgentName = medicalCenterTranslation.AgentName,
                                Name = medicalCenterTranslation.Name,
                                Address = medicalCenterTranslation.Address,
                                LanguageId = medicalCenterTranslation.LanguageId
                            })
                            .ToList()
                    }).SingleOrDefaultAsync();

                //query!.CityName = _dbContext.MedicalCenters.AsNoTracking()
                //    .Include(medicalCenter => medicalCenter.City)
                //    .FirstOrDefault((medicalCenter => medicalCenter.Id == id))!
                //    .City.CityTranslations!.FirstOrDefault(medicalCenter => medicalCenter.LanguageId == _currentServices.LanguageId)!.Name;

                return query;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get medical center Details failed![medicalcenterid={id}]", id.ToString());
                return null;
            }
        }


    }
}
