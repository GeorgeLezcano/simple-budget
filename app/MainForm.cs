using System;
using System.Drawing;
using System.Windows.Forms;

namespace App;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void ShowNotImplemented(string feature)
        => MessageBox.Show($"Not implemented\n\nFeature: {feature}", "Simple Budget");

    // ===== Menu =====
    private void FileExitClicked(object? sender, EventArgs e)
    {
        // TODO: confirm exit if there are unsaved changes once persistence exists
        Close();
    }

    private void ViewRefreshClicked(object? sender, EventArgs e)
    {
        // TODO: re-load current scope (week/month/year) and refresh grids + totals
        ShowNotImplemented("Refresh (reload data)");
    }

    private void HelpDocsClicked(object? sender, EventArgs e)
    {
        // TODO: open local docs or project website (later)
        ShowNotImplemented("Documentation");
    }

    private void HelpAboutClicked(object? sender, EventArgs e)
    {
        // TODO: show About dialog with version, author, license, links
        MessageBox.Show("Simple Budget\nv0.1.0 (placeholder)\n\nA simple income/expense tracker.", "About");
    }

    // ===== Dashboard =====
    private void SummaryMonthChanged(object? sender, EventArgs e)
    {
        // TODO: load totals + grids for selected scope + anchor date
        ShowNotImplemented("Change dashboard date");
    }

    // ===== Income =====
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

    // ===== Expenses =====
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

    // ===== Reports =====
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

    // ===== Settings =====
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

    private void TabMain_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (sender is not TabControl tc) return;

        var page = tc.TabPages[e.Index];
        var isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

        var r = e.Bounds;
        r.Inflate(-2, -2);

        var bg = isSelected ? ThemeTabActive : ThemeTabInactive;
        var fg = isSelected ? Color.White : ThemeText;
        var border = ThemeBorder;

        using var backBrush = new SolidBrush(bg);
        using var textBrush = new SolidBrush(fg);
        using var pen = new Pen(border);

        e.Graphics.FillRectangle(backBrush, r);
        e.Graphics.DrawRectangle(pen, r);

        var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        e.Graphics.DrawString(page.Text, Font, textBrush, r, sf);
    }
}
