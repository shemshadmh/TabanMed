
using Domain.Entities.Destination.Translation;
using Domain.Entities.Hotels;
using Domain.Entities.MedicalCenters;

namespace Domain.Entities.Destination
{
    public class City 
    {
        public int Id { get; set; }

        #region Relations

        // Country
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        // CityTranslation
        public ICollection<CityTranslation>? CityTranslations { get; set; }

        // Hotels
        public ICollection<Hotel>? Hotels { get; set; }

        // MedicalCenters
        public ICollection<MedicalCenter>? MedicalCenters { get; set; }


        #endregion
    }
}
