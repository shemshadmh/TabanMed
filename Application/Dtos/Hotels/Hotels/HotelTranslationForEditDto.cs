
using System.ComponentModel.DataAnnotations;
using Common;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.Hotels
{
    public class HotelTranslationForEditDto
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int HotelId { get; set; }

        public string? LanguageName { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Name))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.Hotel.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string Name { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Address))]
        [MaxLength(length: ModelConstants.Hotel.AddressMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? Address { get; set; }


        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.About))]
        [MaxLength(length: ModelConstants.Hotel.AboutMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? About { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int LanguageId { get; set; }
    }
}
