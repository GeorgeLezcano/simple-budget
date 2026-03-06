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
    /// Ensures every Control/ToolStripItem has a stable Name so it can be translated via dictionary.
    /// If a Name is missing, we set it to the backing field name (e.g., lblIncomeAmount).
    /// Call this once after InitializeComponent().
    /// </summary>
    public static void EnsureNamesFromFields(object owner, MenuStrip? menu = null)
    {
        if (owner is null) return;

        var flags =
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Public;

        var fields = owner.GetType().GetFields(flags);

        foreach (var f in fields)
        {
            var value = f.GetValue(owner);
            if (value is null) continue;

            if (value is Control c)
            {
                if (string.IsNullOrWhiteSpace(c.Name))
                    c.Name = f.Name;
                continue;
            }

            if (value is ToolStripItem tsi)
            {
                if (string.IsNullOrWhiteSpace(tsi.Name))
                    tsi.Name = f.Name;
            }
        }

        if (menu != null)
        {
            foreach (ToolStripItem item in menu.Items)
                EnsureToolStripNamesRecursive(item);
        }
    }

    private static void EnsureToolStripNamesRecursive(ToolStripItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Name))
            item.Name = item.GetType().Name;

        if (item is ToolStripMenuItem mi)
        {
            foreach (ToolStripItem sub in mi.DropDownItems)
                EnsureToolStripNamesRecursive(sub);
        }
    }

    /// <summary>
    /// Key = Control.Name (or ToolStripItem.Name)
    /// Value = translated string
    /// </summary>
    private static readonly Dictionary<Language, Dictionary<string, string>> _labels =

        new()
        {
            [Language.ENGLISH] = new()
            {
                ["lblDashTitle"] = "Simple Budget",
                ["lblDashSubtitle"] = "Track income and expenses. View summaries by week, month, or year.",
                ["lblDashRange"] = "View:",
                ["lblDashMonth"] = "Anchor date:",
                ["gbDashTotals"] = "Summary",
                ["lblIncomeTotalTitle"] = "Total Income",
                ["lblExpenseTotalTitle"] = "Total Expenses",
                ["lblSavingsTotalTitle"] = "Savings",
                ["lblNetTotalTitle"] = "Net",
                ["gbDashNextSteps"] = "Next steps",
                ["lblDashHint"] =
                    "• Use Income to add money coming in.\n" +
                    "• Use Expenses to log spending (recurring or one-time).\n" +
                    "• Use Reports to filter/export.\n" +
                    "• Use Settings to manage categories (Rent, Groceries, etc.).",

                // App shell / menu / tabs
                ["menuHelp"] = "&Help",
                ["menuHelpDocs"] = "&Documentation",
                ["menuHelpAbout"] = "&About",
                ["menuLanguage"] = "&Language",
                ["menuLanguageEnglish"] = "&English",
                ["menuLanguageSpanish"] = "&Español",
                ["tabDashboard"] = "Dashboard",
                ["tabIncome"] = "Income",
                ["tabExpenses"] = "Expenses",
                ["tabReports"] = "Reports",
                ["tabSettings"] = "Settings",

                // Income tab
                ["gbIncomeEntry"] = "Add Income",
                ["lblIncomeCategory"] = "Category:",
                ["lblIncomeAmount"] = "Amount:",
                ["lblIncomeDate"] = "Date:",
                ["chkIncomeRecurring"] = "Recurring",
                ["lblIncomeFrequency"] = "Frequency:",
                ["lblIncomeNotes"] = "Notes:",
                ["btnIncomeAdd"] = "Add Income",
                ["btnIncomeClear"] = "Clear",
                ["gbIncomeList"] = "Income List",
                ["btnIncomeDeleteSelected"] = "Delete Selected",
                ["btnIncomeDeleteAll"] = "Delete All",

                // Expenses tab
                ["gbExpenseEntry"] = "Add Expense",
                ["lblExpenseCategory"] = "Category:",
                ["lblExpenseAmount"] = "Amount:",
                ["lblExpenseDate"] = "Date:",
                ["chkExpenseRecurring"] = "Recurring",
                ["lblExpenseFrequency"] = "Frequency:",
                ["lblExpenseNotes"] = "Notes:",
                ["btnExpenseAdd"] = "Add Expense",
                ["btnExpenseClear"] = "Clear",
                ["gbExpenseList"] = "Expense List",
                ["btnExpenseDeleteSelected"] = "Delete Selected",
                ["btnExpenseDeleteAll"] = "Delete All",

                // Settings tab
                ["lblSettingsTitle"] = "Settings",
                ["lblSettingsHint"] = "Add categories here. They will show up in Income/Expenses dropdowns.",
                ["gbIncomeTypes"] = "Income Categories",
                ["gbExpenseTypes"] = "Expense Categories",
                ["btnAddIncomeType"] = "Add Category",
                ["btnRemoveIncomeType"] = "Remove Selected",
                ["btnAddExpenseType"] = "Add Category",
                ["btnRemoveExpenseType"] = "Remove Selected",
                ["gbSavingsSettings"] = "Savings",
                ["lblSavingsPercent"] = "Savings %:",
                ["btnSavingsSave"] = "Save",
                ["txtNewIncomeType.PlaceholderText"] = "New income category...",
                ["txtNewExpenseType.PlaceholderText"] = "New expense category..."
            },

            [Language.SPANISH] = new()
            {
                ["lblDashTitle"] = "Simple Budget",
                ["lblDashSubtitle"] = "Controla ingresos y gastos. Mira resúmenes por semana, mes o año.",
                ["lblDashRange"] = "Vista:",
                ["lblDashMonth"] = "Fecha ancla:",
                ["gbDashTotals"] = "Resumen",
                ["lblIncomeTotalTitle"] = "Ingresos totales",
                ["lblExpenseTotalTitle"] = "Gastos totales",
                ["lblSavingsTotalTitle"] = "Ahorros",
                ["lblNetTotalTitle"] = "Neto",
                ["gbDashNextSteps"] = "Próximos pasos",
                ["lblDashHint"] =
                    "• Usa Ingresos para agregar dinero que entra.\n" +
                    "• Usa Gastos para registrar gastos (recurrentes o únicos).\n" +
                    "• Usa Informes para filtrar/exportar.\n" +
                    "• Usa Configuración para administrar categorías (Alquiler, Comestibles, etc.).",

                // App shell / menu / tabs
                ["menuHelp"] = "&Ayuda",
                ["menuHelpDocs"] = "&Documentación",
                ["menuHelpAbout"] = "&Acerca de",
                ["menuLanguage"] = "&Idioma",
                ["menuLanguageEnglish"] = "&English",
                ["menuLanguageSpanish"] = "&Español",
                ["tabDashboard"] = "Panel",
                ["tabIncome"] = "Ingresos",
                ["tabExpenses"] = "Gastos",
                ["tabReports"] = "Informes",
                ["tabSettings"] = "Configuración",

                // Income tab
                ["gbIncomeEntry"] = "Agregar ingreso",
                ["lblIncomeCategory"] = "Categoría:",
                ["lblIncomeAmount"] = "Cantidad:",
                ["lblIncomeDate"] = "Fecha:",
                ["chkIncomeRecurring"] = "Repetitivo",
                ["lblIncomeFrequency"] = "Frecuencia:",
                ["lblIncomeNotes"] = "Notas:",
                ["btnIncomeAdd"] = "Agregar ingreso",
                ["btnIncomeClear"] = "Limpiar",
                ["gbIncomeList"] = "Lista de ingresos",
                ["btnIncomeDeleteSelected"] = "Eliminar seleccionado",
                ["btnIncomeDeleteAll"] = "Eliminar todo",

                // Expenses tab
                ["gbExpenseEntry"] = "Agregar gasto",
                ["lblExpenseCategory"] = "Categoría:",
                ["lblExpenseAmount"] = "Cantidad:",
                ["lblExpenseDate"] = "Fecha:",
                ["chkExpenseRecurring"] = "Repetitivo",
                ["lblExpenseFrequency"] = "Frecuencia:",
                ["lblExpenseNotes"] = "Notas:",
                ["btnExpenseAdd"] = "Agregar gasto",
                ["btnExpenseClear"] = "Limpiar",
                ["gbExpenseList"] = "Lista de gastos",
                ["btnExpenseDeleteSelected"] = "Eliminar seleccionado",
                ["btnExpenseDeleteAll"] = "Eliminar todo",

                // Settings tab
                ["lblSettingsTitle"] = "Configuración",
                ["lblSettingsHint"] = "Agrega categorías aquí. Aparecerán en los menús de Ingresos/Gastos.",
                ["gbIncomeTypes"] = "Categorías de ingresos",
                ["gbExpenseTypes"] = "Categorías de gastos",
                ["btnAddIncomeType"] = "Agregar categoría",
                ["btnRemoveIncomeType"] = "Eliminar seleccionado",
                ["btnAddExpenseType"] = "Agregar categoría",
                ["btnRemoveExpenseType"] = "Eliminar seleccionado",
                ["gbSavingsSettings"] = "Ahorros",
                ["lblSavingsPercent"] = "Ahorros %:",
                ["btnSavingsSave"] = "Guardar",
                ["txtNewIncomeType.PlaceholderText"] = "Nueva categoría de ingresos...",
                ["txtNewExpenseType.PlaceholderText"] = "Nueva categoría de gastos..."
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

        if (_labels.TryGetValue(SelectedLanguage, out var map))
        {
            if (!string.IsNullOrWhiteSpace(name) && map.TryGetValue(name, out var text))
                control.Text = text;

            if (control is TextBox tb && !string.IsNullOrWhiteSpace(name))
            {
                var phKey = $"{name}.PlaceholderText";
                if (map.TryGetValue(phKey, out var placeholder))
                    tb.PlaceholderText = placeholder;
            }
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