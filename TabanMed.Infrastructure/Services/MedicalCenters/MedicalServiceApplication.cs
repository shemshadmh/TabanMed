
using Application;
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;
using Application.Dtos.MedicalCenters.MedicalServices;
using Application.Interfaces.Application;
using Application.Interfaces.MedicalCenters;
using Domain.Entities.Hotels.Translation;
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
    public class MedicalServiceApplication: IMedicalServiceApplication
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<MedicalServiceApplication> _logger;
        private readonly IMapper _mapper;
        public MedicalServiceApplication(IApplicationDbContext dbContext,
            ILogger<MedicalServiceApplication> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<MedicalServiceListItemDto>?> GetMedicalServiceListAsync(int? parentId = null)
        {
            try
            {
                var query = _dbContext.MedicalServices.AsNoTracking()
                    .Include(medicalService => medicalService.MedicalServiceTranslations)
                    .Where(medicalService => medicalService.ParentId == parentId)
                    .Select(medicalService => new MedicalServiceListItemDto()
                    {
                        Id = medicalService.Id,
                        ParentId = medicalService.ParentId,
                        FaTitle = medicalService.MedicalServiceTranslations!
                            .Where(medicalServiceTranslation => medicalServiceTranslation.LanguageId == AppConstants.FaLanguageLcid)
                            .Select(medicalServiceTranslation => medicalServiceTranslation.Title)
                            .Single(),
                        EnTitle = medicalService.MedicalServiceTranslations!
                            .Where(medicalServiceTranslation => medicalServiceTranslation.LanguageId == AppConstants.EnLanguageLcid)
                            .Select(medicalServiceTranslation => medicalServiceTranslation.Title)
                            .Single(),
                        ArTitle = medicalService.MedicalServiceTranslations!
                            .Where(medicalServiceTranslation => medicalServiceTranslation.LanguageId == AppConstants.ArLanguageLcid)
                            .Select(medicalServiceTranslation => medicalServiceTranslation.Title)
                            .Single(),
                        AfTitle = medicalService.MedicalServiceTranslations!
                            .Where(medicalServiceTranslation => medicalServiceTranslation.LanguageId == AppConstants.AfLanguageLcid)
                            .Select(medicalServiceTranslation => medicalServiceTranslation.Title)
                            .Single()
                    });

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get all medical service failed!");
                return null;
            }
        }

        public async Task<OperationResult> CreateMedicalService(MedicalServiceListItemDto medicalServiceDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.MedicalServiceTranslations.AnyAsync(medicalServiceTranslation =>
                        medicalServiceTranslation.Title == medicalServiceDto.FaTitle ||
                        medicalServiceTranslation.Title == medicalServiceDto.EnTitle ||
                        medicalServiceTranslation.Title == medicalServiceDto.ArTitle ||
                        medicalServiceTranslation.Title == medicalServiceDto.AfTitle))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var entity = await _mapper.From(medicalServiceDto).AdaptToTypeAsync<MedicalService>();

                entity.MedicalServiceTranslations = new List<MedicalServiceTranslation>()
                {
                    new()
                    {
                        LanguageId = AppConstants.FaLanguageLcid,
                        Title = medicalServiceDto.FaTitle

                    },
                    new()
                    {
                        LanguageId = AppConstants.EnLanguageLcid,
                        Title = medicalServiceDto.EnTitle
                    },
                    new()
                    {
                        LanguageId = AppConstants.ArLanguageLcid,
                        Title = medicalServiceDto.ArTitle
                    },
                    new()
                    {
                        LanguageId = AppConstants.AfLanguageLcid,
                        Title = medicalServiceDto.AfTitle
                    }
                };
                if (medicalServiceDto.ParentId is not null)
                    entity.ParentId = medicalServiceDto.ParentId;

                await _dbContext.MedicalServices.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("New medical service created successfully.[medicalService:{title}]", medicalServiceDto.EnTitle);

                return operation.Succeeded(entity.Id, InformationMessages.SuccessfullySaved);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not create new medical service.[medicalService:{title}]", medicalServiceDto.EnTitle);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public async Task<OperationResult> UpdateMedicalService(MedicalServiceListItemDto medicalServiceDto)
        {
            var operation = new OperationResult();
            try
            {
                if (await _dbContext.MedicalServiceTranslations.AnyAsync(medicalServiceTranslation =>
                       (medicalServiceTranslation.Title == medicalServiceDto.FaTitle ||
                        medicalServiceTranslation.Title == medicalServiceDto.EnTitle ||
                        medicalServiceTranslation.Title == medicalServiceDto.ArTitle ||
                        medicalServiceTranslation.Title == medicalServiceDto.AfTitle)
                       && medicalServiceTranslation.MedicalServiceId != medicalServiceDto.Id))
                    return operation.Failed(ErrorMessages.DuplicatedRecord);

                var translationsList = await _dbContext.MedicalServiceTranslations
                    .Where(medicalServiceTranslation => medicalServiceTranslation.MedicalServiceId == medicalServiceDto.Id)
                    .ToListAsync();

                foreach (var medicalServiceTranslation in translationsList)
                {
                    medicalServiceTranslation.Title = medicalServiceTranslation.LanguageId switch
                    {
                        AppConstants.FaLanguageLcid => medicalServiceDto.FaTitle,
                        AppConstants.EnLanguageLcid => medicalServiceDto.EnTitle,
                        AppConstants.ArLanguageLcid => medicalServiceDto.ArTitle,
                        AppConstants.AfLanguageLcid => medicalServiceDto.AfTitle,
                        _ => medicalServiceTranslation.Title
                    };
                }

                _dbContext.MedicalServiceTranslations.UpdateRange(translationsList);
                await _dbContext.SaveChangesAsync();

                _logger.LogWarning("Medical service updated successfully.[medicalService:{title}]", medicalServiceDto.EnTitle);

                return operation.Succeeded(medicalServiceDto.Id, InformationMessages.SuccessfullyUpdated);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update medical Service.[medicalService:{title}]", medicalServiceDto.EnTitle);
                return operation.Failed(ErrorMessages.ErrorOccurred);
            }
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
