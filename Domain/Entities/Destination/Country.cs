
using Domain.Entities.Destination.Translation;

namespace Domain.Entities.Destination
{
    public class Country
    {
        public int Id { get; set; }
        

        #region Relations

        // Cities
        public ICollection<City>? Cities { get; set; }

        // CountryTranslation
        public ICollection<CountryTranslation>? CountryTranslations { get; set; }

        #endregion
    }
}
