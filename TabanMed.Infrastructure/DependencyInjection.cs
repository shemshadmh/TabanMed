using Application.Interfaces.Application;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TabanMed.Infrastructure.Services;
using TabanMed.Infrastructure.Services.Globalization;

namespace TabanMed.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            #region Application Services

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDateTime, MachineDateTime>();

            #endregion

            services.ConfigureServicesCultureLocalization();

            return services;
        }
    }
}
