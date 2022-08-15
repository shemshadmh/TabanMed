
using Domain.Entities.Destination.Translation;
using Domain.Entities.Hotels;

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

        #endregion
    }
}
