using Domain.Entities.Destination;
using Domain.Entities.MedicalCenters.Translation;

namespace Domain.Entities.MedicalCenters
{
    public class MedicalCenter
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? AgentPhoneNumber { get; set; }

        #region Relations

        // City
        public int CityId { get; set; }
        public City City { get; set; } = null!;

        // City Translations
        public ICollection<MedicalCenterTranslation>? MedicalCenterTranslations { get; set; }

        //MedicalCenterMedicalSrvice
        public ICollection<MedicalCenterMedicalService>? MedicalCenterMedicalServices { get; set; }

        #endregion
    }
}
