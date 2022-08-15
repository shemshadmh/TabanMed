using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;

namespace Application.Dtos.Hotels.Hotels
{
    public class HotelListItemDto
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Name))]
        public string Name { get; set; } = null!;

        public int Stars { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}