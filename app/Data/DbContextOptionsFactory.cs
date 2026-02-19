using Microsoft.EntityFrameworkCore;

namespace App.Data;

/// <summary>
/// Helper class to create consistent Context options throughout the app.
/// </summary>
public static class DbContextOptionsFactory
{
    /// <summary>
    /// Create the DB context options.
    /// </summary>
    public static DbContextOptions<AppDbContext> Create(string dbPath, bool createIfMissing)
    {
        string cs = DatabasePaths.BuildConnectionString(dbPath, createIfMissing);

        return new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(cs)
            .EnableSensitiveDataLogging(false)
            .Options;
    }
}
