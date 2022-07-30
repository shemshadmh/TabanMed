namespace Domain.Entities.Hotels
{
    public class HotelSelectedFacility
    {
        // Hotel
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;

        // Hotel Facility
        public int HotelFacilityId { get; set; }
        public HotelFacility HotelFacility { get; set; } = null!;
    }
}
