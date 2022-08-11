using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;

namespace Application.Dtos.Hotels.Hotels
{
    public class HotelListItemDto
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.FaName))]
        public string FaName { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.EnName))]
        public string EnName { get; set; } = null!;

        public int Stars { get; set; }

        public string ImageUrl { get; set; } = null!;
        /*Used to fill the edit inputs*/

        public string? About { get; set; }
        public string? OtherNames { get; set; }
        public int? RoomCount { get; set; }
        public string? Address { get; set; }
        public string? CallInformation { get; set; }
        public string? WebsiteAddress { get; set; }
        public int CityId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public byte Zoom { get; set; }
    }
}