using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Attributes;
using Common;
using Microsoft.AspNetCore.Http;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.MedicalCenters
{
    public class CreateMedicalCenterDto
    {
        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Name))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.MedicalCenter.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string Name { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Phone))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.MedicalCenter.PhoneNumberMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string PhoneNumber { get; set; } = null!;



        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AgentName))]
        [MaxLength(length: ModelConstants.MedicalCenter.AgentNameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? AgentName { get; set; }




        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AgentPhoneNumber))]
        [MaxLength(length: ModelConstants.MedicalCenter.AgentPhoneNumberMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? AgentPhoneNumber { get; set; }



        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Address))]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.MedicalCenter.AddressMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string Address { get; set; } = null!;



        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.MainImage))]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [ImageFile]
        [FileMaxSizeLimit(AppConstants.MaxMedicalCenterPicFileSizeUpload)]
        [FileExtensionsLimit(".jpg,.png,.jpeg", ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.FileExtension))]
        [DataType(DataType.Upload)]
        public IFormFile MedicalCenterPic { get; set; } = null!;


        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int CityId { get; set; }



        
        
    }
}
