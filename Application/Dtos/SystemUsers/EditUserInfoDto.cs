
using System.ComponentModel.DataAnnotations;

namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class EditUserInfoDto
    {
        public string Id { get; set; }

        // *************************** //

        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 50,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        public string Name { get; set; }

        // *************************** //

        [Display(Name = "نام خانوادگی")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 100,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        public string Family { get; set; }

        // *************************** //

        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 20,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        [RegularExpression("^[A-Za-z ]*$",
            ErrorMessage = "{0} را به صورت کارکتر انگلیسی وارد کنید !")]
        public string Username { get; set; }
    }
}
