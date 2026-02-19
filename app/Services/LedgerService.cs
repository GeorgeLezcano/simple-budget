
namespace App.Services;

/// <summary>
/// Service to handle database requests for transactions.
/// </summary>
/// <param name="db">Database context provider</param>
public sealed class LedgerService(DbContextProvider db)
{
    private readonly DbContextProvider _db = db;

    //TODO Implement this, below is a potential use example.

    // public void AddTransaction(string category, decimal amount)
    // {
    //     using var ctx = _db.CreateContext();

    //     ctx.LedgerEntries.Add(new LedgerEntry
    //     {
    //         Id = Guid.NewGuid().ToString(),
    //         Category = category,
    //         Amount = amount,
    //         CreatedAt = DateTime.UtcNow
    //     });

    //     ctx.SaveChanges();
    // }

    // public decimal GetTotalSaved()
    // {
    //     using var ctx = _db.CreateContext();
    //     return ctx.LedgerEntries.Select(x => x.Amount).DefaultIfEmpty(0m).Sum();
    // }
}
