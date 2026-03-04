using App.Constants;
using App.Data.Entities;
using App.Services;
using App.Utils;

namespace App;

public partial class MainForm : Form
{
    private readonly LedgerService _ledgerService;

    private readonly SettingsService _settingsService;

    private decimal _lastSavedSavingsPercent = -1m;

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="ledgerService">Ledger service to handle DB requests for transactions</param>
    /// <param name="settingsService">Settings service to handler DB requests for settings</param>
    public MainForm(
        LedgerService ledgerService,
        SettingsService settingsService
    )
    {
        InitializeComponent();
        tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
        _ledgerService = ledgerService;
        _settingsService = settingsService;
        Shown += MainForm_Shown;
    }

    #endregion

    #region Helpers

    //Helper for not implemented features. Removing after
    private static void ShowNotImplemented(string feature)
        => MessageBox.Show($"Not implemented\n\nFeature: {feature}", "Simple Budget");

    #endregion

    #region Menu Handlers

    private void HelpDocsClicked(object? sender, EventArgs e)
    {
        // TODO: open local docs or project website (later)
        ShowNotImplemented("Documentation");
    }

    private void HelpAboutClicked(object? sender, EventArgs e)
    {
        // TODO: show About dialog with version, author, license, links
        MessageBox.Show($"Simple Budget\nVersion {XmlHelpers.GetAppVersion()}\n\nA simple income/expense tracker.", "About");
    }

    /// <summary>
    /// Applies english to all applicable labels.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LanguageEnglishClicked(object? sender, EventArgs e)
    {
        LabelFormatter.SelectedLanguage = Language.ENGLISH;

        var setting = new AppSetting
        {
            Setting = AppConfig.LanguagePreference,
            Value = $"{(int)Language.ENGLISH}"
        };

        var ok = _settingsService.Set(setting);
        if (!ok)
        {
            MessageBox.Show("Failed to save language preference.", "Simple Budget");
            return;
        }

        ApplyLanguageAndRefresh();
    }

    /// <summary>
    /// Applies Spanish to all applicable labels.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LanguageSpanishClicked(object? sender, EventArgs e)
    {
        LabelFormatter.SelectedLanguage = Language.SPANISH;

        var setting = new AppSetting
        {
            Setting = AppConfig.LanguagePreference,
            Value = $"{(int)Language.SPANISH}"
        };

        var ok = _settingsService.Set(setting);
        if (!ok)
        {
            MessageBox.Show("Failed to save language preference.", "Simple Budget");
            return;
        }

        ApplyLanguageAndRefresh();
    }

    #endregion

    #region Dashboard

    private void SummaryMonthChanged(object? sender, EventArgs e)
    {
        // TODO: load totals + grids for selected scope + anchor date
        ShowNotImplemented("Change dashboard date");
    }

    #endregion

    #region Income

    /// <summary>
    /// Enables/Disables frequency selctor dropdown.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncomeRecurringChanged(object? sender, EventArgs e)
    {
        cbIncomeFrequency.Enabled = chkIncomeRecurring.Checked;
    }

    /// <summary>
    /// Adds a new income entry to the database.
    /// Tab gets refreshed and inputs return to 
    /// default values.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncomeAddClicked(object? sender, EventArgs e)
    {
        int? frequency = chkIncomeRecurring.Checked == true
            && cbIncomeFrequency.SelectedIndex != -1
            ? cbIncomeFrequency.SelectedIndex
            : null;

        LedgerEntry entry = new()
        {
            Type = (int)LedgerEntryType.Income,
            Category = cbIncomeCategory.Text,
            Amount = nudIncomeAmount.Value,
            TransactionDate = dtpIncomeDate.Value,
            Notes = txtIncomeNotes.Text,
            Recurring = chkIncomeRecurring.Checked,
            Frequency = frequency
        };

        TransactionFieldsValid(entry, out var errors);

        if (errors.Count > 0)
        {
            string message = string.Empty;

            for (int i = 0; i < errors.Count; i++)
            {
                message += $"{i + 1}. {errors[i]}\n";
            }

            MessageBox.Show(message, "Simple Budget", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        bool ok = _ledgerService.AddLedgerEntry(entry);

        if (!ok)
        {
            MessageBox.Show("Failed to add income.", "Simple Budget");
            return;
        }

        RefreshIncomeTab();

        IncomeClearClicked(sender, e);
    }

    /// <summary>
    /// Clears all the income inputs and reset them to their defaults.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncomeClearClicked(object? sender, EventArgs e)
    {
        ClearComboSelection(cbIncomeCategory);
        nudIncomeAmount.Value = 0;
        dtpIncomeDate.Value = DateTime.Now;
        chkIncomeRecurring.Checked = false;
        cbIncomeFrequency.SelectedIndex = -1;
        txtIncomeNotes.Text = string.Empty;
    }

    /// <summary>
    /// Deletes the selected income entry from the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncomeDeleteSelectedClicked(object? sender, EventArgs e)
    {
        var selected = GetSelectedLedgerEntry(dgvIncome);
        if (selected is null)
        {
            MessageBox.Show("Select an income entry to delete.", "Simple Budget");
            return;
        }

        var language = LabelFormatter.SelectedLanguage;

        var confirmText = language switch
        {
            Language.SPANISH => "¿Eliminar la transacción seleccionada?",
            _ => "Delete the selected transaction?"
        };

        var title = "Simple Budget";

        var result = MessageBox.Show(
            confirmText,
            title,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        var ok = _ledgerService.RemoveLedgerEntry(selected.Id);
        if (!ok)
        {
            var failText = language switch
            {
                Language.SPANISH => "No se pudo eliminar la transacción.",
                _ => "Failed to delete the transaction."
            };

            MessageBox.Show(failText, "Simple Budget");
            return;
        }

        RefreshIncomeTab();
    }

    #endregion

    #region Expenses

    /// <summary>
    /// Enables/Disables frequency selctor dropdown.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExpenseRecurringChanged(object? sender, EventArgs e)
    {
        cbExpenseFrequency.Enabled = chkExpenseRecurring.Checked;
    }

    /// <summary>
    /// Adds a new expense entry to the database.
    /// Tab gets refreshed and inputs return to 
    /// default values.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExpenseAddClicked(object? sender, EventArgs e)
    {
        //TODO Similar to IncomeAddClicked when completed.
    }

    /// <summary>
    /// Clears all the income inputs and reset them to their defaults.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExpenseClearClicked(object? sender, EventArgs e)
    {
        ClearComboSelection(cbExpenseCategory);
        nudExpenseAmount.Value = 0;
        dtpExpenseDate.Value = DateTime.Now;
        chkExpenseRecurring.Checked = false;
        cbExpenseFrequency.SelectedIndex = -1;
        txtExpenseNotes.Text = string.Empty;
    }

    /// <summary>
    /// Deletes the selected expense entry from the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExpenseDeleteSelectedClicked(object? sender, EventArgs e)
    {
        // TODO: Delete the entry from the database
        ShowNotImplemented("Delete Expense Entry");
    }

    #endregion

    #region Reports

    private void ReportRunClicked(object? sender, EventArgs e)
    {
        // TODO: build query based on filters and load results grid
        ShowNotImplemented("Run Report");
    }

    private void ReportClearClicked(object? sender, EventArgs e)
    {
        // TODO: reset report filters to defaults
        ShowNotImplemented("Clear Report Filters");
    }

    private void ExportPdfClicked(object? sender, EventArgs e)
    {
        // TODO: export current report results to PDF
        ShowNotImplemented("Export PDF");
    }

    private void ExportExcelClicked(object? sender, EventArgs e)
    {
        // TODO: export current report results to Excel
        ShowNotImplemented("Export Excel");
    }

    #endregion

    #region Settings

    private void AddIncomeTypeClicked(object? sender, EventArgs e)
    {
        var name = txtNewIncomeType.Text.Trim();
        if (string.IsNullOrWhiteSpace(name)) return;
        name = name.ToLower();
        name = char.ToUpper(name[0]) + name[1..];

        var ok = _ledgerService.AddCategory(new TransactionCategory
        {
            Name = name,
            Type = (int)LedgerEntryType.Income
        });

        if (!ok)
        {
            MessageBox.Show("Failed to add category.", "Simple Budget");
            return;
        }

        txtNewIncomeType.Clear();

        RefreshSettingsTab();
        RefreshIncomeTab();
    }

    private void RemoveIncomeTypeClicked(object? sender, EventArgs e)
    {
        if (lbIncomeTypes.SelectedItem is not string name) return;

        var ok = _ledgerService.RemoveCategory(name);
        if (!ok)
        {
            MessageBox.Show("Failed to remove category.", "Simple Budget");
            return;
        }

        RefreshSettingsTab();
        RefreshIncomeTab();
    }

    private void AddExpenseTypeClicked(object? sender, EventArgs e)
    {
        var name = txtNewExpenseType.Text.Trim();
        if (string.IsNullOrWhiteSpace(name)) return;
        name = name.ToLower();
        name = char.ToUpper(name[0]) + name[1..];

        var ok = _ledgerService.AddCategory(new TransactionCategory
        {
            Name = name,
            Type = (int)LedgerEntryType.Expense
        });

        if (!ok)
        {
            MessageBox.Show("Failed to add category.", "Simple Budget");
            return;
        }

        txtNewExpenseType.Clear();

        RefreshSettingsTab();
        RefreshExpensesTab();
    }

    private void RemoveExpenseTypeClicked(object? sender, EventArgs e)
    {
        if (lbExpenseTypes.SelectedItem is not string name) return;

        var ok = _ledgerService.RemoveCategory(name);
        if (!ok)
        {
            MessageBox.Show("Failed to remove category.", "Simple Budget");
            return;
        }

        RefreshSettingsTab();
        RefreshExpensesTab();
    }

    private void SavingsSaveClicked(object? sender, EventArgs e)
    {
        var value = nudSavingsPercent.Value;

        if (value == _lastSavedSavingsPercent)
            return;

        var setting = new AppSetting
        {
            Setting = AppConfig.SavingsPercentage,
            Value = value.ToString(System.Globalization.CultureInfo.InvariantCulture)
        };

        var ok = _settingsService.Set(setting);
        if (!ok)
        {
            MessageBox.Show("Failed to save Savings %.", "Simple Budget");
            return;
        }

        _lastSavedSavingsPercent = value;
    }

    #endregion

    #region Form Events

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        ConfigureSplitSafe(
            scIncome,
            panel1Min: 300,
            panel2Min: 200,
            panel1Target: 330
        );

        ConfigureSplitSafe(
            scExpenses,
            panel1Min: 300,
            panel2Min: 200,
            panel1Target: 330
        );

        if (_settingsService.TryGetValue(AppConfig.LanguagePreference, out string saved) &&
            int.TryParse(saved, out var language))
        {
            LabelFormatter.SelectedLanguage = language switch
            {
                (int)Language.SPANISH => Language.SPANISH,
                _ => Language.ENGLISH
            };
        }
        else
        {
            LabelFormatter.SelectedLanguage = Language.ENGLISH;
        }

        ApplyLanguageAndRefresh();
    }

    #endregion

    #region UI Helpers

    private void TabMain_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (sender is not TabControl tc) return;

        var page = tc.TabPages[e.Index];
        var isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

        var r = e.Bounds;

        var bg = isSelected ? AppConfig.ThemeTabActive : AppConfig.ThemeTabInactive;
        var fg = isSelected ? Color.White : AppConfig.ThemeText;

        using var backBrush = new SolidBrush(bg);
        using var textBrush = new SolidBrush(fg);
        using var pen = new Pen(AppConfig.ThemeBorder);

        e.Graphics.FillRectangle(backBrush, r);
        e.Graphics.DrawRectangle(pen, r);

        var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        e.Graphics.DrawString(page.Text, Font, textBrush, r, sf);
    }

    private static void ConfigureSplitSafe(SplitContainer sc, int panel1Min, int panel2Min, int panel1Target)
    {
        sc.Panel1MinSize = panel1Min;
        sc.Panel2MinSize = panel2Min;

        var total = sc.ClientSize.Height;
        if (total <= 0) return;

        var min = sc.Panel1MinSize;
        var max = Math.Max(min, total - sc.Panel2MinSize);

        sc.SplitterDistance = Math.Max(min, Math.Min(panel1Target, max));

        sc.FixedPanel = FixedPanel.None;
        sc.IsSplitterFixed = false;
        sc.SplitterWidth = 6;
    }

    private void TabMain_SelectedIndexChanged(object? sender, EventArgs e) =>
        RefreshCurrentTab();

    private void RefreshCurrentTab()
    {

        if (tabMain.SelectedTab == tabDashboard)
        {
            RefreshDashboardTab();
            return;
        }

        if (tabMain.SelectedTab == tabIncome)
        {
            RefreshIncomeTab();
            return;
        }

        if (tabMain.SelectedTab == tabExpenses)
        {
            RefreshExpensesTab();
            return;
        }

        if (tabMain.SelectedTab == tabReports)
        {
            RefreshReportsTab();
            return;
        }

        if (tabMain.SelectedTab == tabSettings)
        {
            RefreshSettingsTab();
            return;
        }
    }

    private void RefreshIncomeTab()
    {
        var categories = _ledgerService.GetCategories(LedgerEntryType.Income);

        cbIncomeCategory.BeginUpdate();
        try
        {
            var selected = cbIncomeCategory.SelectedItem as string;

            cbIncomeCategory.DataSource = null;
            cbIncomeCategory.Items.Clear();
            cbIncomeCategory.DataSource = categories;

            if (!string.IsNullOrWhiteSpace(selected) && categories.Contains(selected))
                cbIncomeCategory.SelectedItem = selected;
            else
                cbIncomeCategory.SelectedIndex = -1;
        }
        finally
        {
            cbIncomeCategory.EndUpdate();
        }

        var incomeEntries = _ledgerService.GetLedgerEntries(LedgerEntryType.Income);

        dgvIncome.AutoGenerateColumns = true;
        dgvIncome.DataSource = null;
        dgvIncome.DataSource = incomeEntries;

        if (dgvIncome.Columns.Contains("Type"))
            dgvIncome.Columns["Type"].Visible = false;

        if (dgvIncome.Columns.Contains("Id"))
            dgvIncome.Columns["Id"].Visible = false;
    }

    private void RefreshExpensesTab()
    {
        var categories = _ledgerService.GetCategories(LedgerEntryType.Expense);

        cbExpenseCategory.BeginUpdate();
        try
        {
            var selected = cbExpenseCategory.SelectedItem as string;

            cbExpenseCategory.DataSource = null;
            cbExpenseCategory.Items.Clear();
            cbExpenseCategory.DataSource = categories;

            if (!string.IsNullOrWhiteSpace(selected) && categories.Contains(selected))
                cbExpenseCategory.SelectedItem = selected;
            else
                cbExpenseCategory.SelectedIndex = -1;
        }
        finally
        {
            cbExpenseCategory.EndUpdate();
        }

        // TODO refresh dgvExpenses
    }

    private void RefreshSettingsTab()
    {
        var income = _ledgerService.GetCategories(LedgerEntryType.Income);
        var expense = _ledgerService.GetCategories(LedgerEntryType.Expense);

        lbIncomeTypes.BeginUpdate();
        lbExpenseTypes.BeginUpdate();
        try
        {
            lbIncomeTypes.Items.Clear();
            lbExpenseTypes.Items.Clear();

            foreach (var name in income) lbIncomeTypes.Items.Add(name);
            foreach (var name in expense) lbExpenseTypes.Items.Add(name);
        }
        finally
        {
            lbIncomeTypes.EndUpdate();
            lbExpenseTypes.EndUpdate();
        }

        if (_settingsService.TryGetValue(AppConfig.SavingsPercentage, out var saved))
        {
            if (decimal.TryParse(saved,
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var parsed))
            {
                if (parsed < nudSavingsPercent.Minimum) parsed = nudSavingsPercent.Minimum;
                if (parsed > nudSavingsPercent.Maximum) parsed = nudSavingsPercent.Maximum;

                nudSavingsPercent.Value = parsed;
                _lastSavedSavingsPercent = parsed;
            }
            else
            {
                nudSavingsPercent.Value = 0;
                _lastSavedSavingsPercent = 0;
            }
        }
        else
        {
            nudSavingsPercent.Value = 0;
            _lastSavedSavingsPercent = 0;
        }
    }

    private static void RefreshDashboardTab() { /* TODO */ }
    private static void RefreshReportsTab() { /* TODO */ }

    private void ApplyLanguageAndRefresh()
    {
        LabelFormatter.Apply(this, menuMain);

        RefreshCurrentTab();
        tabMain.Invalidate();
    }

    /// <summary>
    /// Clears the combo box selection to use -1 index.
    /// </summary>
    /// <param name="cb">The combo box</param>
    private static void ClearComboSelection(ComboBox cb)
    {
        cb.SelectedItem = null;
        cb.Text = string.Empty;
        if (cb.SelectedIndex != -1)
            cb.SelectedIndex = -1;
    }

    /// <summary>
    /// Validates the fields of Income/Expenses before adding them to the database.
    /// </summary>
    /// <param name="entry">The entry fields</param>
    /// <param name="errors">The error messages for invalid fields.</param>
    private static void TransactionFieldsValid(LedgerEntry entry, out List<string> errors)
    {
        errors = [];
        Language language = LabelFormatter.SelectedLanguage;

        if (entry.Category == string.Empty)
        {
            switch (language)
            {
                case Language.ENGLISH:
                    {
                        errors.Add("Category cannot be empty.");
                        break;
                    }
                case Language.SPANISH:
                    {
                        errors.Add("La Categoría no puede estar en blanco.");
                        break;
                    }
            }
        }

        if (entry.Amount <= 0)
        {
            switch (language)
            {
                case Language.ENGLISH:
                    {
                        errors.Add("Amount must be greater than zero.");
                        break;
                    }
                case Language.SPANISH:
                    {
                        errors.Add("La Cantidad debe ser mayor que zero.");
                        break;
                    }
            }
        }

        if (entry.Recurring == true && entry.Frequency == null)
        {
            switch (language)
            {
                case Language.ENGLISH:
                    {
                        errors.Add("Frequency cannot be empty for recurring transactions.");
                        break;
                    }
                case Language.SPANISH:
                    {
                        errors.Add("La Frequencia no puede esta en blanco para transacciones repetitivas.");
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// Gets the selected row full entry
    /// </summary>
    /// <param name="grid">The grid</param>
    /// <returns>The ledger entry</returns>
    private static LedgerEntry? GetSelectedLedgerEntry(DataGridView grid)
    {
        if (grid.CurrentRow?.DataBoundItem is LedgerEntry rowItem)
            return rowItem;

        if (grid.SelectedRows.Count > 0 &&
            grid.SelectedRows[0].DataBoundItem is LedgerEntry selectedItem)
            return selectedItem;

        return null;
    }

    #endregion

    #region Theme Helpers

    private static void StyleGroupBox(GroupBox gb)
    {
        gb.BackColor = AppConfig.ThemePanel;
        gb.ForeColor = AppConfig.ThemeText;
    }

    private static void StyleTextBox(TextBox tb)
    {
        tb.BackColor = AppConfig.ThemeInput;
        tb.ForeColor = AppConfig.ThemeText;
    }

    private static void StyleComboBox(ComboBox cb)
    {
        cb.BackColor = AppConfig.ThemeInput;
        cb.ForeColor = AppConfig.ThemeText;
        cb.FlatStyle = FlatStyle.Flat;
    }

    private static void StyleDateTimePicker(DateTimePicker dtp)
    {
        dtp.CalendarMonthBackground = AppConfig.ThemeInput;
        dtp.CalendarForeColor = AppConfig.ThemeText;
    }

    private static void StyleNumeric(NumericUpDown nud)
    {
        nud.BackColor = AppConfig.ThemeInput;
        nud.ForeColor = AppConfig.ThemeText;
    }

    private static void StyleListBox(ListBox lb)
    {
        lb.BackColor = AppConfig.ThemeInput;
        lb.ForeColor = AppConfig.ThemeText;
        lb.BorderStyle = BorderStyle.FixedSingle;
    }

    // Force consistent button styling
    private static void StyleButton(Button btn)
    {
        btn.UseVisualStyleBackColor = false;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderColor = AppConfig.ThemeBorder;
        btn.FlatAppearance.BorderSize = 1;

        btn.BackColor = AppConfig.ThemeAccent;
        btn.ForeColor = Color.White;

        btn.FlatAppearance.MouseOverBackColor = AppConfig.ThemeAccentHover;
        btn.FlatAppearance.MouseDownBackColor = AppConfig.ThemeAccentDown;

        btn.AutoSize = false;
        btn.Height = 36;
        btn.Padding = new Padding(14, 0, 14, 0);
        btn.TextAlign = ContentAlignment.MiddleCenter;
    }

    private static void ApplyDataGridTheme(DataGridView grid)
    {
        grid.BackgroundColor = AppConfig.ThemePanel;
        grid.GridColor = AppConfig.ThemeBorder;
        grid.BorderStyle = BorderStyle.None;

        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.RowHeadersVisible = false;

        grid.DefaultCellStyle.BackColor = AppConfig.ThemeBack;
        grid.DefaultCellStyle.ForeColor = AppConfig.ThemeText;
        grid.DefaultCellStyle.SelectionBackColor = AppConfig.ThemeTabActive;
        grid.DefaultCellStyle.SelectionForeColor = Color.White;

        grid.ColumnHeadersDefaultCellStyle.BackColor = AppConfig.ThemePanel;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = AppConfig.ThemeText;

        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(38, 38, 38);
    }

    #endregion
}
