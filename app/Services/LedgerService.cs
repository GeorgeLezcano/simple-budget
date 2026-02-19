using App.Data.Entities;

namespace App.Services;

/// <summary>
/// Service to handle database requests for transactions.
/// </summary>
/// <param name="db">Database context provider</param>
public sealed class LedgerService(DbContextProvider db)
{
    private readonly DbContextProvider _db = db;

    /// <summary>
    /// Attempts to add a new entry to the database.
    /// This is for both Income and Expenses.
    /// </summary>
    /// <param name="entry">The ledger entry</param>
    /// <returns>True if the request is successful; False otherwise.</returns>
    public bool TryAddLedgerEntry(LedgerEntry entry)
    {
        try
        {
            using var context = _db.CreateContext();

            context.LedgerEntries.Add(entry);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to remove an entry from the database.
    /// This is for both Income and Expenses.
    /// </summary>
    /// <param name="id">The id of the entry</param>
    /// <returns>True if the request is successful; False otherwise.</returns>
    public bool TryRemoveLedgerEntry(Guid id)
    {
        try
        {
            using var context = _db.CreateContext();

            var entity = context.LedgerEntries.Find(id);
            if (entity is null) return false;

            context.LedgerEntries.Remove(entity);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
