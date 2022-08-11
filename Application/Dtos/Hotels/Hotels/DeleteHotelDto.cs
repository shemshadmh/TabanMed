using System.ComponentModel.DataAnnotations;
using Resources.ErrorMessages;

namespace Application.Dtos.Hotels.Hotels;

public class DeleteHotelDto
{
    [Required(ErrorMessageResourceType = typeof(ErrorMessages),
        ErrorMessageResourceName = nameof(ErrorMessages.Required))]
    public int Id { get; set; }
}