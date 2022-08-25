using Application;
using Application.Interfaces.Initializer;
using Persistence;
using Serilog;
using TabanMed.Admin.Controllers;
using TabanMed.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration, builder.Environment)
    .AddApplication();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddKendo();

builder.Services.AddAntiforgery(opts =>
{
    opts.Cookie.Name = "AntFg";
    opts.HeaderName = "X-XSRF-Token";
});

var app = builder.Build();
using IServiceScope scope = app.Services.CreateScope();
#region Database Initializer

var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

// databases
try
{
    await dbInitializer.Initialize(typeof(BaseAdminController), app.Environment.IsDevelopment());
    Log.Information("Application db Initialized successfully on Environment {env}.",
        app.Environment.IsDevelopment() ? "Development" : "Production");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Db Initializer Failed! Please Check migrations or database connection!");
    Log.CloseAndFlush();
    // stop application
}

#endregion

#region Static Folders Ckeck

var webEnv = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

try
{
    var pathList = new List<string>
    {
        // Hotel
        AppConstants.HotelsPhotoPath,
        Path.Combine(AppConstants.HotelsPhotoPath, AppConstants.ThumbnailPath),

        // MedicalCenters
        AppConstants.MedicalCentersPhotoPath,
        Path.Combine(AppConstants.MedicalCentersPhotoPath, AppConstants.ThumbnailPath)
    };

    foreach (var item in pathList)
    {
        var fullPath = Path.Combine(webEnv.WebRootPath, item);
        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);
    }


    Log.Information("storage directories check succeeded.");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An error occurred while validating fibato admin storage directories.");
    Log.CloseAndFlush();
}

#endregion

app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Dashboard}/{action=Index}/{id?}");
});
Log.Logger.Warning("Using {env} Configuration.", app.Environment.EnvironmentName);

app.Run();