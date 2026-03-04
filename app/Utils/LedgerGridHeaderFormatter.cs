using App.Constants;
using App.Data.Entities;

namespace App.Utils;

/// <summary>
/// Helper class to format headers in grids
/// </summary>
public static class LedgerGridHeaderFormatter
{
    private static readonly HashSet<DataGridView> _grids = [];

    /// <summary>
    /// Registers the grid.
    /// </summary>
    /// <param name="grid"></param>
    public static void Register(DataGridView grid)
    {
        if (grid is null) return;

        if (_grids.Add(grid))
        {
            grid.DataBindingComplete += (_, __) => Apply(grid);
            grid.ColumnAdded += (_, __) => Apply(grid);
        }

        Apply(grid);
    }

    /// <summary>
    /// Refreshes all grid headers.
    /// </summary>
    public static void RefreshAll()
    {
        foreach (var g in _grids)
            Apply(g);
    }

    /// <summary>
    /// Applies the updated state to the grid.
    /// </summary>
    /// <param name="grid"></param>
    private static void Apply(DataGridView grid)
    {
        if (grid.Columns.Count == 0) return;

        if (grid.Columns.Contains(nameof(LedgerEntry.Id)))
            grid.Columns[nameof(LedgerEntry.Id)].Visible = false;

        if (grid.Columns.Contains(nameof(LedgerEntry.Type)))
            grid.Columns[nameof(LedgerEntry.Type)].Visible = false;

        var lang = LabelFormatter.SelectedLanguage;

        if (grid.Columns.Contains(nameof(LedgerEntry.TransactionDate)))
            grid.Columns[nameof(LedgerEntry.TransactionDate)].HeaderText =
                lang == Language.SPANISH ? "Fecha de Transacción" : "Transaction Date";

        if (grid.Columns.Contains(nameof(LedgerEntry.CreatedAt)))
            grid.Columns[nameof(LedgerEntry.CreatedAt)].HeaderText =
                lang == Language.SPANISH ? "Fecha de Creación" : "Creation Date";

        if (grid.Columns.Contains(nameof(LedgerEntry.Category)))
            grid.Columns[nameof(LedgerEntry.Category)].HeaderText =
                lang == Language.SPANISH ? "Categoría" : "Category";

        if (grid.Columns.Contains(nameof(LedgerEntry.Amount)))
            grid.Columns[nameof(LedgerEntry.Amount)].HeaderText =
                lang == Language.SPANISH ? "Cantidad" : "Amount";

        if (grid.Columns.Contains(nameof(LedgerEntry.Recurring)))
            grid.Columns[nameof(LedgerEntry.Recurring)].HeaderText =
                lang == Language.SPANISH ? "Repetitivo" : "Recurring";

        if (grid.Columns.Contains(nameof(LedgerEntry.Frequency)))
            grid.Columns[nameof(LedgerEntry.Frequency)].HeaderText =
                lang == Language.SPANISH ? "Frecuencia" : "Frequency";

        if (grid.Columns.Contains(nameof(LedgerEntry.Notes)))
            grid.Columns[nameof(LedgerEntry.Notes)].HeaderText =
                lang == Language.SPANISH ? "Notas" : "Notes";
    }
}