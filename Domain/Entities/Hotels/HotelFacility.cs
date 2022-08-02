
using Domain.Entities.Hotels.Translation;

namespace Domain.Entities.Hotels
{
    public class HotelFacility
    {
        public int Id { get; set; }

        #region Relations

        // Self
        public int? ParentId { get; set; }
        public HotelFacility? Parent { get; set; } = null!;
        public ICollection<HotelFacility> Children { get; set; } = null!;

        //Hotel Facilities
        public ICollection<HotelSelectedFacility>? HotelSelectedFacilities { get; set; }

        // Hotel Facility Translations
        public ICollection<HotelFacilityTranslation>? HotelFacilityTranslations { get; set; }

        #endregion
    }
}
