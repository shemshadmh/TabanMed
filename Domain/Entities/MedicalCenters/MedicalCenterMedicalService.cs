

namespace Domain.Entities.MedicalCenters
{
    public class MedicalCenterMedicalService
    {
        // MedicalCenter
        public int MedicalCenterId { get; set; }
        public MedicalCenter MedicalCenter { get; set; } = null!;

        // Medical Services
        public int MedicalServiceId { get; set; }
        public MedicalService MedicalService { get; set; } = null!;
    }
}
