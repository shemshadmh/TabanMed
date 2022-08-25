using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;

namespace Application.Dtos.TourServices
{
    public class TourServiceDetailesDto
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.TourServiceTitle))]
        public string Title { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Description))]
        public string? Description { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Price))]
        public decimal Price { get; set; }
    }
}
