namespace App.Constants;

/// <summary>
/// Configuration for the Application.
/// Default values to be loaded on startup.
/// </summary>
public static class AppConfig
{
    #region Defaults
    public const string ShellText = "Simple Budget";
    public const string IconName = "$this.Icon";
    public const Language DefaultLanguage = Language.ENGLISH;

    #endregion

    #region UI Theme

    public static readonly Color ThemeBack = Color.FromArgb(32, 32, 32);
    public static readonly Color ThemePanel = Color.FromArgb(45, 45, 48);
    public static readonly Color ThemeInput = Color.FromArgb(55, 55, 58);
    public static readonly Color ThemeText = Color.Gainsboro;
    public static readonly Color ThemeMuted = Color.FromArgb(170, 170, 170);
    public static readonly Color ThemeBorder = Color.FromArgb(80, 80, 80);

    // Accents
    public static readonly Color ThemeAccent = Color.FromArgb(33, 140, 79);      // green buttons
    public static readonly Color ThemeAccentHover = Color.FromArgb(40, 160, 92);
    public static readonly Color ThemeAccentDown = Color.FromArgb(28, 120, 68);

    public static readonly Color ThemeTabActive = Color.FromArgb(0, 122, 204);   // blue selected tab header
    public static readonly Color ThemeTabInactive = Color.FromArgb(55, 55, 58);

    // Simple hover colors (slightly brighter)
    public static readonly Color ThemeTabHoverActive = Color.FromArgb(15, 135, 215);
    public static readonly Color ThemeTabHoverInactive = Color.FromArgb(70, 70, 74);

    #endregion
}