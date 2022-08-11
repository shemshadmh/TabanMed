using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.Hotels;
public class EditHotelDto
{
    public int Id { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.FaName))]
    [Required(AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public string FaName { get; set; } = null!;

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.EnName))]
    [Required(AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    [RegularExpression("^[A-Za-z ]*$",
        ErrorMessage = "{0} را به صورت کارکتر انگلیسی وارد کنید !")]
    public string EnName { get; set; } = null!;

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.About))]
    public string? About { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.HotelStars))]
    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public int Stars { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.HotelOtherNames))]
    public string? OtherNames { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.HotelRoomCount))]
    public int? RoomCount { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Address))]
    public string? Address { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.Phone))]
    public string? CallInformation { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.WebAddress))]
    public string? WebsiteAddress { get; set; }

    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public int CityId { get; set; }

    //[Location(ErrorMessageResourceType = typeof(ErrorMessages),
    //    ErrorMessageResourceName = nameof(ErrorMessages.ChooseLocationOnTheMap))]
    //public LocationDto Location { get; set; } = null!;

    //[Display(ResourceType = typeof(DataDictionary),
    //    Name = nameof(DataDictionary.HotelLogo))]
    //[ImageFile]
    //[FileMaxSizeLimit(StaticDetails.MaxTransportCoLogoFileSizeUpload)]
    //[FileExtensionsLimit(".jpg,.png,.jpeg", ErrorMessageResourceType = typeof(ErrorMessages),
    //    ErrorMessageResourceName = nameof(ErrorMessages.FileExtension))]
    //[DataType(DataType.Upload)]

    //public IFormFile? HotelPic { get; set; }

    //[BindNever] public string? ImageUrl { get; set; }
}