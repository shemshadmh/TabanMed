using System.ComponentModel.DataAnnotations;
using Common;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.TourServices
{
    public class CreateTourServicesDto
    {
        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.FaTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.TourService.TitleMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string FaTitle { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.FaDescription))]
        [MaxLength(length: ModelConstants.TourService.DescriptionMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? FaDescription { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.EnTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.TourService.TitleMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string EnTitle { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.EnDescription))]
        [MaxLength(length: ModelConstants.TourService.DescriptionMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? EnDescription { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.ArTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.TourService.TitleMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string ArTitle { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.ArDescription))]
        [MaxLength(length: ModelConstants.TourService.DescriptionMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? ArDescription { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AfTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.TourService.TitleMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string AfTitle { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AfTitle))]
        [MaxLength(length: ModelConstants.TourService.DescriptionMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? AfDescription { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.PriceInDollar))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [Range(0, 32760,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int Price { get; set; }
    }
}
