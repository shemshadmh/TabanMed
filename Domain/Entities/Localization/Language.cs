
using Domain.Entities.Destination.Translation;
using Domain.Entities.Hotels.Translation;
using Domain.Entities.MedicalCenters.Translation;
using Domain.Entities.TourServices.Translation;

namespace Domain.Entities.Localization
{
    public class Language
    {
        public int Lcid { get; set; }
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

        // Medical Serviec Translations
        public ICollection<MedicalServiceTranslation>? MedicalServiceTranslations { get; set; }

        // Tour Serviec Translations
        public ICollection<TourServiceTranslation>? TourServiceTranslations { get; set; }
    }
}
