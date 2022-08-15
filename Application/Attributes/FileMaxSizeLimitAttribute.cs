
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Application.Extensions;
using Microsoft.AspNetCore.Http;

namespace Application.Attributes
{
    public class FileMaxSizeLimitAttribute : ValidationAttribute
    {
        private readonly double MaxSize;

        public FileMaxSizeLimitAttribute(double maxSize)
        {
            MaxSize = maxSize;
        }

        public override string FormatErrorMessage(string name) =>
            string.Format(CultureInfo.CurrentCulture, ErrorMessageString, Math.Round(MaxSize / 1048576.0));

        public override bool IsValid(object? value)
        {
            if(value is not IFormFile file) return true;
            var fileExtension = file.GetFileSizeMb();
            return fileExtension <= (MaxSize / 1048576.0);
        }

    }
}
