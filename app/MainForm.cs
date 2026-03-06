using System.Globalization;
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

    private string? _incomeSortColumn;
    private bool _incomeSortAscending = true;

    private string? _expenseSortColumn;
    private bool _expenseSortAscending = true;

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

        LedgerGridHeaderFormatter.Register(dgvIncome);
        LedgerGridHeaderFormatter.Register(dgvExpenses);

        LabelFormatter.EnsureNamesFromFields(this, menuMain);

        dgvIncome.CellFormatting += LedgerGrid_CellFormatting;
        dgvExpenses.CellFormatting += LedgerGrid_CellFormatting;

        dgvIncome.ColumnHeaderMouseClick += IncomeGrid_ColumnHeaderMouseClick;
        dgvExpenses.ColumnHeaderMouseClick += ExpenseGrid_ColumnHeaderMouseClick;

        tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
        cbDashRange.SelectedIndexChanged += SummaryMonthChanged;
        nudSavingsPercent.ValueChanged += SavingsPercentChanged;

        _ledgerService = ledgerService;
        _settingsService = settingsService;
        Shown += MainForm_Shown;
    }

    #endregion

    #region Menu Handlers

    /// <summary>
    /// Handler for Help>Documentation button clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HelpDocsClicked(object? sender, EventArgs e)
    {
        // TODO: open local docs or project website (later)
        MessageBox.Show($"Not implemented", "Simple Budget");
    }

    /// <summary>
    /// Displays information about the application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HelpAboutClicked(object? sender, EventArgs e)
    {
        var message = LabelFormatter.SelectedLanguage switch
        {
            Language.SPANISH =>
                $"Simple Budget\nVersión {XmlHelpers.GetAppVersion()}\n\n" +
                "Una aplicación de presupuesto local y ligera para registrar ingresos, gastos y ahorros.\n\n",
            _ =>
                $"Simple Budget\nVersion {XmlHelpers.GetAppVersion()}\n\n" +
                "A lightweight local budget app for tracking income, expenses, and savings.\n\n"
        };

        var title = LabelFormatter.SelectedLanguage switch
        {
            Language.SPANISH => "Acerca de",
            _ => "About"
        };

        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    /// <summary>
    /// Refrehses the dashboard tab when values changed in the summary.;
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SummaryMonthChanged(object? sender, EventArgs e) =>
        RefreshDashboardTab();

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
        int? frequency = chkIncomeRecurring.Checked && cbIncomeFrequency.SelectedIndex != -1
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
        RefreshDashboardTab();

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
        RefreshDashboardTab();
    }

    /// <summary>
    /// Deletes all Income entries from the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncomeDeleteAllClicked(object? sender, EventArgs e)
    {
        var language = LabelFormatter.SelectedLanguage;

        var confirmText = language switch
        {
            Language.SPANISH => "¿Eliminar todos los ingresos? Esta acción no se puede deshacer.",
            _ => "Delete all income entries? This action cannot be undone."
        };

        var result = MessageBox.Show(
            confirmText,
            "Simple Budget",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        var ok = _ledgerService.RemoveAllLedgerEntries(LedgerEntryType.Income);
        if (!ok)
        {
            var failText = language switch
            {
                Language.SPANISH => "No se pudieron eliminar todos los ingresos.",
                _ => "Failed to delete all income entries."
            };

            MessageBox.Show(failText, "Simple Budget");
            return;
        }

        RefreshIncomeTab();
        RefreshDashboardTab();
    }

    /// <summary>
    /// Sorts rows based on column headers.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncomeGrid_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvIncome.Columns.Count)
            return;

        var column = dgvIncome.Columns[e.ColumnIndex];
        var propertyName = GetSortablePropertyName(column);

        if (string.IsNullOrWhiteSpace(propertyName))
            return;

        if (typeof(LedgerEntry).GetProperty(propertyName) is null)
            return;

        if (string.Equals(_incomeSortColumn, propertyName, StringComparison.Ordinal))
            _incomeSortAscending = !_incomeSortAscending;
        else
        {
            _incomeSortColumn = propertyName;
            _incomeSortAscending = true;
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
        int? frequency = chkExpenseRecurring.Checked == true
            && cbExpenseFrequency.SelectedIndex != -1
            ? cbExpenseFrequency.SelectedIndex
            : null;

        LedgerEntry entry = new()
        {
            Type = (int)LedgerEntryType.Expense,
            Category = cbExpenseCategory.Text,
            Amount = nudExpenseAmount.Value,
            TransactionDate = dtpExpenseDate.Value,
            Notes = txtExpenseNotes.Text,
            Recurring = chkExpenseRecurring.Checked,
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
            MessageBox.Show("Failed to add expense.", "Simple Budget");
            return;
        }

        RefreshExpensesTab();
        RefreshDashboardTab();

        ExpenseClearClicked(sender, e);
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
        var selected = GetSelectedLedgerEntry(dgvExpenses);
        if (selected is null)
        {
            MessageBox.Show("Select an expense entry to delete.", "Simple Budget");
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

        RefreshExpensesTab();
        RefreshDashboardTab();
    }

    /// <summary>
    /// Deletes all expense entries from the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExpenseDeleteAllClicked(object? sender, EventArgs e)
    {
        var language = LabelFormatter.SelectedLanguage;

        var confirmText = language switch
        {
            Language.SPANISH => "¿Eliminar todos los gastos? Esta acción no se puede deshacer.",
            _ => "Delete all expense entries? This action cannot be undone."
        };

        var result = MessageBox.Show(
            confirmText,
            "Simple Budget",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        var ok = _ledgerService.RemoveAllLedgerEntries(LedgerEntryType.Expense);
        if (!ok)
        {
            var failText = language switch
            {
                Language.SPANISH => "No se pudieron eliminar todos los gastos.",
                _ => "Failed to delete all expense entries."
            };

            MessageBox.Show(failText, "Simple Budget");
            return;
        }

        RefreshExpensesTab();
        RefreshDashboardTab();
    }

    /// <summary>
    /// Sorts rows by column headers.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExpenseGrid_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvExpenses.Columns.Count)
            return;

        var column = dgvExpenses.Columns[e.ColumnIndex];
        var propertyName = GetSortablePropertyName(column);

        if (string.IsNullOrWhiteSpace(propertyName))
            return;

        if (typeof(LedgerEntry).GetProperty(propertyName) is null)
            return;

        if (string.Equals(_expenseSortColumn, propertyName, StringComparison.Ordinal))
            _expenseSortAscending = !_expenseSortAscending;
        else
        {
            _expenseSortColumn = propertyName;
            _expenseSortAscending = true;
        }

        RefreshExpensesTab();
    }

    #endregion

    #region Reports

    /// <summary>
    /// On click handler for run report button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReportRunClicked(object? sender, EventArgs e)
    {
        // TODO: build query based on filters and load results grid
        MessageBox.Show($"Not implemented", "Simple Budget");
    }

    /// <summary>
    /// On click handler for clear filters button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReportClearClicked(object? sender, EventArgs e)
    {
        // TODO: reset report filters to defaults
        MessageBox.Show($"Not implemented", "Simple Budget");
    }

    /// <summary>
    /// On click handler for Export to PDF button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExportPdfClicked(object? sender, EventArgs e)
    {
        // TODO: export current report results to PDF
        MessageBox.Show($"Not implemented", "Simple Budget");
    }

    /// <summary>
    /// On click handler for Export to Excel button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExportExcelClicked(object? sender, EventArgs e)
    {
        // TODO: export current report results to Excel
        MessageBox.Show($"Not implemented", "Simple Budget");
    }

    #endregion

    #region Settings

    /// <summary>
    /// Handler to add new income types.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Handler to remove existing income types.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Handler to add a new expense type.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Habdler to remove expense types.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Handler for Savings % button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SavingsSaveClicked(object? sender, EventArgs e)
    {
        SaveSavingsPercent(showErrorMessage: true);
        RefreshDashboardTab();
    }

    /// <summary>
    /// Attempts to autosave on text field change.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SavingsPercentChanged(object? sender, EventArgs e)
    {
        SaveSavingsPercent(showErrorMessage: false);
        RefreshDashboardTab();
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

        LoadSavedSavingsPercent();
        ApplyLanguageAndRefresh();
        RefreshDashboardTab();
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

    /// <summary>
    /// On Changed handler that triggers a tab refresh.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TabMain_SelectedIndexChanged(object? sender, EventArgs e) =>
        RefreshCurrentTab();

    /// <summary>
    /// Refreshes the currently selected tab.
    /// </summary>
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

    /// <summary>
    /// Refreshes the income tab.
    /// </summary>
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

        var entries = _ledgerService.GetLedgerEntries(LedgerEntryType.Income);
        entries = ApplyLedgerSort(entries, _incomeSortColumn, _incomeSortAscending);

        dgvIncome.DataSource = null;
        dgvIncome.DataSource = entries;

        foreach (DataGridViewColumn column in dgvIncome.Columns)
            column.SortMode = DataGridViewColumnSortMode.Programmatic;

        if (!string.IsNullOrWhiteSpace(_incomeSortColumn) &&
            !GridContainsSortableProperty(dgvIncome, _incomeSortColumn))
        {
            _incomeSortColumn = null;
        }

        ApplyGridSortGlyph(dgvIncome, _incomeSortColumn, _incomeSortAscending);
    }

    /// <summary>
    /// Refreshes the Expenses tab.
    /// </summary>
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

        var entries = _ledgerService.GetLedgerEntries(LedgerEntryType.Expense);
        entries = ApplyLedgerSort(entries, _expenseSortColumn, _expenseSortAscending);

        dgvExpenses.DataSource = null;
        dgvExpenses.DataSource = entries;

        foreach (DataGridViewColumn column in dgvExpenses.Columns)
            column.SortMode = DataGridViewColumnSortMode.Programmatic;

        if (!string.IsNullOrWhiteSpace(_expenseSortColumn) &&
            !GridContainsSortableProperty(dgvExpenses, _expenseSortColumn))
        {
            _expenseSortColumn = null;
        }

        ApplyGridSortGlyph(dgvExpenses, _expenseSortColumn, _expenseSortAscending);
    }

    /// <summary>
    /// Refreshes the settings tab.
    /// </summary>
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
    }

    /// <summary>
    /// Refreshes the dashboard tab, including values and labels.
    /// </summary>
    private void RefreshDashboardTab()
    {
        var (rangeStart, rangeEndExclusive) = GetDashboardDateRange();

        var today = DateTime.Today;
        var cappedEndExclusive = rangeEndExclusive.Date > today.AddDays(1)
            ? today.AddDays(1)
            : rangeEndExclusive.Date;

        if (cappedEndExclusive <= rangeStart.Date)
        {
            lblIncomeTotalValue.Text = FormatCurrency(0);
            lblExpenseTotalValue.Text = FormatCurrency(0);
            lblSavingsTotalValue.Text = FormatCurrency(0);
            lblNetTotalValue.Text = FormatCurrency(0);
            return;
        }

        var incomes = _ledgerService.GetLedgerEntries(LedgerEntryType.Income);
        var expenses = _ledgerService.GetLedgerEntries(LedgerEntryType.Expense);

        var incomeTotal = CalculateDashboardTotal(incomes, rangeStart.Date, cappedEndExclusive);
        var expenseTotal = CalculateDashboardTotal(expenses, rangeStart.Date, cappedEndExclusive);

        var remainder = incomeTotal - expenseTotal;

        var savingsPercent = nudSavingsPercent.Value / 100m;

        var savingsAmount = remainder > 0
            ? decimal.Round(remainder * savingsPercent, 2)
            : 0m;

        var netAmount = remainder - savingsAmount;

        lblIncomeTotalValue.Text = FormatCurrency(incomeTotal);
        lblExpenseTotalValue.Text = FormatCurrency(expenseTotal);
        lblSavingsTotalValue.Text = FormatCurrency(savingsAmount);
        lblNetTotalValue.Text = FormatCurrency(netAmount);
    }

    private static void RefreshReportsTab() { /* TODO */ }

    /// <summary>
    /// Applies the selected language, and refreshes the tab.
    /// </summary>
    private void ApplyLanguageAndRefresh()
    {
        cbIncomeFrequency.Items.Clear();
        cbExpenseFrequency.Items.Clear();

        var freq = LabelFormatter.SelectedLanguage == Language.SPANISH
            ? AppConfig.TransactionFrequencySpanish
            : AppConfig.TransactionFrequency;

        cbIncomeFrequency.Items.AddRange(freq);
        cbExpenseFrequency.Items.AddRange(freq);

        var selectedDashIndex = cbDashRange.SelectedIndex < 0 ? 1 : cbDashRange.SelectedIndex;

        cbDashRange.Items.Clear();
        cbDashRange.Items.AddRange(
            LabelFormatter.SelectedLanguage == Language.SPANISH
                ? AppConfig.DashboardViewSpanish
                : AppConfig.DashboardView);

        cbDashRange.SelectedIndex = selectedDashIndex;

        LabelFormatter.Apply(this, menuMain);

        RefreshCurrentTab();
        RefreshDashboardTab();

        LedgerGridHeaderFormatter.RefreshAll();
        tabMain.Invalidate();
    }

    /// <summary>
    /// Save handler for percent.
    /// </summary>
    /// <param name="showErrorMessage"></param>
    /// <returns></returns>
    private bool SaveSavingsPercent(bool showErrorMessage)
    {
        var value = nudSavingsPercent.Value;

        if (value == _lastSavedSavingsPercent)
            return true;

        var setting = new AppSetting
        {
            Setting = AppConfig.SavingsPercentage,
            Value = value.ToString(System.Globalization.CultureInfo.InvariantCulture)
        };

        var ok = _settingsService.Set(setting);

        if (!ok)
        {
            if (showErrorMessage)
                MessageBox.Show("Failed to save Savings %.", "Simple Budget");

            return false;
        }

        _lastSavedSavingsPercent = value;
        return true;
    }

    /// <summary>
    /// Gets the dashboard date range.
    /// </summary>
    /// <returns></returns>
    private (DateTime start, DateTime endExclusive) GetDashboardDateRange()
    {
        var anchor = dtpDashMonth.Value.Date;

        return cbDashRange.SelectedIndex switch
        {
            0 => GetWeekDateRange(anchor),
            2 => (new DateTime(anchor.Year, 1, 1), new DateTime(anchor.Year + 1, 1, 1)),
            _ => (new DateTime(anchor.Year, anchor.Month, 1), new DateTime(anchor.Year, anchor.Month, 1).AddMonths(1))
        };
    }

    /// <summary>
    /// Gets the week date range using anchor.
    /// </summary>
    /// <param name="anchor"></param>
    /// <returns></returns>
    private static (DateTime start, DateTime endExclusive) GetWeekDateRange(DateTime anchor)
    {
        var firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        var offset = (7 + (anchor.DayOfWeek - firstDay)) % 7;
        var start = anchor.AddDays(-offset).Date;
        return (start, start.AddDays(7));
    }

    /// <summary>
    /// Formats the currency based on language and culture.
    /// </summary>
    /// <param name="amount">Currency amount</param>
    /// <returns></returns>
    private static string FormatCurrency(decimal amount)
    {
        return string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            "${0:N2}",
            amount);
    }

    private static string GetSortablePropertyName(DataGridViewColumn column)
    {
        if (!string.IsNullOrWhiteSpace(column.DataPropertyName))
            return column.DataPropertyName;

        return column.Name;
    }

    private static List<LedgerEntry> ApplyLedgerSort(List<LedgerEntry> entries, string? propertyName, bool ascending)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            return entries;

        var property = typeof(LedgerEntry).GetProperty(propertyName);
        if (property is null)
            return entries;

        object? selector(LedgerEntry entry) => property.GetValue(entry);

        return ascending
            ? [.. entries.OrderBy(selector, Comparer<object?>.Create(CompareObjects))]
            : [.. entries.OrderByDescending(selector, Comparer<object?>.Create(CompareObjects))];
    }

    private static int CompareObjects(object? left, object? right)
    {
        if (ReferenceEquals(left, right)) return 0;

        if (left is null && right is int) return -1;
        if (left is int && right is null) return 1;

        if (left is null) return -1;
        if (right is null) return 1;

        if (left is string ls && right is string rs)
            return string.Compare(ls, rs, StringComparison.CurrentCultureIgnoreCase);

        if (left is bool lb && right is bool rb)
            return lb.CompareTo(rb);

        if (left is IComparable comparable)
            return comparable.CompareTo(right);

        return string.Compare(left.ToString(), right.ToString(), StringComparison.CurrentCultureIgnoreCase);
    }

    private static void ApplyGridSortGlyph(DataGridView grid, string? propertyName, bool ascending)
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (column.SortMode == DataGridViewColumnSortMode.NotSortable)
                column.SortMode = DataGridViewColumnSortMode.Programmatic;

            column.HeaderCell.SortGlyphDirection = SortOrder.None;
        }

        if (string.IsNullOrWhiteSpace(propertyName))
            return;

        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (GetSortablePropertyName(column) != propertyName)
                continue;

            if (column.SortMode == DataGridViewColumnSortMode.NotSortable)
                column.SortMode = DataGridViewColumnSortMode.Programmatic;

            column.HeaderCell.SortGlyphDirection = ascending
                ? SortOrder.Ascending
                : SortOrder.Descending;

            return;
        }
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
    /// Formats the ledger grid cells.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LedgerGrid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView grid) return;

        if (!grid.Columns.Contains(nameof(LedgerEntry.Frequency))) return;
        if (grid.Columns[e.ColumnIndex].Name != nameof(LedgerEntry.Frequency)) return;

        if (e.Value is int idx)
        {
            e.Value = AppConfig.GetFrequencyDisplay(idx, LabelFormatter.SelectedLanguage);
            e.FormattingApplied = true;
            return;
        }

        if (e.Value is null)
        {
            e.Value = string.Empty;
            e.FormattingApplied = true;
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

    /// <summary>
    /// Loads the savings percent.
    /// </summary>
    private void LoadSavedSavingsPercent()
    {
        if (_settingsService.TryGetValue(AppConfig.SavingsPercentage, out var saved) &&
            decimal.TryParse(
                saved,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
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

    private decimal CalculateDashboardTotal(
    List<LedgerEntry> entries,
    DateTime rangeStart,
    DateTime rangeEndExclusive)
    {
        decimal total = 0m;

        foreach (var entry in entries)
        {
            total += CalculateEntryContribution(entry, rangeStart, rangeEndExclusive);
        }

        return total;
    }

    private decimal CalculateEntryContribution(
        LedgerEntry entry,
        DateTime rangeStart,
        DateTime rangeEndExclusive)
    {
        var startDate = entry.TransactionDate.Date;

        if (startDate >= DateTime.Today.AddDays(1))
            return 0m;

        if (entry.Frequency is null)
        {
            return startDate >= rangeStart && startDate < rangeEndExclusive
                ? entry.Amount
                : 0m;
        }

        var occurrences = CountRecurringOccurrencesInRange(
            startDate,
            entry.Frequency.Value,
            rangeStart,
            rangeEndExclusive);

        return entry.Amount * occurrences;
    }

    private static int CountRecurringOccurrencesInRange(
    DateTime startDate,
    int frequencyIndex,
    DateTime rangeStart,
    DateTime rangeEndExclusive)
    {
        if (startDate >= rangeEndExclusive)
            return 0;

        var todayExclusive = DateTime.Today.AddDays(1);

        if (startDate >= todayExclusive)
            return 0;

        var effectiveEndExclusive = rangeEndExclusive < todayExclusive
            ? rangeEndExclusive
            : todayExclusive;

        if (effectiveEndExclusive <= rangeStart)
            return 0;

        int count = 0;
        var current = startDate.Date;

        while (current < effectiveEndExclusive)
        {
            if (current >= rangeStart)
                count++;

            var next = AddFrequency(current, frequencyIndex);

            if (next <= current)
                break;

            current = next;
        }

        return count;
    }

    private static DateTime AddFrequency(DateTime date, int frequencyIndex)
    {
        return frequencyIndex switch
        {
            0 => date.AddDays(7),   // Weekly
            1 => date.AddDays(14),  // Bi-Weekly
            2 => date.AddMonths(1), // Monthly
            3 => date.AddMonths(3), // Quarterly
            4 => date.AddYears(1),  // Yearly
            _ => date
        };
    }

    private static bool GridContainsSortableProperty(DataGridView grid, string propertyName)
    {
        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (GetSortablePropertyName(column) == propertyName)
                return true;
        }

        return false;
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

        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = AppConfig.ThemePanel;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = AppConfig.ThemeText;

        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(38, 38, 38);
    }

    #endregion
}
