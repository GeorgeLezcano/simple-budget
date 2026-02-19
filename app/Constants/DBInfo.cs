namespace App.Constants;

/// <summary>
/// Definitions for the database.
/// </summary>
public static class DBInfo
{
    /// <summary>
    /// Relative path for the database.
    /// </summary>
    public const string DBDirectory = "data";

    /// <summary>
    /// Database name.
    /// </summary>
    public const string DBName = "SimpleBudgetDB.db";

    /// <summary>
    /// Name of the Table for App Settings.
    /// </summary>
    public const string SettingsTable = "app_settings";

    /// <summary>
    /// Name of the table for the transactions.
    /// </summary>
    public const string LedgerTable = "ledger_entries";
}