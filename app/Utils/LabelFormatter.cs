using App.Constants;

namespace App.Utils;

/// <summary>
/// Utility class to format labels across the application.
/// </summary>
public static class LabelFormatter
{
    public static Language SelectedLanguage { get; set; } = AppConfig.DefaultLanguage;

    /// <summary>
    /// Gets the shell label text including the version.
    /// </summary>
    /// <param name="text">Text to display</param>
    /// <returns>Formatted shell label</returns>
    public static string AppShellText()
    {
        return $"{AppConfig.ShellText} v{XmlHelpers.GetAppVersion()}";
    }
    
}