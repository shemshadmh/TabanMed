
using Domain.Entities.Localization;

namespace Domain.Entities.Hotels.Translation
{
    public class HotelFacilityTranslation
    {
        public string Title { get; set; } = null!;

        #region Relations

        // HotelFacility
        public int FacilityId { get; set; }
        public HotelFacility HotelFacility { get; set; } = null!;

        // Language
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        #endregion
    }
}
