using System.ComponentModel.DataAnnotations;

namespace TabanAgency.Domain.Dtos.SystemUsers
{
    public class EditRoleDto
    {
        public string RoleId { get; set; }

        [Display(Name = "عنوان نمایشی")]
        [Required(AllowEmptyStrings = false,
            ErrorMessage = "{0} الزامی است!")]
        [MaxLength(length: 50,
            ErrorMessage = "حداکثر تعداد کارکتر  {0} باید {1} باشد!")]
        public string DisplayName { get; set; }

    }
}
