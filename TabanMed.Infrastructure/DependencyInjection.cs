using Application.Extensions;
using Application.Interfaces.Application;
using Application.Interfaces.Destination;
using Application.Interfaces.Hotels;
using Application.Interfaces.MedicalCenters;
using Application.Interfaces.Users;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TabanMed.Infrastructure.Services;
using TabanMed.Infrastructure.Services.Application;
using TabanMed.Infrastructure.Services.Destination;
using TabanMed.Infrastructure.Services.Globalization;
using TabanMed.Infrastructure.Services.Hotels;
using TabanMed.Infrastructure.Services.MedicalCenters;
using TabanMed.Infrastructure.Services.Users;

namespace TabanMed.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            #region users

            services.AddScoped<IUserApplication, UsersApplication>();

            #endregion

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
            services.AddTransient<IMedicalServiceApplication, MedicalServiceApplication>();

            #endregion

            services.ConfigureServicesCultureLocalization();
            services.AddApplicationCookieConfiguration(webHostEnvironment);

            return services;
        }

        private static IServiceCollection AddApplicationCookieConfiguration(this IServiceCollection services,
            IWebHostEnvironment environment)
        {
            services.ConfigureApplicationCookie(options =>
            {
                // cookie is only available to servers, The browser only sends the cookie but cannot access it through JavaScript.
                options.Cookie.HttpOnly = true;
                // secure & limit the cookie to HTTPS / HTTP (optional in development [in SameAsRequest], required for production [in Always].)
                options.Cookie.SecurePolicy = environment.IsProduction()
                    ? CookieSecurePolicy.SameAsRequest
                    : CookieSecurePolicy.None;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
                options.Cookie.Name = "TabanMedCo";
                options.LoginPath = new PathString("/Login");
                options.AccessDeniedPath = "/Error?statusCode=403";
                options.LogoutPath = "/Account/Logout";
                options.ReturnUrlParameter = "returnUrl";
                // strict is only for a single site, cross-site requests and oauth need Lax
                if (environment.IsProduction())
                    options.Cookie.SameSite = SameSiteMode.Lax;

                options.Events = new CookieAuthenticationEvents
                {
                    // execute every time to check user changes and cookie validation. must login again if user was changed in db (usually change a role, info, claim, ... by admin or self). except admin.
                    OnValidatePrincipal = async context =>
                    {
                        var userPrincipal = context.Principal;
                        if (userPrincipal != null)
                        {
                            var userId = userPrincipal.GetUserId();
                            var statusClaim = userPrincipal.GetSecurityStamp();

                            var userService = context.HttpContext.RequestServices
                                .GetRequiredService<IUserApplication>();

                            var dbSecurityStamp = await userService.GetUserSecurityStamp(userId!);

                            if (dbSecurityStamp != statusClaim)
                            {
                                context.RejectPrincipal();
                                await context.HttpContext.SignOutAsync();
                            }
                        }
                    }
                };
            });
            services.ConfigureServicesCultureLocalization();
            return services;
        }
    }
}