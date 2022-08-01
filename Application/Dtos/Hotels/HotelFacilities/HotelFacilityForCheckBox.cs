namespace Application.Dtos.Hotels.HotelFacilities
{
    public class HotelFacilityForCheckBox
    {
        public string DisplayName { get; set; } = null!;
        public int Value { get; set; }
        public int? ParentId { get; set; }
        public string? ParentDisplay { get; set; }
        public bool IsSelected { get; set; }
    }
}
