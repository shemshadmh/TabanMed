
using Domain.Entities.Destination.Translation;
using Domain.Entities.Hotels.Translation;
using Domain.Entities.MedicalCenters.Translation;

namespace Domain.Entities.Localization
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string IsoName { get; set; } = null!;

        // Hotel Facility Translations
        public ICollection<HotelFacilityTranslation>? HotelFacilityTranslations { get; set; }

        // Hotel Translations
        public ICollection<HotelTranslation>? HotelTranslations { get; set; }

        // CountryTranslation
        public ICollection<CountryTranslation>? CountryTranslations { get; set; }

        // CityTranslation
        public ICollection<CityTranslation>? CityTranslations { get; set; }

        // MedicalCenter Translation
        public ICollection<MedicalCenterTranslation>? MedicalCenterTranslations{ get; set; }
    }
}
