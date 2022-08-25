using System.ComponentModel.DataAnnotations;

namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class EditSystemUserPasswordDto
    {
        public string Id { get; set; }

        // *************************** //

        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [DataType(DataType.Password)]
        [MinLength(8,
            ErrorMessage = "حداقل تعداد کارکتر  {0} باید {1} باشد!")]
        public string Password { get; set; }

        // *************************** //

        [Display(Name = "تکرار رمز عبور")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),
            ErrorMessage = "{0} و {1} مطابق نیستند !")]
        public string RePassword { get; set; }
    }
}
