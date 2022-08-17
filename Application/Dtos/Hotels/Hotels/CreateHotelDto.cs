using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Common;
using Microsoft.AspNetCore.Http;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.Hotels;
public class CreateHotelDto
{
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
        Name = nameof(DataDictionary.About))]
    [MaxLength(length: ModelConstants.Hotel.AboutMaxLength,
        ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
    public string? About { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.HotelStars))]
    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public int Stars { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Address))]
    [MaxLength(length: ModelConstants.Hotel.AddressMaxLength,
        ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
    public string? Address { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Phone))]
    [MaxLength(length: ModelConstants.Hotel.CallInformationMaxLength,
        ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
    public string? CallInformation { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.WebAddress))]
    [MaxLength(length: ModelConstants.Hotel.WebsiteAddressMaxLength,
        ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
    public string? WebsiteAddress { get; set; }

    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public int CityId { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.MainImage))]
    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    [ImageFile]
    [FileMaxSizeLimit(AppConstants.MaxHotelLogoFileSizeUpload)]
    [FileExtensionsLimit(".jpg,.png,.jpeg", ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.FileExtension))]
    [DataType(DataType.Upload)]
    public IFormFile HotelPic { get; set; } = null!;
}