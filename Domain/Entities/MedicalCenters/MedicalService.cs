
using Domain.Entities.MedicalCenters.Translation;

namespace Domain.Entities.MedicalCenters
{
    public class MedicalService
    {
        public int Id { get; set; }
        public int Price { get; set; }

        #region Relations

        // Self
        public int? ParentId { get; set; }
        public MedicalService? Parent { get; set; } = null!;
        public ICollection<MedicalService> Children { get; set; } = null!;

        //MedicalCenterMedicalSrvice
        public ICollection<MedicalCenterMedicalService>? MedicalCenterMedicalServices { get; set; }

        // Medical Services Translations
        public ICollection<MedicalServiceTranslation>? MedicalServiceTranslations { get; set; }



        #endregion
    }
}
