
using Domain.Common;
using Domain.Entities.Destination;
using Domain.Entities.Hotels.Translation;

namespace Domain.Entities.Hotels
{
    public class Hotel : DeletableEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int Stars { get; set; }
        public string? CallInformation { get; set; }
        public string? WebsiteAddress { get; set; }

        #region Relations

        //Hotel Facilities
        public ICollection<HotelSelectedFacility>? HotelSelectedFacilities { get; set; }

        // City
        public int CityId { get; set; }
        public City City { get; set; } = null!;

        // Gallery
        public ICollection<HotelImage>? Gallery { get; set; }

        // Hotel Translations
        public ICollection<HotelTranslation>? HotelTranslations { get; set; }

        #endregion
    }
}
