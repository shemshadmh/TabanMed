

namespace Application.Dtos.MedicalCenters
{
    public  class CityWithMedicalCenterCount
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int MedicalCenterCount { get; set; }
    }
}
