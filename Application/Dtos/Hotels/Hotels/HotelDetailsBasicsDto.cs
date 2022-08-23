using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Attributes;
using Application.Extensions;
using Common;
using Microsoft.AspNetCore.Http;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.Hotels
{
    public class HotelDetailsBasicsDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;



        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Phone))]
        [MaxLength(length: ModelConstants.Hotel.CallInformationMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? CallInformation 
        {
            get => _callInformation;
            set => _callInformation = value.ToEnglishNumbers();
        }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.HotelStars))]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int Stars { get; set; }


        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.WebAddress))]
        [MaxLength(length: ModelConstants.Hotel.WebsiteAddressMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        [RegularExpression(AppConstants.EnglishRegex,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.EnterJustEnglishCharacters))]
        public string? WebsiteAddress { get; set; }


        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.MainImage))]
       
        [ImageFile]
        [FileMaxSizeLimit(AppConstants.MaxHotelLogoFileSizeUpload)]
        [FileExtensionsLimit(".jpg,.png,.jpeg", ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.FileExtension))]
        [DataType(DataType.Upload)]
        public IFormFile? HotelPic { get; set; } = null!;







        

        #region Private properties

        private string _callInformation = null!;
        

        #endregion
    }
}
