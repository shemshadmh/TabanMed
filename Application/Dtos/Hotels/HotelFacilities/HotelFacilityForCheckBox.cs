namespace Application.Dtos.Hotels.HotelFacilities
{
    public class HotelFacilityForCheckBox
    {
        public string  Title { get; set; } = null!;
        public int Value { get; set; }
        public int? ParentId { get; set; }
        public bool IsSelected { get; set; }
    }
}
