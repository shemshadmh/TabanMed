using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Hotels.Hotels;
public class RemoveFromHotelAlbumDto
{
    [Required(AllowEmptyStrings = false)] public string PhotoUrl { get; set; } = null!;
    [Required] public int HotelId { get; set; }
}