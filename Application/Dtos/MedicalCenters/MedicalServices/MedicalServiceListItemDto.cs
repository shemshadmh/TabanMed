

using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.MedicalCenters.MedicalServices
{
    public class MedicalServiceListItemDto
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.FaTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string FaTitle { get; set; } = null!;


        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.EnTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string EnTitle { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.ArTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string ArTitle { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AfTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: 100, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string AfTitle { get; set; } = null!;

        public int? ParentId { get; set; }
    }
}
