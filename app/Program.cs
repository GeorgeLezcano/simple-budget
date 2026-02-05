using System.Diagnostics.CodeAnalysis;

namespace App;

/// <summary>
/// Application program class.
/// </summary>
[ExcludeFromCodeCoverage]
static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
         using var mutex = new Mutex(initiallyOwned: true, name: @"Global\SimpleBudget_SingleInstance", out bool isNewInstance);
        if (!isNewInstance)
        {
            MessageBox.Show("Simple Budget is already running.", "SimpleBudget", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}