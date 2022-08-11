using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;

namespace Application.Dtos.Hotels.Hotels;
public class HotelToursDto
{
    public int TourId { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FaTitle))]
    public string Title { get; set; } = null!;

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.DepartureDate))]
    public DateTime Departure { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ReturnDate))]
    public DateTime Return { get; set; }
}