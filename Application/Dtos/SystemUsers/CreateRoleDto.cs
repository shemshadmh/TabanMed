using System.ComponentModel.DataAnnotations;

namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class CreateRoleDto
    {
        public CreateRoleDto()
        {
            Permissions = new List<string>();
        }

        [Display(Name = "عنوان نمایشی")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 50,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        public string DisplayName { get; set; }

        [Display(Name = "نام نقش")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 50,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        [RegularExpression("^[A-Za-z ]*$",
            ErrorMessage = "{0} را به صورت کارکتر انگلیسی وارد کنید !")]
        public string Name { get; set; }

        public List<string> Permissions { get; set; }
    }
}
