
using Resources.UIElements;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static string NotingRegisteredIfNull(this string? text)
        {
            return string.IsNullOrEmpty(text) ? UIElements.NotingRegistered : text;
        }

        public static string ToEnglishNumbers(this string? input)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            string englishNumbers = "";

            for(int i = 0; i < input.Length; i++)
            {
                if(char.IsDigit(input[i]))
                {
                    englishNumbers += char.GetNumericValue(input, i);
                }
                else
                {
                    englishNumbers += input[i].ToString();
                }
            }
            return englishNumbers;
        }
    }
}
