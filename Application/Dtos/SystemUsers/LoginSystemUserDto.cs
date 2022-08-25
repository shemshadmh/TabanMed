
using System.ComponentModel.DataAnnotations;

namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class LoginSystemUserDto
    {
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 20,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        [RegularExpression("^[A-Za-z ]*$",
            ErrorMessage = "{0} را به صورت کارکتر انگلیسی وارد کنید !")]
        public string UserName { get; set; }

        // *************************** //

        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [DataType(DataType.Password)]
        [MinLength(8,
            ErrorMessage = "حداقل تعداد کارکتر  {0} باید {1} باشد!")]
        public string Password { get; set; }

        // *************************** //
        public bool RememberMe { get; set; }
    }
}
