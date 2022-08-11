
using Domain.Entities.Hotels.Translation;

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
    }
}
