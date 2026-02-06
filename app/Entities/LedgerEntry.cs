namespace App.Entities;

/// <summary>
/// Represents a transaction entry in the database.
/// This includes Income and Expenses.
/// </summary>
public class LedgerEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int Type { get; set; } //Type is Income/Expense from enum. Stored as int

    public string Category { get; set; } = string.Empty; //Category list will be populated based on type in the dropdown. 

    public double Amount { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow; 

    public string Notes { get; set; } = string.Empty;

    public bool Recurring { get; set; } = false;

    public string? Frequency { get; set; } = null; //Get from Frequency enum if Recurring is true
}

public enum LedgerEntryType
{
    Income,
    Expense
}

public enum Frequency
{
    Weekly,
    BiWeekly,
    Monthly,
    Quarterly,
    Yearly
}
