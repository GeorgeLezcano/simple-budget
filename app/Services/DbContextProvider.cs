using App.Data;

namespace App.Services;

/// <summary>
/// Provides <see cref="AppDbContext"/> instances configured for the app's database file.
/// </summary>
/// <param name="dbPath">Full path to the SQLite database file.</param>
public sealed class DbContextProvider(string dbPath)
{
    private readonly string _dbPath = dbPath;

    /// <summary>
    /// Creates a new <see cref="AppDbContext"/> instance.
    /// Caller owns disposal of the returned context.
    /// </summary>
    public AppDbContext CreateContext()
    {
        var options = DbContextOptionsFactory.Create(_dbPath, createIfMissing: true);
        return new AppDbContext(options);
    }
}
