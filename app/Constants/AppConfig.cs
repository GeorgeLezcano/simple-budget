namespace App.Constants;

/// <summary>
/// Configuration for the Application.
/// </summary>
public static class AppConfig //TODO Consider loading from a config file? Hardocded defaults for now
{
    #region Defaults
    public const string ShellText = "Simple Budget";
    public const string IconName = "$this.Icon";
    public const string ShellName = "MainForm";
    public const int ShellWidth = 1200;
    public const int ShellHeight = 800;
    public static SizeF DefaultAutoScaleDimensions { get; } = new(8F, 20F);
    public const Language DefaultLanguage = Language.ENGLISH;

    #endregion

}