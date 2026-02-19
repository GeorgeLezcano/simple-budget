using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Data;

/// <summary>
/// Factory for Database context.
/// </summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    /// Creates the database context.
    /// </summary>
    /// <returns>The database context</returns>
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbPath = DatabasePaths.DbFilePath;

        string connectionString = DatabasePaths.BuildConnectionString(dbPath, createIfMissing: true);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .EnableSensitiveDataLogging(false)
            .Options;

        return new AppDbContext(options);
    }
}
