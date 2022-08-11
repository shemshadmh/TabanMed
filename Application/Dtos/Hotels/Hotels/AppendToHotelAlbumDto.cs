using System.ComponentModel.DataAnnotations;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.Hotels;

public class AppendToHotelAlbumDto
{
    public int HotelId { get; set; }

    [Display(ResourceType = typeof(DataDictionary),
        Name = nameof(DataDictionary.ImageAlt))]
    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public string ImageAlt { get; set; } = null!;

    //[Display(ResourceType = typeof(DataDictionary),
    //    Name = nameof(DataDictionary.HotelAlbumItem))]
    //[Required(ErrorMessageResourceType = typeof(ErrorMessages),
    //    ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    //[ImageFile]
    //[FileMaxSizeLimit(StaticDetails.MaxTransportCoLogoFileSizeUpload)]
    //[FileExtensionsLimit(".jpg,.png,.jpeg", ErrorMessageResourceType = typeof(ErrorMessages),
    //    ErrorMessageResourceName = nameof(ErrorMessages.FileExtension))]
    //[DataType(DataType.Upload)]
    //public IFormFile ImageFile { get; set; } = null!;
}