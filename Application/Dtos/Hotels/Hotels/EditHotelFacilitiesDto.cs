using Application.Dtos.Hotels.HotelFacilities;

namespace Application.Dtos.Hotels.Hotels;
public class EditHotelFacilitiesDto
{
    //[BindNever] 
    public ICollection<HotelFacilityForCheckBox>? AllFacilities { get; set; }

    public List<int> SelectedHotelFacilities { get; set; } = null!;
    public int HotelId { get; set; }
}