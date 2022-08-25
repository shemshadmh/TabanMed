

using Application.Common;
using Application.Dtos.MedicalCenters.MedicalServices;

namespace Application.Interfaces.MedicalCenters
{
    public interface IMedicalServiceApplication
    {
        Task<IReadOnlyList<MedicalServiceListItemDto>?> GetMedicalServiceListAsync(int? parentId = null);

        Task<OperationResult> CreateMedicalService(MedicalServiceListItemDto medicalServiceDto);

        Task<OperationResult> UpdateMedicalService(MedicalServiceListItemDto medicalServiceDto);

        Task<IEnumerable<MedicalServiceForCheckBox>> GetSelectedMedicalService(int medicalCenterId);
        Task<EditMedicalServiceDto> GetMedicalServiceForEdit(int medicalCenterId);
        Task<OperationResult> EditMedicalServices(EditMedicalServiceDto model);
    }
}
