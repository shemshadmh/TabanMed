
namespace Application.Dtos.Hotels.Hotels{
    public class CityWithHotelsCount
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int HotelsCount { get; set; }
    }
}
