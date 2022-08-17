
using Application;
using Persistence;
using Serilog;
using TabanMed.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
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


#region Static Folders Ckeck

var webEnv = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

try
{
    var pathList = new List<string>
    {
        AppConstants.HotelsPhotoPath,
        Path.Combine(AppConstants.HotelsPhotoPath,AppConstants.ThumbnailPath)
    };

    foreach(var item in pathList)
    {
        var fullPath = Path.Combine(webEnv.WebRootPath, item);
        if(!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);
    }


    Log.Information("storage directories check succeeded.");
}
catch(Exception ex)
{
    Log.Fatal(ex, "An error occurred while validating fibato admin storage directories.");
    Log.CloseAndFlush();
}

#endregion

app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Dashboard}/{action=Index}/{id?}");
});
Log.Logger.Warning("Using {env} Configuration.", app.Environment.EnvironmentName);

app.Run();
