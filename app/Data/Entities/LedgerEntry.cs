using System.ComponentModel.DataAnnotations;
using App.Constants;

namespace App.Data.Entities;

/// <summary>
/// Represents a transaction entry in the database.
/// This includes Income and Expenses.
/// </summary>
public class LedgerEntry
{
    /// <summary>
    /// Unique identifider of the entry.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Type is Income/Expense from enum. Stored as int.
    /// See <see cref="LedgerEntryType"/> for more details.
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// Category list will be populated based on type in the dropdown.
    /// Examples can be "Rent, Salary"/
    /// </summary>
    [Required]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The amount of the entry (Money).
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Timestamp of creation time.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date of when the transaction occurred. This can be in the past since
    /// its a record of the date. Different than <see cref="CreatedAt"/> which 
    /// is when the entry was added to the database.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Optional notes to store. Extra information about the entry.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if the entry is supposed to be recurring.
    /// </summary>    
    public bool Recurring { get; set; } = false;

    /// <summary>
    /// If <see cref="Recurring"/> is true, this will be populated with
    /// the frequency type using the index from <see cref="AppConfig.TransactionFrequency"/>
    /// </summary>
    public int? Frequency { get; set; } = null;
}


