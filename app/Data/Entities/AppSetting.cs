using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities;

/// <summary>
/// Application settings and user prefrences stored in the database.
/// </summary>
public sealed class AppSetting
{
    /// <summary>
    /// The setting to be stored. This a unique in the database.
    /// </summary>
    [Key]
    public string Setting { get; set; } = default!;

    /// <summary>
    /// The value for the setting. Stored as a string. Casting may be necesary to retrieve.
    /// </summary>
    [Required]
    public string Value { get; set; } = default!;
}
