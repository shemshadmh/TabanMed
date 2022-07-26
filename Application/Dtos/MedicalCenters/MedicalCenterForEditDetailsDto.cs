﻿using System.ComponentModel.DataAnnotations;
using Common;
using Resources.DataDictionary;
using Resources.ErrorMessages;

namespace Application.Dtos.MedicalCenters
{
    public  class MedicalCenterForEditDetailsDto
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int MedicalCenterId { get; set; }

        public string? LanguageName { get; set; }

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Name))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.MedicalCenter.NameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string Name { get; set; } = null!;

        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.Address))]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        [MaxLength(length: ModelConstants.MedicalCenter.AddressMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string Address { get; set; } = null!;


        [Display(ResourceType = typeof(DataDictionary),
            Name = nameof(DataDictionary.AgentName))]
        [MaxLength(length: ModelConstants.MedicalCenter.AgentNameMaxLength,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.MaxLength))]
        public string? AgentName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = nameof(ErrorMessages.Required))]
        public int LanguageId { get; set; }

    }
}
