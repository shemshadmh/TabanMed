
using Domain.Entities.Localization;

namespace Domain.Entities.Destination.Translation
{
    public class CityTranslation
    {
        public string Name { get; set; } = null!;

        #region Relations

        // City
        public int CityId { get; set; }
        public City City { get; set; } = null!;

        // Language
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        #endregion
    }
}
