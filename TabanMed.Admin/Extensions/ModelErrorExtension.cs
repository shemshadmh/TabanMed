using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TabanMed.Admin.Extensions
{
    public static class ModelErrorExtension
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(e => e.Errors).Select(error => error.ErrorMessage).ToList();
        }
    }
}
