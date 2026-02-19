using Microsoft.EntityFrameworkCore;
using App.Data;

namespace App.Services;

/// <summary>
///  Service to handle database requests.
/// </summary>
/// <param name="dbPath">The path of the database.</param>
public sealed class DatabaseService(string dbPath)
{
    private readonly string _dbPath = dbPath;

    /// <summary>
    /// Creates the database context.
    /// </summary>
    /// <returns>DB Context</returns>
    public AppDbContext CreateContext()
    {
        string connectionString = DatabasePaths.BuildConnectionString(_dbPath, createIfMissing: true);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
