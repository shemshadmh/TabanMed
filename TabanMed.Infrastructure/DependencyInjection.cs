using Application.Interfaces.Application;
using Application.Interfaces.Destination;
using Application.Interfaces.Hotels;
using Application.Interfaces.MedicalCenters;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TabanMed.Infrastructure.Services;
using TabanMed.Infrastructure.Services.Application;
using TabanMed.Infrastructure.Services.Destination;
using TabanMed.Infrastructure.Services.Globalization;
using TabanMed.Infrastructure.Services.Hotels;
using TabanMed.Infrastructure.Services.MedicalCenters;

namespace TabanMed.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            #region Application Services

            services.AddScoped<ICurrentServices, CurrentServices>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddScoped<IFileManagerService, FileManagerService>();

            #endregion

            #region Hotels

            services.AddTransient<IHotelFacilityApplication, HotelFacilityApplication>();
            services.AddTransient<IHotelApplication, HotelApplication>();

            #endregion
            #region Countries
            services.AddTransient<ICountryApplication, CountryApplication>();
            #endregion
            #region Cities
            services.AddTransient<ICityApplication, CityApplication>();

            #endregion

            #region Medical Center

            services.AddTransient<IMedicalCenterApplication, MedicalCenterApplication>();

            #endregion

            services.ConfigureServicesCultureLocalization();

            return services;
        }
    }
}
