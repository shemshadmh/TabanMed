using Application.Interfaces.Application;
using Application.Interfaces.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TabanMed.Infrastructure.Services.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly IApplicationDbContext _db;
    private readonly ILogger<DbInitializer> _logger;
    private readonly IPermissionInitializer _roleClaimInitializer;


    public DbInitializer(ILogger<DbInitializer> logger, IApplicationDbContext db,
        IPermissionInitializer roleClaimInitializer)
    {
        _logger = logger;
        _db = db;
        _roleClaimInitializer = roleClaimInitializer;
    }

    public async Task Initialize(Type baseControllerType, bool isDevelopment = false)
    {
        try
        {
            var newMigrations = await _db.Database.GetPendingMigrationsAsync();
            if (newMigrations.Any())
            {
                await _db.Database.MigrateAsync();
                _logger.LogWarning("Database Initializer Migrated to latest version.");
            }
            else
            {
                _logger.LogInformation("Database Migrations is under latest version.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Database Initializer: Migrate failed!");
            throw;
        }

        await _roleClaimInitializer.Initialize(baseControllerType);

        // this method must be removed in production
        // await SeedTestData.SeedData(_db, _logger);

        ReleaseResource();
    }

    private static void ReleaseResource()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect(2, GCCollectionMode.Forced, true);
    }
}