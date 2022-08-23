namespace Application.Dtos.Hotels.Hotels;
public class HotelDetailsDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int Stars { get; set; }
    public string? CallInformation { get; set; }
    public string? WebsiteAddress { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; } = null!;

    public ICollection<HotelTranslationDto>? HotelTranslationDto { get; set; }  

    
}