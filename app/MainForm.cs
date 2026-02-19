using App.Constants;
using App.Services;
using App.Utils;

namespace App;

public partial class MainForm : Form
{
    private readonly LedgerService _ledgerService;

    private readonly SettingsService _settingsService;

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
        _ledgerService = ledgerService;
        _settingsService = settingsService;
        Shown += MainForm_Shown;
    }

    #endregion

    #region Helpers

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

    private void LanguageEnglishClicked(object? sender, EventArgs e)
    {
        LabelFormatter.SelectedLanguage = Language.ENGLISH;
        //TODO RefreshAppLabels()
    }

    private void LanguageSpanishClicked(object? sender, EventArgs e)
    {
        LabelFormatter.SelectedLanguage = Language.SPANISH;
        //TODO RefreshAppLabels()
        ShowNotImplemented("Language: Spanish");
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

    private void IncomeRecurringChanged(object? sender, EventArgs e)
    {
        // TODO: enable/disable frequency selector based on recurring checkbox
        cbIncomeFrequency.Enabled = chkIncomeRecurring.Checked;
    }

    private void IncomeAddClicked(object? sender, EventArgs e)
    {
        // TODO: validate fields, insert income record into SQLite, refresh grid + totals
        ShowNotImplemented("Add Income");
    }

    private void IncomeClearClicked(object? sender, EventArgs e)
    {
        // TODO: clear income entry fields (and maybe reset to defaults)
        ShowNotImplemented("Clear Income Entry");
    }

    #endregion

    #region Expenses

    private void ExpenseRecurringChanged(object? sender, EventArgs e)
    {
        // TODO: enable/disable frequency selector based on recurring checkbox
        cbExpenseFrequency.Enabled = chkExpenseRecurring.Checked;
    }

    private void ExpenseAddClicked(object? sender, EventArgs e)
    {
        // TODO: validate fields, insert expense record into SQLite, refresh grid + totals
        ShowNotImplemented("Add Expense");
    }

    private void ExpenseClearClicked(object? sender, EventArgs e)
    {
        // TODO: clear expense entry fields
        ShowNotImplemented("Clear Expense Entry");
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
        // TODO: validate + add new income category, persist to config/db, update dropdowns
        ShowNotImplemented("Add Income Category");
    }

    private void RemoveIncomeTypeClicked(object? sender, EventArgs e)
    {
        // TODO: remove selected income category (with safety checks) and update dropdowns
        ShowNotImplemented("Remove Income Category");
    }

    private void AddExpenseTypeClicked(object? sender, EventArgs e)
    {
        // TODO: validate + add new expense category, persist to config/db, update dropdowns
        ShowNotImplemented("Add Expense Category");
    }

    private void RemoveExpenseTypeClicked(object? sender, EventArgs e)
    {
        // TODO: remove selected expense category (with safety checks) and update dropdowns
        ShowNotImplemented("Remove Expense Category");
    }

    private void SettingsSaveClicked(object? sender, EventArgs e)
    {
        // TODO: persist settings/categories and refresh all category dropdowns
        ShowNotImplemented("Save Settings");
    }

    private void SettingsResetDefaultsClicked(object? sender, EventArgs e)
    {
        // TODO: reset categories/settings to defaults
        ShowNotImplemented("Reset Defaults");
    }

    #endregion

    #region Tabs Rendering

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
    }

    #endregion

    #region Layout Helpers

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
