
using Domain.Common;
using Domain.Entities.Destination;

namespace Domain.Entities.Hotels
{
    public class Hotel : DeletableEntity
    {
        public int Id { get; set; }
        public string FaName { get; set; } = null!;
        public string EnName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? About { get; set; }
        public int Stars { get; set; }
        public string? Address { get; set; }
        public string? CallInformation { get; set; }
        public string? WebsiteAddress { get; set; }

        #region Location

        public double Lat { get; set; }
        public double Lng { get; set; }
        public byte Zoom { get; set; }

        #endregion

        #region Relations

        //Hotel Facilities
        public ICollection<HotelSelectedFacility>? HotelSelectedFacilities { get; set; }

        // City
        public int CityId { get; set; }
        public City City { get; set; } = null!;

        // Gallery
        public ICollection<HotelImage>? Gallery { get; set; }

        #endregion
    }
}
