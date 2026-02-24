using App.Constants;

namespace App.Utils;

/// <summary>
/// Utility class to format labels across the application.
/// Uses Control.Name (and ToolStripItem.Name) as translation keys.
/// </summary>
public static class LabelFormatter
{
    /// <summary>
    /// Current selected language for the UI.
    /// </summary>
    public static Language SelectedLanguage { get; set; } = AppConfig.DefaultLanguage;

    /// <summary>
    /// Key = Control.Name (or ToolStripItem.Name)
    /// Value = translated string
    /// </summary>
    private static readonly Dictionary<Language, Dictionary<string, string>> _labels =

    //TODO Consider not hardcoding and moving to xml file to load. At the same time, if anything
    //changes with labels, a new .exe needs to be compiled to apply the version so...does it really matter?
    
        new()
        {
            [Language.ENGLISH] = new()
            {
                ["gbDashNextSteps"] = "Next steps",
                ["lblDashHint"] =
                    "• Use Income to add money coming in.\n" +
                    "• Use Expenses to log spending (recurring or one-time).\n" +
                    "• Use Reports to filter/export.\n" +
                    "• Use Settings to manage categories (Rent, Groceries, etc.).",

                ["menuLanguage"] = "&Language",


                //TODO Add .Name to more labels in designer and add them here in english
            },

            [Language.SPANISH] = new()
            {
                ["gbDashNextSteps"] = "Próximos pasos",
                ["lblDashHint"] =
                    "• Usa Ingresos para agregar dinero que entra.\n" +
                    "• Usa Gastos para registrar gastos (recurrentes o únicos).\n" +
                    "• Usa Informes para filtrar/exportar.\n" +
                    "• Usa Configuración para administrar categorías (Alquiler, Comestibles, etc.).",

                ["menuLanguage"] = "&Idioma",


                //TODO Add .Name to more labels in designer and add them here in spanish

            }
        };

    /// <summary>
    /// Sets the application shell text.
    /// </summary>
    /// <returns>Formatted shell label</returns>
    public static string AppShellText()
    {
        return $"{AppConfig.ShellText} v{XmlHelpers.GetAppVersion()}";
    }

    /// <summary>
    /// Applies translated UI text from a root control (form),
    /// plus optional MenuStrip handling and language checkmarks.
    /// </summary>
    /// <param name="root">Root control (usually the Form)</param>
    /// <param name="menu">MenuStrip (ToolStrip items are NOT in Controls tree)</param>
    public static void Apply(Control root, MenuStrip? menu = null)
    {
        ApplyRecursive(root);

        if (menu != null)
        {
            ApplyMenu(menu);
            ApplyLanguageMenuChecks(menu);
        }
    }

    private static void ApplyRecursive(Control control)
    {
        string name = control.Name;

        if (!string.IsNullOrWhiteSpace(name) &&
            _labels.TryGetValue(SelectedLanguage, out var map) &&
            map.TryGetValue(name, out var text))
        {
            control.Text = text;
        }

        foreach (Control child in control.Controls)
            ApplyRecursive(child);
    }

    private static void ApplyMenu(MenuStrip menu)
    {
        foreach (ToolStripItem item in menu.Items)
            ApplyToolStripItem(item);
    }

    private static void ApplyToolStripItem(ToolStripItem item)
    {
        if (!string.IsNullOrWhiteSpace(item.Name) &&
            _labels.TryGetValue(SelectedLanguage, out var map) &&
            map.TryGetValue(item.Name, out var text))
        {
            item.Text = text;
        }

        if (item is ToolStripMenuItem mi)
        {
            foreach (ToolStripItem sub in mi.DropDownItems)
                ApplyToolStripItem(sub);
        }
    }

    private static void ApplyLanguageMenuChecks(MenuStrip menu)
    {
        //TODO Fragile if statement. Figure out a better way, especially to support more languages.

        if (FindToolStrip(menu, "menuLanguageEnglish") is not ToolStripMenuItem en
            || FindToolStrip(menu, "menuLanguageSpanish") is not ToolStripMenuItem es)
            return;

        en.Checked = SelectedLanguage == Language.ENGLISH;
        es.Checked = SelectedLanguage == Language.SPANISH;
    }

    private static ToolStripItem? FindToolStrip(MenuStrip menu, string name)
    {
        foreach (ToolStripItem item in menu.Items)
        {
            var found = FindToolStrip(item, name);
            if (found != null) return found;
        }
        return null;
    }

    private static ToolStripItem? FindToolStrip(ToolStripItem item, string name)
    {
        if (item.Name == name) return item;

        if (item is ToolStripMenuItem mi)
        {
            foreach (ToolStripItem sub in mi.DropDownItems)
            {
                var found = FindToolStrip(sub, name);
                if (found != null) return found;
            }
        }

        return null;
    }
}