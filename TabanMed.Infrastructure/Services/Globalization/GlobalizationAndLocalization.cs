
using System.Globalization;
using Application;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace TabanMed.Infrastructure.Services.Globalization
{
    public static class GlobalizationAndLocalization
    {
        /// <summary>
        /// configure globalization and localization for resources and UI
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServicesCultureLocalization(this IServiceCollection services)
        {
            CultureInfo[] supportedCultures = new[]
            {
                new CultureInfo(AppConstants.FaIrCulture)
                {
                    NumberFormat =
                    {
                        CurrencySymbol = "تومان ", // currency display symbol
                        CurrencyDecimalSeparator = ".",
                        NumberDecimalSeparator = ".",
                        CurrencyDecimalDigits = 2 // decimal places digits (ex: 1000/00)
                    }
                    // fow now, show persian date's by default
                    //DateTimeFormat = DateTimeFormatInfo.GetInstance(CultureInfo.GetCultureInfoByIetfLanguageTag("fa"))
                    // DateTimeFormatInfo.CurrentInfo
                },

                new CultureInfo(AppConstants.EnUsCulture)
                {
                    NumberFormat =
                    {
                        CurrencySymbol = "Toman "
                    }
                },
            };
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddRequestLocalization(options =>
            {
                options.DefaultRequestCulture =
                    new RequestCulture(AppConstants.FaIrCulture, AppConstants.FaIrCulture);
                options.ApplyCurrentCultureToResponseHeaders = true;
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });
        }
    }
}
