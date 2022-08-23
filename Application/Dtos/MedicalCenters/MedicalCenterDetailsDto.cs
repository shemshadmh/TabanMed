
namespace Application.Dtos.MedicalCenters
{
    public class MedicalCenterDetailsDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? AgentPhoneNumber { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public ICollection<MedicalCenterForEditDetailsDto>? MedicalCenterForEditDetailsDto { get; set; }


    }
}
