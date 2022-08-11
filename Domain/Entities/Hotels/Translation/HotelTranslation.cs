
using Domain.Entities.Localization;

namespace Domain.Entities.Hotels.Translation
{
    public class HotelTranslation
    {
        public string Name { get; set; } = null!;
        public string? About { get; set; }
        public string? Address { get; set; }

        #region Relations

        // HotelFacility
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;

        // Language
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        #endregion
    }
}
