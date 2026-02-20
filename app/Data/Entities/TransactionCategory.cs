namespace App.Data.Entities;

/// <summary>
/// Database model to store Categories.
/// </summary>
public class TransactionCategory
{
    /// <summary>
    /// Category display name. This is unique in the database.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Type is Income/Expense from enum. Stored as int.
    /// See <see cref="LedgerEntryType"/> for more details.
    /// </summary>
    public int Type { get; set; }
}