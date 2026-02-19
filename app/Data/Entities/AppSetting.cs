namespace App.Data.Entities;

/// <summary>
/// Application settings and user prefrences stored in the database.
/// </summary>
public sealed class AppSetting
{
    /// <summary>
    /// The setting to be stored. This a unique in the database.
    /// </summary>
    public string Setting { get; set; } = string.Empty; //TODO Define these in a class/enum to ensure they are unique.

    /// <summary>
    /// The value for the setting. Stored as a string. Casting may be necesary to retrieve.
    /// </summary>
    public string? Value { get; set; }
}
