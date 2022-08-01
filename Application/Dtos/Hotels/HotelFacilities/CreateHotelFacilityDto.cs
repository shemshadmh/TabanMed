
using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.HotelFacilities
{
    public class CreateHotelFacilityDto
    {
        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Title))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string Title { get; set; } = null!;

        public int ParentId { get; set; }
    }
}
