
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Resources.ErrorMessages;
using Resources.InformationMessages;

namespace TabanMed.Infrastructure.Services.Hotels
{
    public class HotelApplication : IHotelApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<HotelApplication> _logger;
        private readonly IFileManagerService _fileManagerService;
        private readonly IMapper _mapper;
        private readonly ICurrentServices _currentServices;

        public HotelApplication(IApplicationDbContext dbContext,
            ILogger<HotelApplication> logger,
            IFileManagerService fileManagerService,
            IMapper mapper,
            ICurrentServices currentServices)
        {
            _dbContext = dbContext;
            _logger = logger;
            _fileManagerService = fileManagerService;
            _mapper = mapper;
            _currentServices = currentServices;
        }

        public async Task<IReadOnlyList<HotelListItemDto>?> GetHotels(int cityId)
        {
            try
            {
                return await _dbContext.HotelTranslations.AsNoTracking()
                    .Include(hotelTranslation=> hotelTranslation.Hotel)
                    .Where(hotelTranslation =>
                        hotelTranslation.LanguageId == _currentServices.LanguageId
                        &&hotelTranslation.Hotel.CityId == cityId)
                    .Select(hotelTranslation => new HotelListItemDto()
                    {
                        Id = hotelTranslation.HotelId,
                        Name = hotelTranslation.Name,
                        Stars = hotelTranslation.Hotel.Stars,
                        ImageUrl = hotelTranslation.Hotel.ImageUrl
                    })
                    .ToListAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Get hotels list failed![cityId={CityId}]", cityId.ToString());
                return null;
            }
        }

        public async Task<OperationResult> CreateHotel(CreateHotelDto model)
        {
            var operation = new OperationResult();
            var savedPhotoPath = string.Empty;
            try
            {
                if(await _dbContext.HotelTranslations.AnyAsync(hotelTranslation =>
                       hotelTranslation.Name == model.Name))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                savedPhotoPath = await _fileManagerService
                    .SaveFileAsync(model.HotelPic, AppConstants.HotelsPhotoPath, true, true);

                if(string.IsNullOrEmpty(savedPhotoPath))
                    return operation.Failed(ErrorMessages.CouldNotSavePic);

                var entity = await _mapper.From(model).AdaptToTypeAsync<Hotel>();
                entity.ImageUrl = savedPhotoPath;

                entity.HotelTranslations = new List<HotelTranslation>()
                {
                    new ()
                    {
                        LanguageId = _currentServices.LanguageId,
                        Name = model.Name,
                        About = model.About,
                        Address = model.Address
                    }
                };

                await _dbContext.Hotels.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New hotel created!");

                return operation.Succeeded(InformationMessages.SuccessfullySaved);
            }
            catch(Exception e)
            {
                if(!string.IsNullOrEmpty(savedPhotoPath))
                    await _fileManagerService.DeleteFileAsync(savedPhotoPath);

                _logger.LogCritical(e, "Could not create hotel");
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }

        }
      
        public async Task<HotelDetailsDto?> GetHotelDetails(int id)
        {
            try
            {
                var hotelDetailsDto = await _dbContext.Hotels.AsNoTracking()
                    //.Include(medicalCenter => medicalCenter.MedicalCenterTranslations)
                    .Where(hotel => hotel.Id == id)
                    .Select(hotel => new HotelDetailsDto()
                    {
                        Id = hotel.Id,
                        ImageUrl = Path.Combine(Path.AltDirectorySeparatorChar.ToString(), hotel.ImageUrl),
                        Stars = hotel.Stars,
                        CallInformation = hotel.CallInformation,
                        WebsiteAddress = hotel.WebsiteAddress,
                        CityId = hotel.CityId,
                        HotelTranslationDto = hotel.HotelTranslations!
                            .Select(hotelTranslation => new HotelTranslationDto()
                            {
                                Name = hotelTranslation.Name,
                                Address = hotelTranslation.Address,
                                LanguageId = hotelTranslation.LanguageId,
                                About = hotelTranslation.About
                            })
                            .ToList()
                    }).SingleOrDefaultAsync();

                hotelDetailsDto!.CityName = _dbContext.CityTranslations.AsNoTracking()
                    .FirstOrDefaultAsync(cityTranslation =>
                        cityTranslation.CityId == hotelDetailsDto.CityId
                        && cityTranslation.LanguageId == _currentServices.LanguageId)!.Result!.Name;
                
                //hotelDetailsDto!.CountryName = _dbContext.CountriesTranslation.AsNoTracking()
                //    .FirstOrDefaultAsync(countryTranslation =>
                //        countryTranslation.CountryId == hotelDetailsDto.CityId
                //        && countryTranslation.LanguageId == _currentServices.LanguageId)!.Result!.Name;


                return hotelDetailsDto;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get hotel Details failed![hotelId={id}]", id.ToString());
                return null;
            }

        }

        public async Task<OperationResult> EditHotel(EditHotelDto model)
        {
            var operation = new OperationResult();
            var savedPhotoPath = string.Empty;
            return operation.Succeeded();
            //try
            //{
            //    var entity = await _hotelRepository.GetAsync(hotel => hotel.Id == model.Id, true)
            //        .Result
            //        .FirstOrDefaultAsync();
            //    if(entity is null)
            //        return operation.Failed(ErrorMessages.ItemNotFound);

            //    if(entity.EnName.ToUpper() != model.EnName.ToUpper().Trim() &&
            //        await _hotelRepository.ExistsAsync(hotel => hotel.EnName.ToUpper() == model.EnName.ToUpper().Trim()))
            //        return operation.Failed(ErrorMessages.DuplicatedRecord);

            //    var oldPhotoPath = entity.ImageUrl;
            //    await _mapper.From(model).AdaptToAsync(entity);
            //    if(model.HotelPic is null)
            //    {
            //        await _hotelRepository.UpdateAsync(entity, true);
            //        return operation.Succeeded();
            //    }

            //    savedPhotoPath = await _fileManagerService
            //        .SaveAsync(model.HotelPic, StaticDetails.HotelsPhotoPath, true, true);

            //    if(string.IsNullOrEmpty(savedPhotoPath))
            //        return operation.Failed(ErrorMessages.CouldNotSavePic);

            //    entity.ImageUrl = $"/{savedPhotoPath}";
            //    await _hotelRepository.UpdateAsync(entity, true);
            //    if(!string.IsNullOrEmpty(oldPhotoPath))
            //        await _fileManagerService
            //            .DeleteOperationAsync($"{StaticDetails.RootFilesPath}//{oldPhotoPath}", true);

            //    return operation.Succeeded(InformationMessages.SuccessfullySaved);
            //}
            //catch(Exception e)
            //{
            //    if(!string.IsNullOrEmpty(savedPhotoPath))
            //        await _fileManagerService.DeleteOperationAsync(savedPhotoPath, true);

            //    _logger.LogCritical(e, "couldnt edit hotel");
            //    return operation.Failed(ErrorMessages.ErrorOccurred);
            //}
        }


        public async Task<HotelTranslationForEditDto?> GetHotelForEditAsync(int hotelId, int languageId)
        {
            try
            {
                if (!await _dbContext.Hotels.AsNoTracking()
                        .AnyAsync(hotel => hotel.Id == hotelId))
                {
                    return null;
                }

                var hotelTranslationDto = await _dbContext.HotelTranslations.AsNoTracking()
                    .Where(hotelTranslation => hotelTranslation.HotelId == hotelId
                                               && hotelTranslation.LanguageId == languageId)
                    .Select(hotelTranslation => new HotelTranslationForEditDto()
                    {
                        HotelId = hotelId,
                        LanguageName = languageId.GetLanguageName(),
                        LanguageId = languageId,
                        Name = hotelTranslation.Name,
                        Address = hotelTranslation.Address,
                        About = hotelTranslation.About
                    })
                    .SingleOrDefaultAsync();
                
                if (hotelTranslationDto is null)
                    return new HotelTranslationForEditDto()
                    {
                        HotelId = hotelId,
                        LanguageName = languageId.GetLanguageName(),
                        LanguageId = languageId,
                        Name = string.Empty,
                        Address = string.Empty,
                        About = string.Empty
                    };

                return hotelTranslationDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get hotel Details failed![hotelId={id}]",
                    hotelId.ToString());
                return null;
            }
        }


        public async Task<OperationResult> EditHotelAsync(HotelTranslationForEditDto hotelTranslationDto)
        {
            var operation = new OperationResult();
            try
            {
                var hotel = await _dbContext.Hotels.AsNoTracking()
                    .FirstOrDefaultAsync(hotel =>
                        hotel.Id == hotelTranslationDto.HotelId);

                // check for exist
                if (hotel is null)
                    return operation.Failed(ErrorMessages.ItemNotFound);

                var hotelTranslation = await _dbContext.HotelTranslations.AsNoTracking()
                    .FirstOrDefaultAsync(hotelTranslation =>
                        hotelTranslation.LanguageId == hotelTranslationDto.LanguageId
                        && hotelTranslation.HotelId == hotelTranslationDto.HotelId);
                //if hotelTranslation is null ==> create it
                if (hotelTranslation is null)
                {
                    var newHotelTranslation = new HotelTranslation()
                    {
                        LanguageId = hotelTranslationDto.LanguageId,
                        Name = hotelTranslationDto.Name,
                        Address = hotelTranslationDto.Address,
                        About = hotelTranslationDto.About,
                        HotelId = hotelTranslationDto.HotelId
                    };

                    await _dbContext.HotelTranslations.AddAsync(newHotelTranslation);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    hotelTranslation.Name = hotelTranslationDto.Name;
                    hotelTranslation.About = hotelTranslationDto.About;
                    hotelTranslation.Address = hotelTranslationDto.Address;

                    _dbContext.HotelTranslations.Update(hotelTranslation);
                    await _dbContext.SaveChangesAsync();
                }

                return operation.Succeeded(InformationMessages.SuccessfullyUpdated);
            }
            catch
            {
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }

        }


        public async Task<IReadOnlyList<CityWithHotelsCount>?> GetCitiesWithHotels()
        {
            
            try
            {
                return await _dbContext.Cities.AsNoTracking()
                    .Select(city => new CityWithHotelsCount()
                    {
                        CityId = city.Id,
                        HotelsCount = city.Hotels!.Count,
                        CityName = city.CityTranslations!
                            .First(cityTranslation 
                                => cityTranslation.LanguageId == _currentServices.LanguageId).Name

                    })
                    .ToListAsync();
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "Could not get cities with hotels count!!");
                return null;
            }
        }

        public async Task<CityWithHotelsCount?> GetCityInformation(int cityId)
        {
            try
            {
                return await _dbContext.Cities.AsNoTracking()
                    .Where(city => city.Id == cityId)
                    .Select(city => new CityWithHotelsCount()
                    {
                        CityId = city.Id,
                        HotelsCount = city.Hotels!.Count,
                        CityName = city.CityTranslations!
                            .First(cityTranslation
                                => cityTranslation.LanguageId == _currentServices.LanguageId).Name

                    })
                    .SingleOrDefaultAsync();
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "Could not get city informations.[cityId={cityId}]", cityId);
                return null;
            }
        }

        public async Task<HotelDetailsBasicsDto?> GetHotelBasicsDetails(int id)
        {
            try
            {
                return await _dbContext.Hotels.AsNoTracking()
                    .Where(hotel => hotel.Id == id)
                    .Select(hotel => new HotelDetailsBasicsDto()
                    {
                        Id = hotel.Id,
                        CallInformation = hotel.CallInformation,
                        Stars = hotel.Stars,
                        WebsiteAddress = hotel.WebsiteAddress,
                        ImageUrl = Path.Combine(Path.AltDirectorySeparatorChar.ToString(), hotel.ImageUrl)
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get hotel basics details failed![hotelId={id}]", id.ToString());
                return null;
            }
        }

        public async Task<OperationResult> EditHotelBasicsAsync(HotelDetailsBasicsDto model)
        {
            var operation = new OperationResult();
            var savedPhotoPath = string.Empty;
            try
            {
                var hotel = await _dbContext.Hotels.FindAsync(model.Id);

                if (hotel is null)
                    return operation.Failed(ErrorMessages.HotelNotFound);

                if (model.HotelPic is not null)
                {
                    // Save new image file
                    savedPhotoPath = await _fileManagerService
                        .SaveFileAsync(model.HotelPic, AppConstants.HotelsPhotoPath, true, true);

                    if (string.IsNullOrEmpty(savedPhotoPath))
                        return operation.Failed(ErrorMessages.CouldNotSavePic);

                    // Delete previous image
                    var deleteResult =
                        await _fileManagerService.DeleteFileAsync(
                            Path.Combine(AppConstants.RootFilesPath, hotel.ImageUrl), true);
                    if (!deleteResult)
                        return operation.Failed(ErrorMessages.CouldNotRemovePic);

                    hotel.ImageUrl = savedPhotoPath;
                }

                hotel.CallInformation= model.CallInformation;
                hotel.Stars= model.Stars;
                hotel.WebsiteAddress = model.WebsiteAddress;

                _dbContext.Hotels.Update(hotel);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("Hotel base information updated![hotelId={}]", hotel.Id);

                return operation.Succeeded(InformationMessages.SuccessfullyUpdated);
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(savedPhotoPath))
                    await _fileManagerService.DeleteFileAsync(savedPhotoPath);

                _logger.LogCritical(e, "Could not update hotel[hotelId={}]", model.Id);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }


        public async Task<IEnumerable<HotelFacilityForCheckBox>> GetHotelSelectedFacilities(int hotelId)
        {
            try
            {
                var result = new List<HotelFacilityForCheckBox>();

                var parentFacilities = await  _dbContext.HotelFacilities
                    .Where(facility => facility.ParentId == null)
                    .Select(facility=>new HotelFacilityForCheckBox()
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
                _logger.LogCritical(e, "Could not GetHotelSelectedFacilities[hotelId={}]",hotelId);
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
                var listToRemove =  _dbContext.HotelSelectedFacilities.AsNoTracking()
                    .Where(facility => facility.HotelId == model.HotelId);
                if (await listToRemove.AnyAsync())
                {
                    _dbContext.HotelSelectedFacilities.RemoveRange(listToRemove);
                    await _dbContext.SaveChangesAsync();
                }

                foreach (var facilityId in model.SelectedHotelFacilities)
                {
                    /*if (!await _selectedFacilitiesRepository
                            .ExistsAsync(fa => fa.HotelId == model.HotelId &&
                                               fa.HotelFacilityId == facilityId))*/
                    await _dbContext.HotelSelectedFacilities
                        .AddAsync(new HotelSelectedFacility()
                        {
                            HotelId = model.HotelId,
                            HotelFacilityId = facilityId
                        });
                    
                }
                await _dbContext.SaveChangesAsync();
                /*watch.Stop();
                _logger.LogInformation("it took [{WatchElapsedMilliseconds}]ms", watch.ElapsedMilliseconds);*/
                return operation.Succeeded();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "could not edit hotel facilities");
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }




        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
