using App.Constants;
using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
    public bool AddLedgerEntry(LedgerEntry entry)
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
    public bool RemoveLedgerEntry(Guid id)
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

    /// <summary>
    /// Get all the categories of the provided type.
    /// See <see cref="LedgerEntryType"/> for more details.
    /// </summary>
    /// <param name="type">The type of category</param>
    /// <returns>A list of strings containing all categories.</returns>
    public List<string> GetCategories(LedgerEntryType type)
    {
        using var context = _db.CreateContext();

        return [.. context.Categories
            .AsNoTracking()
            .Where(c => c.Type == (int)type)
            .OrderBy(c => c.Name)
            .Select(c => c.Name)];
    }

    /// <summary>
    /// Attempts to add a new category to the database. If it already exists, 
    /// it will be ignored and still consider success.
    /// </summary>
    /// <param name="category">The category</param>
    /// <returns>True if operation is successful, False otherwise</returns>
    public bool AddCategory(TransactionCategory category)
    {
        try
        {
            using var context = _db.CreateContext();

            if (context.Categories.Find(category.Name) is not null)
                return true;

            context.Categories.Add(category);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to remove a new category from the database.
    /// </summary>
    /// <param name="category">The category</param>
    /// <returns>True if operation is successful, False otherwise</returns>
    public bool RemoveCategory(string category)
    {
        try
        {
            using var context = _db.CreateContext();

            var entity = context.Categories.Find(category);
            if (entity is null) return false;

            context.Categories.Remove(entity);
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
