
using Application;
using Application.Common;
using Application.Dtos.Hotels.Hotels;
using Application.Dtos.MedicalCenters;
using Application.Interfaces.Application;
using Application.Interfaces.MedicalCenters;
using Domain.Entities.Hotels;
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
    public class MedicalCenterApplication : IMedicalCenterApplication
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
                    .Include(medicalCenterTranslation=> medicalCenterTranslation.MedicalCenter)
                    .Where(medicalCenterTranslation =>
                        medicalCenterTranslation.LanguageId == _currentServices.LanguageId
                        && medicalCenterTranslation.MedicalCenter.CityId == cityId)
                    .Select(medicalCenterTranslation => new MedicalCenterListItemDto()
                    {
                        Id = medicalCenterTranslation.MedicalCenterId,
                        Name = medicalCenterTranslation.Name,

                        ImageUrl = medicalCenterTranslation.MedicalCenter.ImageUrl
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
                    new()
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
                var medicalCenterDetailsDto = await _dbContext.MedicalCenters.AsNoTracking()
                    //.Include(medicalCenter => medicalCenter.MedicalCenterTranslations)
                    .Where(medicalCenter => medicalCenter.Id == id)
                    .Select(medicalCenter => new MedicalCenterDetailsDto()
                    {
                        Id = medicalCenter.Id,
                        ImageUrl = Path.Combine(Path.AltDirectorySeparatorChar.ToString(), medicalCenter.ImageUrl),
                        PhoneNumber = medicalCenter.PhoneNumber,
                        AgentPhoneNumber = medicalCenter.AgentPhoneNumber,
                        CityId = medicalCenter.CityId,
                        MedicalCenterForEditDetailsDto = medicalCenter.MedicalCenterTranslations!
                            .Select(medicalCenterTranslation => new MedicalCenterForEditDetailsDto()
                            {
                                AgentName = medicalCenterTranslation.AgentName,
                                Name = medicalCenterTranslation.Name,
                                Address = medicalCenterTranslation.Address,
                                LanguageId = medicalCenterTranslation.LanguageId
                            })
                            .ToList()
                    }).SingleOrDefaultAsync();

                medicalCenterDetailsDto!.CityName= _dbContext.CityTranslations.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CityId == medicalCenterDetailsDto.CityId
                                              && x.LanguageId== _currentServices.LanguageId)!.Result!.Name;

                return medicalCenterDetailsDto;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get medical center Details failed![medicalCenterId={id}]", id.ToString());
                return null;
            }
        }

        public async Task<MedicalCenterForEditDetailsDto?> GetMedicalCenterForEditAsync(int medicalCenterId, int langId)
        {
            try
            {
                if (!await _dbContext.MedicalCenters.AsNoTracking()
                        .AnyAsync(medicalCenter => medicalCenter.Id == medicalCenterId))
                {
                    return null;
                }

                var medicalCenterDto = await _dbContext.MedicalCenterTranslations.AsNoTracking()
                    .Where(medicalTranslation => medicalTranslation.MedicalCenterId == medicalCenterId
                                                 && medicalTranslation.LanguageId == langId)
                    .Select(medicalTranslation => new MedicalCenterForEditDetailsDto()
                    {
                        MedicalCenterId = medicalCenterId,
                        LanguageName = langId.GetLanguageName(),
                        LanguageId = langId,
                        Name = medicalTranslation.Name,
                        Address = medicalTranslation.Address,
                        AgentName = medicalTranslation.AgentName
                    })
                    .SingleOrDefaultAsync();
                if (medicalCenterDto is null)
                    return new MedicalCenterForEditDetailsDto()
                    {
                        MedicalCenterId = medicalCenterId,
                        LanguageName = langId.GetLanguageName(),
                        LanguageId = langId,
                        Name = string.Empty,
                        Address = string.Empty,
                        AgentName = string.Empty
                    };

                return medicalCenterDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get medical center Details failed![medicalcenterid={id}]",
                    medicalCenterId.ToString());
                return null;
            }
        }
        
        public async Task<OperationResult> EditMedicalCenterAsync(MedicalCenterForEditDetailsDto medicalCenterDto)
        {
            var operation = new OperationResult();
            try
            {
                var medicalCenter = await _dbContext.MedicalCenters.AsNoTracking()
                    .FirstOrDefaultAsync(medicalCenter =>
                        medicalCenter.Id == medicalCenterDto.MedicalCenterId);

                // check for exist
                if (medicalCenter is null)
                    return operation.Failed(ErrorMessages.ItemNotFound);

                var medicalCenterTranslation = await _dbContext.MedicalCenterTranslations.AsNoTracking()
                    .FirstOrDefaultAsync(medicalCenterTranslation =>
                        medicalCenterTranslation.LanguageId ==
                        medicalCenterDto.LanguageId 
                        && medicalCenterTranslation.MedicalCenterId== medicalCenterDto.MedicalCenterId);
                //if medicalCenterTranslation null ==> create it
                if (medicalCenterTranslation is null)
                {
                    var newMedicalCenterTranslation = new MedicalCenterTranslation
                    {
                        LanguageId = medicalCenterDto.LanguageId,
                        Name = medicalCenterDto.Name,
                        AgentName = medicalCenterDto.AgentName,
                        Address = medicalCenterDto.Address,
                        MedicalCenterId = medicalCenterDto.MedicalCenterId
                    };

                    await _dbContext.MedicalCenterTranslations.AddAsync(newMedicalCenterTranslation);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    medicalCenterTranslation.Name = medicalCenterDto.Name;
                    medicalCenterTranslation.AgentName = medicalCenterDto.AgentName;
                    medicalCenterTranslation.Address = medicalCenterDto.Address;

                    _dbContext.MedicalCenterTranslations.Update(medicalCenterTranslation);
                    await _dbContext.SaveChangesAsync();
                }

                return operation.Succeeded(InformationMessages.SuccessfullyUpdated);
            }
            catch
            {
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }

        }

        public async Task<MedicalCenterDetailsBasicsDto?> GetMedicalCenterBasicsDetails(int id)
        {
            try
            {
                return await _dbContext.MedicalCenters.AsNoTracking()
                    .Where(medicalCenter => medicalCenter.Id == id)
                    .Select(medicalCenter => new MedicalCenterDetailsBasicsDto()
                    {
                        Id = medicalCenter.Id,
                        PhoneNumber = medicalCenter.PhoneNumber,
                        AgentPhoneNumber = medicalCenter.AgentPhoneNumber,
                        ImageUrl = Path.Combine(Path.AltDirectorySeparatorChar.ToString(), medicalCenter.ImageUrl)
                    })
                    .SingleOrDefaultAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Get medical center basics details failed![medicalCenterId={id}]", id.ToString());
                return null;
            }
        }

        public async Task<OperationResult> EditMedicalCenterBasicsAsync(MedicalCenterDetailsBasicsDto model)
        {
            var operation = new OperationResult();
            var savedPhotoPath = string.Empty;
            try
            {
                var medicalCenter = await _dbContext.MedicalCenters.FindAsync(model.Id);

                if(medicalCenter is null)
                    return operation.Failed(ErrorMessages.MedicalCenterNotFound);

                if (model.MedicalCenterPic is not null)
                {
                    // Save new image file
                    savedPhotoPath = await _fileManagerService
                        .SaveFileAsync(model.MedicalCenterPic, AppConstants.MedicalCentersPhotoPath, true, true);

                    if(string.IsNullOrEmpty(savedPhotoPath))
                        return operation.Failed(ErrorMessages.CouldNotSavePic);

                    // Delete previous image
                    var deleteResult =
                        await _fileManagerService.DeleteFileAsync(
                            Path.Combine(AppConstants.RootFilesPath, medicalCenter.ImageUrl),true);
                    if(!deleteResult)
                        return operation.Failed(ErrorMessages.CouldNotRemovePic);

                    medicalCenter.ImageUrl = savedPhotoPath;
                }

                medicalCenter.AgentPhoneNumber = model.AgentPhoneNumber;
                medicalCenter.PhoneNumber = model.PhoneNumber;

                _dbContext.MedicalCenters.Update(medicalCenter);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("Medical center base information updated![medicalCenterId={}]",medicalCenter.Id);

                return operation.Succeeded(InformationMessages.SuccessfullyUpdated);
            }
            catch(Exception e)
            {
                if(!string.IsNullOrEmpty(savedPhotoPath))
                    await _fileManagerService.DeleteFileAsync(savedPhotoPath);

                _logger.LogCritical(e, "Could not update medical center[medicalCenterId={}]", model.Id);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }
    }
}


