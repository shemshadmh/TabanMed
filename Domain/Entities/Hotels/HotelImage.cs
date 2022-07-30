
namespace Domain.Entities.Hotels
{
    public class HotelImage
    {
        public string ImageUrl { get; set; } = null!;
        public string ImageAlt { get; set; } = null!;

        #region Relations

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;

        #endregion
    }
}
