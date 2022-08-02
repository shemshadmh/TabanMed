
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Interfaces.Application;
using Application.Interfaces.Hotels;
using Domain.Entities.Hotels;
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
                    .Where(facility => facility.ParentId == parentId);

                return await _mapper.From(query).AdaptToTypeAsync<List<HotelFacilityListItem>>();
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
                //if(await _dbContext.HotelFacilities.AnyAsync(facility => facility.Title == facilityDto.Title))
                //    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(facilityDto).AdaptToTypeAsync<HotelFacility>();

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

        public async Task<OperationResult> CreateFacility(CreateHotelFacilityDto facilityDto)
        {
            var operation = new OperationResult();
            try
            {
                //if(await _dbContext.HotelFacilities.AnyAsync(facility => facility.Title == facilityDto.Title))
                //    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(facilityDto).AdaptToTypeAsync<HotelFacility>();

                await _dbContext.HotelFacilities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New hotel facility created successfully.[facility:{title},parentId={parentId}]",
                    facilityDto.Title, facilityDto.ParentId);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullySaved);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Could not create new hotel facility.[facility:{title}],parentId={parentId}]",
                    facilityDto.Title, facilityDto.ParentId);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public async Task<OperationResult> UpdateFacility(HotelFacilityListItem facilityDto)
        {
            var operation = new OperationResult();
            try
            {
                //if(await _dbContext.HotelFacilities
                //       .AnyAsync(facility => facility.Title == facilityDto.Title
                //                                                            && facility.Id != facilityDto.Id))
                //    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(facilityDto).AdaptToTypeAsync<HotelFacility>();

                _dbContext.HotelFacilities.Update(entity);

                _logger.LogWarning("Hotel facility updated successfully.[facility:{title}]", facilityDto.EnTitle);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullyUpdated);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Could not update hotel facility.[facility:{title}]", facilityDto.EnTitle);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public async Task<OperationResult> UpdateFacility(EditHotelFacilityDto facilityDto)
        {
            var operation = new OperationResult();
            try
            {
                //if(await _dbContext.HotelFacilities
                //       .AnyAsync(facility => facility.Title == facilityDto.Title
                //                                                            && facility.Id != facilityDto.Id))
                //    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(facilityDto).AdaptToTypeAsync<HotelFacility>();

                _dbContext.HotelFacilities.Update(entity);

                _logger.LogWarning("Hotel facility updated successfully.[facility:{title},parentId={parentId}]",
                    facilityDto.Title, facilityDto.ParentId);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullyUpdated);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Could not update hotel facility.[facility:{title},parentId={parentId}]",
                    facilityDto.Title, facilityDto.ParentId);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public  ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
