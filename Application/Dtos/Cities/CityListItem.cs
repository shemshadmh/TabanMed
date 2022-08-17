using Common;
using Resources.DataDictionary;
using Resources.ErrorMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Cities
{
    public class CityListItem

    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.FaTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.Country.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string FaName { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.EnTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.Country.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string EnName { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.ArTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.Country.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string ArName { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AfTitle))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.Country.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string AfName { get; set; } = null!;

        public int CountryId { get; set; }
    }
}
