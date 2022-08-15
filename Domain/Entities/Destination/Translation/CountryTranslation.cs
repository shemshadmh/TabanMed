
using Domain.Entities.Localization;

namespace Domain.Entities.Destination.Translation
{
    public class CountryTranslation
    {
        public string Name { get; set; } = null!;

        #region Relations

        // Country
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;

        // Language
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

        #endregion
    }
}
