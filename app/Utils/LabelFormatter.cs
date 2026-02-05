namespace App.Utils;

/// <summary>
/// Utility class to format labels across the application.
/// </summary>
public static class LabelFormatter
{
    /// <summary>
    /// Sets the shell label text including the version.
    /// </summary>
    /// <param name="text">Text to display</param>
    /// <returns>Formatted shell label</returns>
    public static string SetAppShellText(string text)
    {
        return $"{text} {XmlHelpers.GetAppVersion()}";
    }

}