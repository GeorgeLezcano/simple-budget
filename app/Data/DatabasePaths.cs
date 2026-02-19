using App.Constants;
using Microsoft.Data.Sqlite;

namespace App.Data;

/// <summary>
/// Helper class for local database paths and names.
/// </summary>
public static class DatabasePaths
{
    /// <summary>
    /// Directory where the database is located.
    /// </summary>
    public static string DataDirectory =>
        Path.Combine(AppContext.BaseDirectory, DBInfo.DBDirectory);

    /// <summary>
    /// Full path for the database.
    /// </summary>
    public static string DbFilePath =>
        Path.Combine(DataDirectory, DBInfo.DBName);


    /// <summary>
    /// Creates the connection string for the database.
    /// </summary>
    /// <param name="dbPath">Path of the database</param>
    /// <param name="createIfMissing">Indicates if connection string should be created when missing</param>
    /// <returns>The connection string</returns>
    public static string BuildConnectionString(string dbPath, bool createIfMissing = true)
    {
        return new SqliteConnectionStringBuilder
        {
            DataSource = dbPath,
            Mode = createIfMissing ? SqliteOpenMode.ReadWriteCreate : SqliteOpenMode.ReadWrite,
            Cache = SqliteCacheMode.Shared
        }.ToString();
    }
}
