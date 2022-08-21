
using Resources.UIElements;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static string NotingRegisteredIfNull(this string? text)
        {
            return string.IsNullOrEmpty(text) ? UIElements.NotingRegistered : text;
        }
    }
}
