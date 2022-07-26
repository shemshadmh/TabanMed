﻿using System.ComponentModel.DataAnnotations;
using Application.Extensions;
using Microsoft.AspNetCore.Http;
using Resources.ErrorMessages;

namespace Application.Attributes
{
    public class ImageFileAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is IFormFile file && !file.IsValidImage())
            {
                return new ValidationResult(ErrorMessages.IsNotAValidImageFile);
            }

            return ValidationResult.Success;
        }

    }
}
