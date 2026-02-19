using App.Data.Entities;

namespace App.Services;

public sealed class LedgerService(DatabaseService db)
{
    private readonly DatabaseService _db = db;

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
