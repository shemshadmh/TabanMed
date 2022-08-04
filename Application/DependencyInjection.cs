using System.Reflection;
using Application.Common.Mapster.HotelFacilities;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            #region Mapster Configurations

            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(HotelFacilitiesConfigurations))!);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            #endregion

            return services;
        }
    }
}
