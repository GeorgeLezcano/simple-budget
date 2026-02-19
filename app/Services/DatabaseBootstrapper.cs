using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Constants;

namespace App.Services;

/// <summary>
/// Boostrap service for the database. Ensures that its ready on startup and fully functional.
/// It creates a "fresh" database if its not found.
/// </summary>
public static class DatabaseBootstrapper
{
    /// <summary>
    /// Ensures the SQLite database exists, is readable, and matches the latest schema.
    /// Returns true if this is effectively a "first time run" (missing or reset due to corruption).
    /// </summary>
    public static bool EnsureDatabaseReady(out string dbPath)
    {
        dbPath = DatabasePaths.DbFilePath;

        Directory.CreateDirectory(DatabasePaths.DataDirectory);

        if (!File.Exists(dbPath))
        {
            CreateFreshDatabase(dbPath);
            return true;
        }

        if (!IsDatabaseHealthy(dbPath, out var healthError))
        {
            var msg =
                "Your local database appears to be corrupted or unreadable.\n\n" +
                "The app will reset it now (your old DB will be backed up).\n\n" +
                $"Details: {healthError}";

            MessageBox.Show(msg, "SimpleBudget - Database Reset",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            BackupAndDelete(dbPath);
            CreateFreshDatabase(dbPath);
            return true;
        }

        ApplyMigrations(dbPath);
        return false;
    }

    /// <summary>
    /// Initializes a fresh database.
    /// </summary>
    /// <param name="dbPath">The path of the database.</param>
    private static void CreateFreshDatabase(string dbPath)
    {
        ApplyMigrations(dbPath);
    }

    /// <summary>
    /// Applies migrations to the database.
    /// </summary>
    /// <param name="dbPath">The path of the database.</param>
    private static void ApplyMigrations(string dbPath)
    {
        using var db = CreateDbContext(dbPath);
        db.Database.Migrate();
    }

    /// <summary>
    /// Creates the Database Context. 
    /// See <see cref="AppDbContext"/> for reference.
    /// </summary>
    /// <param name="dbPath">The path of the database.</param>
    /// <returns>The database context</returns>
    private static AppDbContext CreateDbContext(string dbPath)
    {
        string connectionString = DatabasePaths.BuildConnectionString(dbPath, createIfMissing: true);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connectionString)
            .EnableSensitiveDataLogging(false)
            .Options;

        return new AppDbContext(options);
    }

    /// <summary>
    /// Checks if the database is ready for use.
    /// </summary>
    /// <param name="dbPath">The path of the database.</param>
    /// <param name="error">Error message</param>
    /// <returns>True if the database is ready; False otherwise.</returns>
    private static bool IsDatabaseHealthy(string dbPath, out string error)
    {
        try
        {
            string connectionString = DatabasePaths.BuildConnectionString(dbPath, createIfMissing: false);

            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            //TODO Simple check, think about this more, what does it mean to be "healthy"
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' LIMIT 1;";
            _ = cmd.ExecuteScalar();

            error = "";
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Stores a backup of the corrupted database with different name
    /// and timestamp.
    /// </summary>
    /// <param name="dbPath">The path of the database.</param>
    private static void BackupAndDelete(string dbPath)
    {
        string ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        try
        {
            var backupPath = Path.Combine(DatabasePaths.DataDirectory, $"Backup_{ts}_{DBInfo.DBName}");

            File.Copy(dbPath, backupPath, overwrite: true);
        }
        catch { /* If backup fails, still try to proceed with delete/recreate.*/ }

        try
        {
            File.Delete(dbPath);
        }
        catch
        {
            var movedPath = Path.Combine(DatabasePaths.DataDirectory, $"Locked_{ts}_{DBInfo.DBName}");
            File.Move(dbPath, movedPath, overwrite: true);
        }
    }
}
