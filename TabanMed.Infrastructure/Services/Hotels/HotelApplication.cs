
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.Hotels.Hotels;
using Application.Interfaces.Application;
using Application.Interfaces.Hotels;
using Domain.Entities.Hotels;
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
        private readonly IMapper _mapper;
        private readonly string _twoLetterISOLanguageName;

        public HotelApplication(IApplicationDbContext dbContext,
            ILogger<HotelApplication> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _twoLetterISOLanguageName = Thread.CurrentThread.CurrentUICulture.Name;
        }

        public async Task<IReadOnlyList<HotelListItemDto>?> GetHotels(int cityId)
        {
            try
            {
                var query = _dbContext.Hotels
                    .Where(hotel => hotel.CityId == cityId);

                return await _mapper.From(query).AdaptToTypeAsync<List<HotelListItemDto>>();
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
            return operation.Succeeded();
            //try
            //{
            //    if(await _hotelRepository.ExistsAsync(hotel => hotel.EnName.ToUpper() == model.EnName.ToUpper() ||
            //                                                    hotel.FaName == model.FaName))
            //        return operation.Failed(ErrorMessages.DuplicatedRecord);

            //    var entity = await _mapper.From(model).AdaptToTypeAsync<Hotel>();

            //    savedPhotoPath = await _fileManagerService
            //        .SaveAsync(model.HotelPic, StaticDetails.HotelsPhotoPath, true, true);

            //    if(string.IsNullOrEmpty(savedPhotoPath))
            //        return operation.Failed(ErrorMessages.CouldNotSavePic);

            //    entity.ImageUrl = $"/{savedPhotoPath}";
            //    await _hotelRepository.AddAsync(entity, true);
            //    return operation.Succeeded(InformationMessages.SuccessfullySaved);
            //}
            //catch(Exception e)
            //{
            //    if(!string.IsNullOrEmpty(savedPhotoPath))
            //        await _fileManagerService.DeleteOperationAsync(savedPhotoPath);

            //    _logger.LogCritical(e, "couldnt Create hotel");
            //    return operation.Failed(ErrorMessages.ErrorOccurred);
            //}

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

        public async Task<OperationResult> DeleteHotel(DeleteHotelDto model)
        {
            var operation = new OperationResult();
            return operation.Succeeded();
            //try
            //{
            //    if(!await _hotelRepository.ExistsAsync(hotel => hotel.Id == model.Id))
            //        return operation.Failed(ErrorMessages.ItemNotFound);

            //    var hotelEntity = await _hotelRepository.GetAsync(hotel => hotel.Id == model.Id, true)
            //        .Result.FirstOrDefaultAsync();

            //    var photoUrl = hotelEntity!.ImageUrl;
            //    await _hotelRepository.DeleteAsync(hotelEntity, true);
            //    //If it was necessary to delete image uncomment these lines 

            //    /*
            //    if (!string.IsNullOrEmpty(photoUrl))
            //        await _fileManagerService
            //            .DeleteOperationAsync($"{StaticDetails.RootFilesPath}//{photoUrl}", true);
            //            */

            //    return operation.Succeeded();
            //}
            //catch(Exception e)
            //{
            //    _logger.LogCritical(e, "Could not Delete Hotel !!");
            //    return operation.Failed(ErrorMessages.ErrorOccurred);
            //}
        }

        public async Task<HotelDetailsDto?> GetHotelDetails(int id)
        {
            throw new NotImplementedException();
            //var entity = await (await _hotelRepository
            //        .GetAsync(hotel => hotel.Id == id,
            //            includes: hotel => hotel.Include(hotelDto => hotelDto.City)
            //        ))
            //    .FirstOrDefaultAsync();
            //var mappedEntity = await _mapper.From(entity).AdaptToTypeAsync<HotelDetailsDto>();
            //mappedEntity.CountPendingComments = await _commentRepository.CountAsync(comment => comment.EntityId == id &&
            //    comment.Type == EntityType.Hotel &&
            //    comment.IsActive == null);

            //return mappedEntity;
        }

        public async Task<IEnumerable<HotelFacilityListItem>> GetHotelSelectedFacilities(int hotelId)
        {
            throw new NotImplementedException();
            //var result = new List<HotelFacilityListItem>();

            //var parentFacilities = await _hotelFacilityRepository
            //    .GetAsync(facility => facility.ParentId == null, true);
            //var mappedParentFacilities = await _mapper.From(parentFacilities)
            //    .AdaptToTypeAsync<List<HotelFacilityListItem>>();

            //var selectedFacilitiesQuery = await _selectedFacilitiesRepository
            //    .GetAsync(selectedFacility => selectedFacility.HotelId == hotelId,
            //        includes: facility => facility
            //            .Include(selectedFacility => selectedFacility.HotelFacility));
            //var mappedSelectedFacilities = await _mapper.From(selectedFacilitiesQuery)
            //    .AdaptToTypeAsync<List<HotelFacilityListItem>>();

            //result.AddRange(mappedParentFacilities);
            //result.AddRange(mappedSelectedFacilities);
            //return result;
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
                                => cityTranslation.Language.IsoName == _twoLetterISOLanguageName).Name

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
                                => cityTranslation.Language.IsoName == _twoLetterISOLanguageName).Name

                    })
                    .SingleOrDefaultAsync();
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "Could not get city informations.[cityId={cityId}]", cityId);
                return null;
            }
        }

        public async Task<IEnumerable<HotelToursDto>> GetHotelTours(int hotelId)
        {
            throw new NotImplementedException();
            //var query = (await _hotelRepository.GetAsync(hotel => hotel.Id == hotelId,
            //        includes: hotel => hotel.Include(hotelDto => hotelDto.PackageSelectedHotels!)
            //            .ThenInclude(package => package.TourPackageHotel)
            //            .ThenInclude(packageHotel => packageHotel.TourDate)
            //            .ThenInclude(tourDate => tourDate.TourPackage)))
            //    .SelectMany(hotel => hotel.PackageSelectedHotels!);
            //var data = await _mapper.From(query).AdaptToTypeAsync<IEnumerable<HotelToursDto>>();
            //return data;
        }

        public async Task<IEnumerable<HotelAlbumDto>> GetHotelAlbum(int hotelId)
        {
            throw new NotImplementedException();
            //var query = (await _hotelRepository.GetAsync(hotel => hotel.Id == hotelId,
            //        includes: hotel => hotel.Include(image => image.Gallery!)))
            //    .SelectMany(hotel => hotel.Gallery!);
            //var data = await _mapper.From(query).AdaptToTypeAsync<IEnumerable<HotelAlbumDto>>();
            //return data;
        }

        public async Task<OperationResult> AppendToHotelAlbum(AppendToHotelAlbumDto model)
        {
            throw new NotImplementedException();
            //var operation = new OperationResult();
            //var savedPhotoPath = "";
            //try
            //{
            //    if(!await _hotelRepository.ExistsAsync(hotel => hotel.Id == model.HotelId))
            //        operation.Failed(ErrorMessages.HotelNotFound);

            //    var entity = await _mapper.From(model).AdaptToTypeAsync<HotelImage>();
            //    savedPhotoPath = await _fileManagerService
            //        .SaveAsync(model.ImageFile, StaticDetails.HotelsPhotoPath, true, true);

            //    if(string.IsNullOrEmpty(savedPhotoPath))
            //        return operation.Failed(ErrorMessages.CouldNotSavePic);

            //    entity.ImageUrl = $"/{savedPhotoPath}";
            //    await _hotelImageRepository.AddAsync(entity, true);
            //    return operation.Succeeded(entity.ImageUrl);
            //}
            //catch(Exception e)
            //{
            //    _logger.LogCritical(e, "Could not append image to hotel album");
            //    if(!string.IsNullOrEmpty(savedPhotoPath))
            //        await _fileManagerService
            //            .DeleteOperationAsync(StaticDetails.RootFilesPath + "//" + savedPhotoPath, true);

            //    return operation.Failed(ErrorMessages.ErrorOccurred);
            //}
        }

        public async Task<OperationResult> RemoveFromHotelAlbum(RemoveFromHotelAlbumDto model)
        {
            throw new NotImplementedException();
            //var operation = new OperationResult();
            //try
            //{
            //    var entity = await _hotelImageRepository.GetAsync(image =>
            //            image.HotelId == model.HotelId && image.ImageUrl == model.PhotoUrl, false)
            //        .Result.FirstOrDefaultAsync();
            //    if(entity is null)
            //        return operation.Failed(ErrorMessages.ItemNotFound);
            //    await _hotelImageRepository.DeleteAsync(entity, true);
            //    await _fileManagerService.DeleteOperationAsync(StaticDetails.RootFilesPath + "//" + entity.ImageUrl,
            //        true);
            //    return operation.Succeeded();
            //}
            //catch(Exception e)
            //{
            //    _logger.LogCritical(e, "error occurred while deleting photo album");
            //    return operation.Failed(ErrorMessages.ErrorOccurred);
            //}
        }

        public async Task<EditHotelFacilitiesDto> GetHotelFacilitiesForEdit(int hotelId)
        {
            throw new NotImplementedException();
            //var allHotelFacilities = await _hotelFacilityRepository
            //    .GetAsync(null,
            //        includes: facility => facility
            //            .Include(facilityItem => facilityItem.Parent!));

            //var selectedFacilitiesByHotel = await _hotelRepository.GetHotelFacilityIds(hotelId);
            //var facilityListForCheckBox = await allHotelFacilities
            //    .Select(facility => new HotelFacilityForCheckBox()
            //    {
            //        DisplayName = facility.Title,
            //        Value = facility.Id,
            //        IsSelected = selectedFacilitiesByHotel != null && selectedFacilitiesByHotel.Contains(facility.Id),
            //        ParentId = facility.ParentId,
            //        ParentDisplay = facility.Parent != null ? facility.Parent.Title : null
            //    }).ToListAsync();
            //var res = new EditHotelFacilitiesDto
            //{
            //    AllFacilities = facilityListForCheckBox,
            //    HotelId = hotelId
            //};
            //return res;
        }

        public async Task<OperationResult> EditHotelFacilities(EditHotelFacilitiesDto model)
        {
            throw new NotImplementedException();
            //// var watch = Stopwatch.StartNew();
            //var operation = new OperationResult();
            //try
            //{
            //    var listToRemove = await _selectedFacilitiesRepository
            //        .GetAsync(facility => facility.HotelId == model.HotelId, false);
            //    if(await listToRemove.AnyAsync())
            //        await _selectedFacilitiesRepository.DeleteRangeAsync(listToRemove, true);

            //    foreach(var facilityId in model.SelectedHotelFacilities)
            //    {
            //        /*if (!await _selectedFacilitiesRepository
            //                .ExistsAsync(fa => fa.HotelId == model.HotelId &&
            //                                   fa.HotelFacilityId == facilityId))*/
            //        await _selectedFacilitiesRepository
            //            .AddAsync(new HotelSelectedFacility()
            //            {
            //                HotelId = model.HotelId,
            //                HotelFacilityId = facilityId
            //            }, true);
            //    }

            //    /*watch.Stop();
            //    _logger.LogInformation("it took [{WatchElapsedMilliseconds}]ms", watch.ElapsedMilliseconds);*/
            //    return operation.Succeeded();
            //}
            //catch(Exception e)
            //{
            //    _logger.LogCritical(e, "could not edit hotel facilities");
            //    return operation.Failed(ErrorMessages.ErrorOccurred);
            //}
        }

        //public async Task<IEnumerable<CommentListItemsDto>> GetAllHotelComments(int hotelId)
        //{

        //    var query = await _commentRepository
        //        .GetAsync(comment => comment.EntityId == hotelId && comment.Type == EntityType.Hotel,
        //            orderBy: comments => comments.OrderBy(comment => comment.IsActive)
        //            , includeString: null);
        //    return await _mapper.From(query).AdaptToTypeAsync<IEnumerable<CommentListItemsDto>>();
        //}

        //public async Task<OperationResult> RejectHotelComment(int id)
        //{
        //    var operation = new OperationResult();
        //    try
        //    {
        //        var hotelComment = await (await _commentRepository
        //                .GetAsync(comment => comment.Id == id &&
        //                                     comment.Type == EntityType.Hotel, true))
        //            .FirstOrDefaultAsync();

        //        if(hotelComment == null)
        //            operation.Failed(ErrorMessages.ItemNotFound);

        //        hotelComment!.IsActive = false;
        //        await _commentRepository.UpdateAsync(hotelComment, true);
        //        return operation.Succeeded();
        //    }
        //    catch(Exception e)
        //    {
        //        _logger.LogCritical(e, "could not reject comment");
        //        return operation.Failed(ErrorMessages.ErrorOccurred);
        //    }
        //}

        //public async Task<OperationResult> AcceptHotelComment(int id)
        //{
        //    var operation = new OperationResult();
        //    try
        //    {
        //        var hotelComment = await (await _commentRepository
        //                .GetAsync(comment => comment.Id == id &&
        //                                     comment.Type == EntityType.Hotel, true))
        //            .FirstOrDefaultAsync();

        //        if(hotelComment == null)
        //            operation.Failed(ErrorMessages.ItemNotFound);

        //        hotelComment!.IsActive = true;
        //        await _commentRepository.UpdateAsync(hotelComment, true);
        //        return operation.Succeeded();
        //    }
        //    catch(Exception e)
        //    {
        //        _logger.LogCritical(e, "could not accept comment");
        //        return operation.Failed(ErrorMessages.ErrorOccurred);
        //    }
        //}
        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
