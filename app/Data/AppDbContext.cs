using App.Constants;
using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data;

/// <summary>
/// Context for the database.
/// </summary>
/// <param name="options">DB options</param>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{   
    /// <summary>
    /// Application Settings table.
    /// </summary>
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();

    /// <summary>
    /// Ledger Entries table.
    /// </summary>
    public DbSet<LedgerEntry> LedgerEntries => Set<LedgerEntry>();

    /// <summary>
    /// Categories table.
    /// </summary>
    public DbSet<TransactionCategory> Categories => Set<TransactionCategory>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO Add constraints to these. Also ensure type is correct.

        modelBuilder.Entity<AppSetting>(b =>
        {
            b.ToTable(DBInfo.SettingsTable);
            b.HasKey(x => x.Setting);
            b.Property(x => x.Setting);
            b.Property(x => x.Value);
        });

        modelBuilder.Entity<LedgerEntry>(b => 
        {
            b.ToTable(DBInfo.LedgerTable);
            b.HasKey(x => x.Id);
            b.Property(x => x.Type);
            b.Property(x => x.Category);
            b.Property(x => x.Amount).HasPrecision(18, 2);
            b.Property(x => x.CreatedAt);
            b.Property(x => x.TransactionDate);
            b.Property(x => x.Notes);
            b.Property(x => x.Recurring);
            b.Property(x => x.Frequency);
        });

        modelBuilder.Entity<TransactionCategory> (b =>
        {
         b.ToTable(DBInfo.TransactionCategoryTable);
         b.HasKey(x => x.Name);
         b.Property(x => x.Type);
        });
    }
}
