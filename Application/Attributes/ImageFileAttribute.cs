using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
