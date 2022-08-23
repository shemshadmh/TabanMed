
using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Application.Extensions;
using Common;
using Microsoft.AspNetCore.Http;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.MedicalCenters
{
    public class MedicalCenterDetailsBasicsDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Phone))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.MedicalCenter.PhoneNumberMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        [RegularExpression(AppConstants.EnglishRegex,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.EnterJustEnglishCharacters))]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value.ToEnglishNumbers();
        }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AgentPhoneNumber))]
        [MaxLength(length: ModelConstants.MedicalCenter.AgentPhoneNumberMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        [RegularExpression(AppConstants.EnglishRegex,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.EnterJustEnglishCharacters))]
        public string? AgentPhoneNumber
        {
            get => _agentPhoneNumber;
            set => _agentPhoneNumber = value!.ToEnglishNumbers();
        }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.MainImage))]
        [ImageFile]
        [FileMaxSizeLimit(AppConstants.MaxMedicalCenterPicFileSizeUpload)]
        [FileExtensionsLimit(".jpg,.png,.jpeg", ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.FileExtension))]
        [DataType(DataType.Upload)]
        public IFormFile? MedicalCenterPic { get; set; } = null!;


        #region Private properties

        private string _phoneNumber = null!;
        private string? _agentPhoneNumber;

        #endregion
    }
}
