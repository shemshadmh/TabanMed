
using Domain.Entities.Localization;

namespace Domain.Entities.MedicalCenters.Translation
{
    public class MedicalServiceTranslation
    {
        public string Title { get; set; } = null!;

        #region Relations

        // MedicalService
        public int MedicalServiceId { get; set; }
        public MedicalService MedicalService { get; set; } = null!;

        // Language
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        #endregion
    }
}
