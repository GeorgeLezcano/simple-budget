using App.Constants;
using App.Data.Entities;

namespace App.Services;

/// <summary>
/// Service to handle database requests for settings.
/// </summary>
/// <param name="db">Database context provider</param>
public class SettingsService(DbContextProvider db)
{
    private readonly DbContextProvider _db = db;

    /// <summary>
    /// Updates a setting if it exists with provided value.
    /// If its not on the database, an entry is created for it.
    /// </summary>
    /// <param name="request">Request settings</param>
    /// <returns>True if request is successful, False otherwise.</returns>
    public bool Set(AppSetting request)
    {
        if (request is null) return false;
        if (string.IsNullOrWhiteSpace(request.Setting)) return false;

        try
        {
            using var context = _db.CreateContext();

            var entity = context.AppSettings.Find(request.Setting);

            if (entity is null)
            {
                context.AppSettings.Add(new AppSetting
                {
                    Setting = request.Setting,
                    Value = request.Value
                });
            }
            else
            {
                entity.Value = request.Value;
            }

            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Retrives the value of the provided setting
    /// </summary>
    /// <param name="Key">The settings Key in the database</param>
    /// <returns>The settings value</returns>
    public bool TryGetValue(string Key, out string value)
    {
        value = string.Empty;

        try
        {
            using var context = _db.CreateContext();

            var entity = context.AppSettings.Find(Key);

            if (entity is not null)
                value = entity.Value;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}