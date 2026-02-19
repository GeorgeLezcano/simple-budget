using Microsoft.EntityFrameworkCore.Design;

namespace App.Data;

/// <summary>
/// Factory for Database context helper.
/// Design-time only (IDesignTimeDbContextFactory) so EF CLI can 
/// create a context without the app running.
/// </summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    /// Creates the database context.
    /// </summary>
    /// <returns>The database context</returns>
    public AppDbContext CreateDbContext(string[] args)
    {
        string dbPath = DatabasePaths.DbFilePath;
        var options = DbContextOptionsFactory.Create(dbPath, createIfMissing: false);
        return new AppDbContext(options);
    }
}
