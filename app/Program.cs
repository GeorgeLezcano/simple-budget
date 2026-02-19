using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using App.Services;

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

        bool firstRun = DatabaseBootstrapper.EnsureDatabaseReady(out var dbPath);

        var services = new ServiceCollection();

        services.AddSingleton(new DatabaseService(dbPath));
        services.AddSingleton<LedgerService>();
        services.AddSingleton<SettingsService>();

        services.AddTransient<MainForm>();

        using var serviceProvider = services.BuildServiceProvider();

        Application.Run(serviceProvider.GetRequiredService<MainForm>());
    }
}