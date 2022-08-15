using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Application.Extensions;
using Microsoft.AspNetCore.Http;

namespace Application.Attributes
{
    public class FileExtensionsLimitAttribute : ValidationAttribute
    {
        private string[] Extensions { get; set; }

        public FileExtensionsLimitAttribute(string allowedExtensions)
        {
            Extensions = allowedExtensions.Split(',');
        }

        public override string FormatErrorMessage(string name) =>
            string.Format(CultureInfo.CurrentCulture, ErrorMessageString, string.Join(" , ", Extensions));

        public override bool IsValid(object? value)
        {
            if(value is not IFormFile file) return true;
            var fileExtension = file.GetFileExtension();
            return Extensions.Contains(fileExtension);
        }
    }
}
