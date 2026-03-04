using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Constants;
using App.Utils;

namespace App;

partial class MainForm
{
    #region Designer Fields

    private IContainer components = null;

    // App chrome
    private MenuStrip menuMain;
    private ToolStripMenuItem menuHelp;
    private ToolStripMenuItem menuLanguage;
    private ToolStripMenuItem menuLanguageEnglish;
    private ToolStripMenuItem menuLanguageSpanish;
    private ToolStripMenuItem menuHelpDocs;
    private ToolStripMenuItem menuHelpAbout;

    private ToolTip toolTip;

    // Tabs
    private TabControl tabMain;
    private TabPage tabDashboard;
    private TabPage tabIncome;
    private TabPage tabExpenses;
    private TabPage tabReports;
    private TabPage tabSettings;

    // Dashboard
    private TableLayoutPanel tlpDashboard;
    private Panel pnlDashHeader;
    private Label lblDashTitle;
    private Label lblDashSubtitle;
    private Label lblDashMonth;
    private DateTimePicker dtpDashMonth;

    private GroupBox gbDashTotals;
    private TableLayoutPanel tlpDashTotals;
    private Label lblIncomeTotalTitle;
    private Label lblIncomeTotalValue;
    private Label lblExpenseTotalTitle;
    private Label lblExpenseTotalValue;
    private Label lblNetTotalTitle;
    private Label lblNetTotalValue;
    private Label lblSavingsTotalTitle;
    private Label lblSavingsTotalValue;

    private Label lblDashRange;
    private ComboBox cbDashRange;

    private GroupBox gbDashNextSteps;
    private Label lblDashHint;

    // Income
    private SplitContainer scIncome;
    private GroupBox gbIncomeEntry;
    private TableLayoutPanel tlpIncomeEntry;

    private Label lblIncomeCategory;
    private ComboBox cbIncomeCategory;

    private Label lblIncomeAmount;
    private NumericUpDown nudIncomeAmount;

    private Label lblIncomeDate;
    private DateTimePicker dtpIncomeDate;

    private Label lblIncomeNotes;
    private TextBox txtIncomeNotes;

    private FlowLayoutPanel flpIncomeButtons;
    private Button btnIncomeAdd;
    private Button btnIncomeClear;

    private GroupBox gbIncomeList;
    private DataGridView dgvIncome;

    private Panel pnlIncomeListActions;
    private FlowLayoutPanel flpIncomeListActions;
    private Button btnIncomeDeleteSelected;

    private CheckBox chkIncomeRecurring;
    private Label lblIncomeFrequency;
    private ComboBox cbIncomeFrequency;

    // Expenses
    private SplitContainer scExpenses;
    private GroupBox gbExpenseEntry;
    private TableLayoutPanel tlpExpenseEntry;

    private Label lblExpenseCategory;
    private ComboBox cbExpenseCategory;

    private Label lblExpenseAmount;
    private NumericUpDown nudExpenseAmount;

    private Label lblExpenseDate;
    private DateTimePicker dtpExpenseDate;

    private CheckBox chkExpenseRecurring;
    private Label lblExpenseFrequency;
    private ComboBox cbExpenseFrequency;

    private Label lblExpenseNotes;
    private TextBox txtExpenseNotes;

    private FlowLayoutPanel flpExpenseButtons;
    private Button btnExpenseAdd;
    private Button btnExpenseClear;

    private GroupBox gbExpenseList;
    private DataGridView dgvExpenses;

    private Panel pnlExpenseListActions;
    private FlowLayoutPanel flpExpenseListActions;
    private Button btnExpenseDeleteSelected;

    // Reports
    private SplitContainer scReports;
    private GroupBox gbReportFilters;
    private TableLayoutPanel tlpReportFilters;

    private Label lblReportHint;

    private Label lblReportFrom;
    private DateTimePicker dtpReportFrom;
    private Label lblReportTo;
    private DateTimePicker dtpReportTo;

    private Label lblReportMin;
    private NumericUpDown nudReportMin;
    private Label lblReportMax;
    private NumericUpDown nudReportMax;

    private Label lblReportSearch;
    private TextBox txtReportSearch;

    private Label lblReportScope;
    private ComboBox cbReportScope;

    private FlowLayoutPanel flpReportButtons;
    private Button btnReportRun;
    private Button btnReportClear;
    private Button btnExportPdf;
    private Button btnExportExcel;

    private GroupBox gbReportResults;
    private DataGridView dgvReportResults;

    // Settings
    private Panel pnlSettingsHeader;
    private Label lblSettingsTitle;
    private Label lblSettingsHint;

    private TableLayoutPanel tlpSettingsRoot;
    private GroupBox gbIncomeTypes;
    private GroupBox gbExpenseTypes;

    private TableLayoutPanel tlpIncomeTypes;
    private TableLayoutPanel tlpExpenseTypes;

    private ListBox lbIncomeTypes;
    private ListBox lbExpenseTypes;

    private TextBox txtNewIncomeType;
    private TextBox txtNewExpenseType;

    private Button btnAddIncomeType;
    private Button btnRemoveIncomeType;
    private Button btnAddExpenseType;
    private Button btnRemoveExpenseType;

    private GroupBox gbSavingsSettings;
    private TableLayoutPanel tlpSavingsSettings;
    private Label lblSavingsPercent;
    private NumericUpDown nudSavingsPercent;

    private Panel pnlSettingsActions;
    private FlowLayoutPanel flpSettingsActions;

    private Button btnSavingsSave;

    #endregion

    #region Dispose

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #endregion

    #region InitializeComponent

    /// <summary>
    /// Component Initialization.
    /// </summary>
    private void InitializeComponent()
    {
        #region Setup / Resources

        ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
        components = new Container();
        toolTip = new ToolTip(components);

        #endregion

        #region Instantiate Controls

        menuMain = new MenuStrip();
        menuHelp = new ToolStripMenuItem();
        menuLanguage = new ToolStripMenuItem();
        menuLanguageEnglish = new ToolStripMenuItem();
        menuLanguageSpanish = new ToolStripMenuItem();
        menuHelpDocs = new ToolStripMenuItem();
        menuHelpAbout = new ToolStripMenuItem();

        tabMain = new TabControl();
        tabDashboard = new TabPage();
        tabIncome = new TabPage();
        tabExpenses = new TabPage();
        tabReports = new TabPage();
        tabSettings = new TabPage();

        // Dashboard
        tlpDashboard = new TableLayoutPanel();
        pnlDashHeader = new Panel();
        lblDashTitle = new Label();
        lblDashSubtitle = new Label();
        lblDashMonth = new Label();
        dtpDashMonth = new DateTimePicker();

        gbDashTotals = new GroupBox();
        tlpDashTotals = new TableLayoutPanel();
        lblIncomeTotalTitle = new Label();
        lblIncomeTotalValue = new Label();
        lblExpenseTotalTitle = new Label();
        lblExpenseTotalValue = new Label();
        lblNetTotalTitle = new Label();
        lblNetTotalValue = new Label();
        lblSavingsTotalTitle = new Label();
        lblSavingsTotalValue = new Label();

        lblDashRange = new Label();
        cbDashRange = new ComboBox();

        gbDashNextSteps = new GroupBox();
        lblDashHint = new Label();

        // Income
        scIncome = new SplitContainer();
        gbIncomeEntry = new GroupBox();
        tlpIncomeEntry = new TableLayoutPanel();

        lblIncomeCategory = new Label();
        cbIncomeCategory = new ComboBox();

        lblIncomeAmount = new Label();
        nudIncomeAmount = new NumericUpDown();

        lblIncomeDate = new Label();
        dtpIncomeDate = new DateTimePicker();

        lblIncomeNotes = new Label();
        txtIncomeNotes = new TextBox();

        flpIncomeButtons = new FlowLayoutPanel();
        btnIncomeAdd = new Button();
        btnIncomeClear = new Button();

        gbIncomeList = new GroupBox();
        dgvIncome = new DataGridView();

        pnlIncomeListActions = new Panel();
        flpIncomeListActions = new FlowLayoutPanel();
        btnIncomeDeleteSelected = new Button();

        chkIncomeRecurring = new CheckBox();
        lblIncomeFrequency = new Label();
        cbIncomeFrequency = new ComboBox();

        // Expenses
        scExpenses = new SplitContainer();
        gbExpenseEntry = new GroupBox();
        tlpExpenseEntry = new TableLayoutPanel();

        lblExpenseCategory = new Label();
        cbExpenseCategory = new ComboBox();

        lblExpenseAmount = new Label();
        nudExpenseAmount = new NumericUpDown();

        lblExpenseDate = new Label();
        dtpExpenseDate = new DateTimePicker();

        chkExpenseRecurring = new CheckBox();
        lblExpenseFrequency = new Label();
        cbExpenseFrequency = new ComboBox();

        lblExpenseNotes = new Label();
        txtExpenseNotes = new TextBox();

        flpExpenseButtons = new FlowLayoutPanel();
        btnExpenseAdd = new Button();
        btnExpenseClear = new Button();

        gbExpenseList = new GroupBox();
        dgvExpenses = new DataGridView();

        pnlExpenseListActions = new Panel();
        flpExpenseListActions = new FlowLayoutPanel();
        btnExpenseDeleteSelected = new Button();

        // Reports
        scReports = new SplitContainer();
        gbReportFilters = new GroupBox();
        tlpReportFilters = new TableLayoutPanel();

        lblReportHint = new Label();

        lblReportFrom = new Label();
        dtpReportFrom = new DateTimePicker();
        lblReportTo = new Label();
        dtpReportTo = new DateTimePicker();

        lblReportMin = new Label();
        nudReportMin = new NumericUpDown();
        lblReportMax = new Label();
        nudReportMax = new NumericUpDown();

        lblReportSearch = new Label();
        txtReportSearch = new TextBox();

        lblReportScope = new Label();
        cbReportScope = new ComboBox();

        flpReportButtons = new FlowLayoutPanel();
        btnReportRun = new Button();
        btnReportClear = new Button();
        btnExportPdf = new Button();
        btnExportExcel = new Button();

        gbReportResults = new GroupBox();
        dgvReportResults = new DataGridView();

        // Settings
        pnlSettingsHeader = new Panel();
        lblSettingsTitle = new Label();
        lblSettingsHint = new Label();

        tlpSettingsRoot = new TableLayoutPanel();
        gbIncomeTypes = new GroupBox();
        gbExpenseTypes = new GroupBox();
        tlpIncomeTypes = new TableLayoutPanel();
        tlpExpenseTypes = new TableLayoutPanel();

        lbIncomeTypes = new ListBox();
        lbExpenseTypes = new ListBox();

        txtNewIncomeType = new TextBox();
        txtNewExpenseType = new TextBox();

        btnAddIncomeType = new Button();
        btnRemoveIncomeType = new Button();
        btnAddExpenseType = new Button();
        btnRemoveExpenseType = new Button();

        gbSavingsSettings = new GroupBox();
        tlpSavingsSettings = new TableLayoutPanel();
        lblSavingsPercent = new Label();
        nudSavingsPercent = new NumericUpDown();

        pnlSettingsActions = new Panel();
        flpSettingsActions = new FlowLayoutPanel();

        btnSavingsSave = new Button();

        #endregion

        #region SuspendLayout

        SuspendLayout();

        #endregion

        #region MainForm

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 800);
        MinimumSize = new Size(1100, 720);
        StartPosition = FormStartPosition.CenterScreen;
        Text = LabelFormatter.AppShellText();
        Icon = (Icon)resources.GetObject(AppConfig.IconName);
        BackColor = AppConfig.ThemeBack;
        ForeColor = AppConfig.ThemeText;

        #endregion

        #region MenuStrip

        menuMain.Dock = DockStyle.Top;
        menuMain.RenderMode = ToolStripRenderMode.System;

        menuLanguage.Text = "&Language";

        menuLanguage.Name = nameof(menuLanguage);
        menuLanguageEnglish.Text = "&English";
        menuLanguageEnglish.CheckOnClick = true;
        menuLanguageEnglish.Checked = true;
        menuLanguageEnglish.Click += LanguageEnglishClicked;
        menuLanguageEnglish.Name = nameof(menuLanguageEnglish);

        menuLanguageSpanish.Text = "&Español";
        menuLanguageSpanish.CheckOnClick = true;
        menuLanguageSpanish.Checked = false;
        menuLanguageSpanish.Click += LanguageSpanishClicked;
        menuLanguageSpanish.Name = nameof(menuLanguageSpanish);

        menuLanguage.DropDownItems.Add(menuLanguageEnglish);
        menuLanguage.DropDownItems.Add(menuLanguageSpanish);
        

        menuHelp.Text = "&Help";
        menuHelpDocs.Text = "&Documentation";
        menuHelpDocs.Click += HelpDocsClicked;
        menuHelpAbout.Text = "&About";
        menuHelpAbout.Click += HelpAboutClicked;
        menuHelp.DropDownItems.Add(menuHelpDocs);
        menuHelp.DropDownItems.Add(new ToolStripSeparator());
        menuHelp.DropDownItems.Add(menuHelpAbout);

        menuMain.Items.AddRange(new ToolStripItem[] { menuHelp, menuLanguage });
        MainMenuStrip = menuMain;

        #endregion

        #region Tabs

        tabMain.Dock = DockStyle.Fill;
        tabMain.DrawMode = TabDrawMode.OwnerDrawFixed;
        tabMain.SizeMode = TabSizeMode.Fixed;

        tabMain.ItemSize = new Size(150, 34);
        tabMain.Padding = new Point(14, 4);

        tabMain.DrawItem += TabMain_DrawItem;

        tabMain.Controls.Add(tabDashboard);
        tabMain.Controls.Add(tabIncome);
        tabMain.Controls.Add(tabExpenses);
        tabMain.Controls.Add(tabReports);
        tabMain.Controls.Add(tabSettings);

        #endregion

        #region Dashboard Tab

        tabDashboard.Text = "Dashboard";
        tabDashboard.BackColor = AppConfig.ThemeBack;
        tabDashboard.ForeColor = AppConfig.ThemeText;
        tabDashboard.Padding = new Padding(12);
        tabDashboard.UseVisualStyleBackColor = false;

        tlpDashboard.Dock = DockStyle.Fill;
        tlpDashboard.RowCount = 3;
        tlpDashboard.ColumnCount = 1;

        tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));

        pnlDashHeader.Dock = DockStyle.Fill;
        pnlDashHeader.BackColor = AppConfig.ThemePanel;

        lblDashTitle.AutoSize = true;
        lblDashTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblDashTitle.Text = "Simple Budget";
        lblDashTitle.Location = new Point(16, 12);

        lblDashSubtitle.AutoSize = true;
        lblDashSubtitle.ForeColor = AppConfig.ThemeMuted;
        lblDashSubtitle.Text = "Track income and expenses. View summaries by week, month, or year.";
        lblDashSubtitle.Location = new Point(18, 50);

        lblDashRange.AutoSize = true;
        lblDashRange.Text = "View:";
        lblDashRange.Location = new Point(580, 18);

        cbDashRange.DropDownStyle = ComboBoxStyle.DropDownList;
        cbDashRange.Items.AddRange(new object[] { "Week", "Month", "Year" });
        cbDashRange.SelectedIndex = 1;
        cbDashRange.Width = 110;
        cbDashRange.Location = new Point(640, 14);
        StyleComboBox(cbDashRange);

        toolTip.SetToolTip(cbDashRange,
            "View scope:\n" +
            "• Week = week containing the anchor date\n" +
            "• Month = month of the anchor date\n" +
            "• Year = calendar year of the anchor date\n\n");

        lblDashMonth.AutoSize = true;
        lblDashMonth.Text = "Anchor date:";
        lblDashMonth.Location = new Point(780, 18);

        dtpDashMonth.Format = DateTimePickerFormat.Custom;
        dtpDashMonth.CustomFormat = "MMM dd, yyyy";
        dtpDashMonth.Width = 160;
        dtpDashMonth.Location = new Point(870, 14);
        dtpDashMonth.ValueChanged += SummaryMonthChanged;
        StyleDateTimePicker(dtpDashMonth);
        toolTip.SetToolTip(dtpDashMonth, "Pick a date to anchor Week/Month/Year views.");

        pnlDashHeader.Controls.Add(lblDashTitle);
        pnlDashHeader.Controls.Add(lblDashSubtitle);
        pnlDashHeader.Controls.Add(lblDashRange);
        pnlDashHeader.Controls.Add(cbDashRange);
        pnlDashHeader.Controls.Add(lblDashMonth);
        pnlDashHeader.Controls.Add(dtpDashMonth);

        gbDashTotals.Dock = DockStyle.Fill;
        gbDashTotals.Text = "Summary";
        StyleGroupBox(gbDashTotals);

        tlpDashTotals.Dock = DockStyle.Fill;
        tlpDashTotals.Padding = new Padding(12);
        tlpDashTotals.ColumnCount = 2;
        tlpDashTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
        tlpDashTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        tlpDashTotals.RowCount = 4;

        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

        lblIncomeTotalTitle.AutoSize = true;
        lblIncomeTotalTitle.Text = "Total Income";
        lblIncomeTotalValue.AutoSize = true;
        lblIncomeTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblIncomeTotalValue.Text = "-";

        lblExpenseTotalTitle.AutoSize = true;
        lblExpenseTotalTitle.Text = "Total Expenses";
        lblExpenseTotalValue.AutoSize = true;
        lblExpenseTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblExpenseTotalValue.Text = "-";

        lblSavingsTotalTitle.AutoSize = true;
        lblSavingsTotalTitle.Text = "Savings";
        lblSavingsTotalValue.AutoSize = true;
        lblSavingsTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblSavingsTotalValue.Text = "-";

        lblNetTotalTitle.AutoSize = true;
        lblNetTotalTitle.Text = "Net";
        lblNetTotalValue.AutoSize = true;
        lblNetTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblNetTotalValue.Text = "-";

        tlpDashTotals.Controls.Add(lblIncomeTotalTitle, 0, 0);
        tlpDashTotals.Controls.Add(lblIncomeTotalValue, 1, 0);
        tlpDashTotals.Controls.Add(lblExpenseTotalTitle, 0, 1);
        tlpDashTotals.Controls.Add(lblExpenseTotalValue, 1, 1);
        tlpDashTotals.Controls.Add(lblSavingsTotalTitle, 0, 2);
        tlpDashTotals.Controls.Add(lblSavingsTotalValue, 1, 2);
        tlpDashTotals.Controls.Add(lblNetTotalTitle, 0, 3);
        tlpDashTotals.Controls.Add(lblNetTotalValue, 1, 3);

        gbDashTotals.Controls.Add(tlpDashTotals);

        gbDashNextSteps.Dock = DockStyle.Fill;
        gbDashNextSteps.Text = "Next steps";
        gbDashNextSteps.Name = nameof(gbDashNextSteps);
        StyleGroupBox(gbDashNextSteps);

        lblDashHint.Dock = DockStyle.Fill;
        lblDashHint.ForeColor = AppConfig.ThemeMuted;
        lblDashHint.Text ="";
        lblDashHint.Padding = new Padding(12);
        lblDashHint.Name = nameof(lblDashHint);

        gbDashNextSteps.Controls.Add(lblDashHint);

        tlpDashboard.Controls.Add(pnlDashHeader, 0, 0);
        tlpDashboard.Controls.Add(gbDashTotals, 0, 1);
        tlpDashboard.Controls.Add(gbDashNextSteps, 0, 2);

        tabDashboard.Controls.Add(tlpDashboard);

        #endregion

        #region Income Tab

        tabIncome.Text = "Income";
        tabIncome.BackColor = AppConfig.ThemeBack;
        tabIncome.ForeColor = AppConfig.ThemeText;
        tabIncome.Padding = new Padding(12);
        tabIncome.UseVisualStyleBackColor = false;

        scIncome.Dock = DockStyle.Fill;
        scIncome.Orientation = Orientation.Horizontal;

        scIncome.IsSplitterFixed = false;
        scIncome.SplitterWidth = 6;

        gbIncomeEntry.Dock = DockStyle.Fill;
        gbIncomeEntry.Text = "Add Income";
        StyleGroupBox(gbIncomeEntry);

        tlpIncomeEntry.Dock = DockStyle.Fill;
        tlpIncomeEntry.Padding = new Padding(12);
        tlpIncomeEntry.ColumnCount = 2;
        tlpIncomeEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        tlpIncomeEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpIncomeEntry.RowCount = 6;
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

        lblIncomeCategory.Text = "Category:";
        lblIncomeCategory.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeCategory.Dock = DockStyle.Fill;

        cbIncomeCategory.Dock = DockStyle.Fill;
        cbIncomeCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        StyleComboBox(cbIncomeCategory);

        lblIncomeAmount.Text = "Amount:";
        lblIncomeAmount.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeAmount.Dock = DockStyle.Fill;

        nudIncomeAmount.Dock = DockStyle.Fill;
        nudIncomeAmount.DecimalPlaces = 2;
        nudIncomeAmount.Maximum = 100000000;
        StyleNumeric(nudIncomeAmount);

        lblIncomeDate.Text = "Date:";
        lblIncomeDate.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeDate.Dock = DockStyle.Fill;

        dtpIncomeDate.Dock = DockStyle.Fill;
        StyleDateTimePicker(dtpIncomeDate);

        chkIncomeRecurring.Text = "Recurring";
        chkIncomeRecurring.AutoSize = true;
        chkIncomeRecurring.CheckedChanged += IncomeRecurringChanged;

        lblIncomeFrequency.Text = "Frequency:";
        lblIncomeFrequency.AutoSize = true;

        cbIncomeFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
        cbIncomeFrequency.Items.AddRange(AppConfig.TransactionFrequency);
        cbIncomeFrequency.Enabled = false;
        cbIncomeFrequency.Width = 150;
        StyleComboBox(cbIncomeFrequency);

        var pnlIncomeRecurring = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 1,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };
        pnlIncomeRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        pnlIncomeRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 14));
        pnlIncomeRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        pnlIncomeRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        chkIncomeRecurring.Margin = new Padding(0, 8, 0, 0);
        chkIncomeRecurring.Anchor = AnchorStyles.Left;

        lblIncomeFrequency.Margin = new Padding(0, 9, 0, 0);
        lblIncomeFrequency.Anchor = AnchorStyles.Left;

        cbIncomeFrequency.Margin = new Padding(6, 6, 0, 0);
        cbIncomeFrequency.Anchor = AnchorStyles.Left;

        pnlIncomeRecurring.Controls.Add(chkIncomeRecurring, 0, 0);
        pnlIncomeRecurring.Controls.Add(new Label { Width = 14, Text = "", Margin = new Padding(0) }, 1, 0);
        pnlIncomeRecurring.Controls.Add(lblIncomeFrequency, 2, 0);
        pnlIncomeRecurring.Controls.Add(cbIncomeFrequency, 3, 0);

        lblIncomeNotes.Text = "Notes:";
        lblIncomeNotes.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeNotes.Dock = DockStyle.Fill;

        txtIncomeNotes.Dock = DockStyle.Fill;
        txtIncomeNotes.BorderStyle = BorderStyle.FixedSingle;
        txtIncomeNotes.Multiline = true;
        StyleTextBox(txtIncomeNotes);

        flpIncomeButtons.Dock = DockStyle.Fill;
        flpIncomeButtons.FlowDirection = FlowDirection.LeftToRight;
        flpIncomeButtons.WrapContents = false;
        flpIncomeButtons.Padding = new Padding(0, 6, 0, 0);

        btnIncomeAdd.Text = "Add Income";
        btnIncomeAdd.Width = 160;
        btnIncomeAdd.Click += IncomeAddClicked;

        btnIncomeClear.Text = "Clear";
        btnIncomeClear.Width = 120;
        btnIncomeClear.Click += IncomeClearClicked;

        StyleButton(btnIncomeAdd);
        StyleButton(btnIncomeClear);

        flpIncomeButtons.Controls.Add(btnIncomeAdd);
        flpIncomeButtons.Controls.Add(btnIncomeClear);

        tlpIncomeEntry.Controls.Add(lblIncomeCategory, 0, 0);
        tlpIncomeEntry.Controls.Add(cbIncomeCategory, 1, 0);

        tlpIncomeEntry.Controls.Add(lblIncomeAmount, 0, 1);
        tlpIncomeEntry.Controls.Add(nudIncomeAmount, 1, 1);

        tlpIncomeEntry.Controls.Add(lblIncomeDate, 0, 2);
        tlpIncomeEntry.Controls.Add(dtpIncomeDate, 1, 2);

        tlpIncomeEntry.Controls.Add(new Label { Text = "", Dock = DockStyle.Fill }, 0, 3);
        tlpIncomeEntry.Controls.Add(pnlIncomeRecurring, 1, 3);

        tlpIncomeEntry.Controls.Add(lblIncomeNotes, 0, 4);
        tlpIncomeEntry.Controls.Add(txtIncomeNotes, 1, 4);

        tlpIncomeEntry.Controls.Add(flpIncomeButtons, 0, 5);
        tlpIncomeEntry.SetColumnSpan(flpIncomeButtons, 2);


        gbIncomeEntry.Controls.Add(tlpIncomeEntry);

        gbIncomeList.Dock = DockStyle.Fill;
        gbIncomeList.Text = "Income List";
        StyleGroupBox(gbIncomeList);

        var tlpIncomeList = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(0),
        };
        tlpIncomeList.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpIncomeList.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

        dgvIncome.Dock = DockStyle.Fill;
        dgvIncome.AllowUserToAddRows = false;
        dgvIncome.AllowUserToDeleteRows = false;
        dgvIncome.ReadOnly = true;
        dgvIncome.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvIncome.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvIncome.MultiSelect = false;
        ApplyDataGridTheme(dgvIncome);

        pnlIncomeListActions.Dock = DockStyle.Fill;
        pnlIncomeListActions.BackColor = AppConfig.ThemePanel;

        flpIncomeListActions.Dock = DockStyle.Fill;
        flpIncomeListActions.FlowDirection = FlowDirection.LeftToRight;
        flpIncomeListActions.WrapContents = false;
        flpIncomeListActions.Padding = new Padding(12, 10, 12, 10);

        btnIncomeDeleteSelected.Text = "Delete Selected";
        btnIncomeDeleteSelected.Width = 160;
        StyleButton(btnIncomeDeleteSelected);
        btnIncomeDeleteSelected.Click += IncomeDeleteSelectedClicked;

        flpIncomeListActions.Controls.Add(btnIncomeDeleteSelected);
        pnlIncomeListActions.Controls.Add(flpIncomeListActions);

        tlpIncomeList.Controls.Add(dgvIncome, 0, 0);
        tlpIncomeList.Controls.Add(pnlIncomeListActions, 0, 1);

        gbIncomeList.Controls.Add(tlpIncomeList);

        scIncome.Panel1.Controls.Add(gbIncomeEntry);
        scIncome.Panel2.Controls.Add(gbIncomeList);

        tabIncome.Controls.Add(scIncome);

        #endregion

        #region Expenses Tab

        tabExpenses.Text = "Expenses";
        tabExpenses.BackColor = AppConfig.ThemeBack;
        tabExpenses.ForeColor = AppConfig.ThemeText;
        tabExpenses.Padding = new Padding(12);
        tabExpenses.UseVisualStyleBackColor = false;

        scExpenses.Dock = DockStyle.Fill;
        scExpenses.Orientation = Orientation.Horizontal;

        scExpenses.IsSplitterFixed = false;
        scExpenses.SplitterWidth = 6;

        gbExpenseEntry.Dock = DockStyle.Fill;
        gbExpenseEntry.Text = "Add Expense";
        StyleGroupBox(gbExpenseEntry);

        tlpExpenseEntry.Dock = DockStyle.Fill;
        tlpExpenseEntry.Padding = new Padding(12);
        tlpExpenseEntry.ColumnCount = 2;
        tlpExpenseEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        tlpExpenseEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpExpenseEntry.RowCount = 6;
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

        lblExpenseCategory.Text = "Category:";
        lblExpenseCategory.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseCategory.Dock = DockStyle.Fill;

        cbExpenseCategory.Dock = DockStyle.Fill;
        cbExpenseCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        StyleComboBox(cbExpenseCategory);

        lblExpenseAmount.Text = "Amount:";
        lblExpenseAmount.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseAmount.Dock = DockStyle.Fill;

        nudExpenseAmount.Dock = DockStyle.Fill;
        nudExpenseAmount.DecimalPlaces = 2;
        nudExpenseAmount.Maximum = 100000000;
        StyleNumeric(nudExpenseAmount);

        lblExpenseDate.Text = "Date:";
        lblExpenseDate.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseDate.Dock = DockStyle.Fill;

        dtpExpenseDate.Dock = DockStyle.Fill;
        StyleDateTimePicker(dtpExpenseDate);

        chkExpenseRecurring.Text = "Recurring";
        chkExpenseRecurring.AutoSize = true;
        chkExpenseRecurring.CheckedChanged += ExpenseRecurringChanged;

        lblExpenseFrequency.Text = "Frequency:";
        lblExpenseFrequency.AutoSize = true;

        cbExpenseFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
        cbExpenseFrequency.Items.AddRange(AppConfig.TransactionFrequency);
        cbExpenseFrequency.Enabled = false;
        cbExpenseFrequency.Width = 150;
        StyleComboBox(cbExpenseFrequency);

        var pnlRecurring = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 1,
            Padding = new Padding(0),
            Margin = new Padding(0)
        };
        pnlRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        pnlRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 14));
        pnlRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        pnlRecurring.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        chkExpenseRecurring.Margin = new Padding(0, 8, 0, 0);
        chkExpenseRecurring.Anchor = AnchorStyles.Left;

        lblExpenseFrequency.Margin = new Padding(0, 9, 0, 0);
        lblExpenseFrequency.Anchor = AnchorStyles.Left;

        cbExpenseFrequency.Margin = new Padding(6, 6, 0, 0);
        cbExpenseFrequency.Anchor = AnchorStyles.Left;

        pnlRecurring.Controls.Add(chkExpenseRecurring, 0, 0);
        pnlRecurring.Controls.Add(new Label { Width = 14, Text = "", Margin = new Padding(0) }, 1, 0);
        pnlRecurring.Controls.Add(lblExpenseFrequency, 2, 0);
        pnlRecurring.Controls.Add(cbExpenseFrequency, 3, 0);

        lblExpenseNotes.Text = "Notes:";
        lblExpenseNotes.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseNotes.Dock = DockStyle.Fill;

        txtExpenseNotes.Dock = DockStyle.Fill;
        txtExpenseNotes.BorderStyle = BorderStyle.FixedSingle;
        txtExpenseNotes.Multiline = true;
        StyleTextBox(txtExpenseNotes);

        flpExpenseButtons.Dock = DockStyle.Fill;
        flpExpenseButtons.FlowDirection = FlowDirection.LeftToRight;
        flpExpenseButtons.WrapContents = false;
        flpExpenseButtons.Padding = new Padding(0, 6, 0, 0);

        btnExpenseAdd.Text = "Add Expense";
        btnExpenseAdd.Width = 160;
        btnExpenseAdd.Click += ExpenseAddClicked;

        btnExpenseClear.Text = "Clear";
        btnExpenseClear.Width = 120;
        btnExpenseClear.Click += ExpenseClearClicked;

        StyleButton(btnExpenseAdd);
        StyleButton(btnExpenseClear);

        flpExpenseButtons.Controls.Add(btnExpenseAdd);
        flpExpenseButtons.Controls.Add(btnExpenseClear);

        tlpExpenseEntry.Controls.Add(lblExpenseCategory, 0, 0);
        tlpExpenseEntry.Controls.Add(cbExpenseCategory, 1, 0);

        tlpExpenseEntry.Controls.Add(lblExpenseAmount, 0, 1);
        tlpExpenseEntry.Controls.Add(nudExpenseAmount, 1, 1);

        tlpExpenseEntry.Controls.Add(lblExpenseDate, 0, 2);
        tlpExpenseEntry.Controls.Add(dtpExpenseDate, 1, 2);

        tlpExpenseEntry.Controls.Add(new Label { Text = "", Dock = DockStyle.Fill }, 0, 3);
        tlpExpenseEntry.Controls.Add(pnlRecurring, 1, 3);

        tlpExpenseEntry.Controls.Add(lblExpenseNotes, 0, 4);
        tlpExpenseEntry.Controls.Add(txtExpenseNotes, 1, 4);

        tlpExpenseEntry.Controls.Add(flpExpenseButtons, 0, 5);
        tlpExpenseEntry.SetColumnSpan(flpExpenseButtons, 2);

        gbExpenseEntry.Controls.Add(tlpExpenseEntry);

        gbExpenseList.Dock = DockStyle.Fill;
        gbExpenseList.Text = "Expense List";
        StyleGroupBox(gbExpenseList);

        var tlpExpenseList = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(0),
        };
        tlpExpenseList.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpExpenseList.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

        dgvExpenses.Dock = DockStyle.Fill;
        dgvExpenses.AllowUserToAddRows = false;
        dgvExpenses.AllowUserToDeleteRows = false;
        dgvExpenses.ReadOnly = true;
        dgvExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvExpenses.MultiSelect = false;
        ApplyDataGridTheme(dgvExpenses);

        pnlExpenseListActions.Dock = DockStyle.Fill;
        pnlExpenseListActions.BackColor = AppConfig.ThemePanel;

        flpExpenseListActions.Dock = DockStyle.Fill;
        flpExpenseListActions.FlowDirection = FlowDirection.LeftToRight;
        flpExpenseListActions.WrapContents = false;
        flpExpenseListActions.Padding = new Padding(12, 10, 12, 10);

        btnExpenseDeleteSelected.Text = "Delete Selected";
        btnExpenseDeleteSelected.Width = 160;
        StyleButton(btnExpenseDeleteSelected);
        btnExpenseDeleteSelected.Click += ExpenseDeleteSelectedClicked;

        flpExpenseListActions.Controls.Add(btnExpenseDeleteSelected);
        pnlExpenseListActions.Controls.Add(flpExpenseListActions);

        tlpExpenseList.Controls.Add(dgvExpenses, 0, 0);
        tlpExpenseList.Controls.Add(pnlExpenseListActions, 0, 1);

        gbExpenseList.Controls.Add(tlpExpenseList);

        scExpenses.Panel1.Controls.Add(gbExpenseEntry);
        scExpenses.Panel2.Controls.Add(gbExpenseList);

        tabExpenses.Controls.Add(scExpenses);

        #endregion

        #region Reports Tab

        tabReports.Text = "Reports";
        tabReports.BackColor = AppConfig.ThemeBack;
        tabReports.ForeColor = AppConfig.ThemeText;
        tabReports.Padding = new Padding(12);
        tabReports.UseVisualStyleBackColor = false;

        scReports.Dock = DockStyle.Fill;
        scReports.Orientation = Orientation.Vertical;

        scReports.HandleCreated += (_, __) =>
        {
            scReports.Panel1MinSize = 380;
            scReports.Panel2MinSize = 380;

            var w = scReports.ClientSize.Width;
            if (w <= 0) return;

            var target = w / 2; // ~50/50

            var min = scReports.Panel1MinSize;
            var max = Math.Max(min, w - scReports.Panel2MinSize);

            scReports.SplitterDistance = Math.Max(min, Math.Min(target, max));
        };

        gbReportFilters.Dock = DockStyle.Fill;
        gbReportFilters.Text = "Filters";
        StyleGroupBox(gbReportFilters);

        tlpReportFilters.Dock = DockStyle.Fill;
        tlpReportFilters.Padding = new Padding(12);
        tlpReportFilters.ColumnCount = 2;
        tlpReportFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        tlpReportFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpReportFilters.RowCount = 8;

        tlpReportFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        for (int i = 0; i < 6; i++)
            tlpReportFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        tlpReportFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        lblReportHint.Dock = DockStyle.Fill;
        lblReportHint.ForeColor = AppConfig.ThemeMuted;
        lblReportHint.Text = "Filters stack. Leave blank to ignore.";
        lblReportHint.TextAlign = ContentAlignment.MiddleLeft;

        lblReportFrom.Text = "From:";
        lblReportFrom.TextAlign = ContentAlignment.MiddleLeft;
        lblReportFrom.Dock = DockStyle.Fill;

        dtpReportFrom.Dock = DockStyle.Fill;
        StyleDateTimePicker(dtpReportFrom);

        lblReportTo.Text = "To:";
        lblReportTo.TextAlign = ContentAlignment.MiddleLeft;
        lblReportTo.Dock = DockStyle.Fill;

        dtpReportTo.Dock = DockStyle.Fill;
        StyleDateTimePicker(dtpReportTo);

        lblReportMin.Text = "Amount Min:";
        lblReportMin.TextAlign = ContentAlignment.MiddleLeft;
        lblReportMin.Dock = DockStyle.Fill;

        nudReportMin.Dock = DockStyle.Fill;
        nudReportMin.DecimalPlaces = 2;
        nudReportMin.Maximum = 100000000;
        StyleNumeric(nudReportMin);

        lblReportMax.Text = "Amount Max:";
        lblReportMax.TextAlign = ContentAlignment.MiddleLeft;
        lblReportMax.Dock = DockStyle.Fill;

        nudReportMax.Dock = DockStyle.Fill;
        nudReportMax.DecimalPlaces = 2;
        nudReportMax.Maximum = 100000000;
        StyleNumeric(nudReportMax);

        lblReportSearch.Text = "Search:";
        lblReportSearch.TextAlign = ContentAlignment.MiddleLeft;
        lblReportSearch.Dock = DockStyle.Fill;

        txtReportSearch.Dock = DockStyle.Fill;
        txtReportSearch.BorderStyle = BorderStyle.FixedSingle;
        StyleTextBox(txtReportSearch);

        lblReportScope.Text = "Scope:";
        lblReportScope.TextAlign = ContentAlignment.MiddleLeft;
        lblReportScope.Dock = DockStyle.Fill;

        cbReportScope.Dock = DockStyle.Fill;
        cbReportScope.DropDownStyle = ComboBoxStyle.DropDownList;
        cbReportScope.Items.AddRange(new object[] { "All", "Income Only", "Expenses Only" });
        cbReportScope.SelectedIndex = 0;
        StyleComboBox(cbReportScope);

        flpReportButtons.Dock = DockStyle.Fill;
        flpReportButtons.FlowDirection = FlowDirection.LeftToRight;
        flpReportButtons.WrapContents = true;
        flpReportButtons.Padding = new Padding(0, 6, 0, 0);

        btnReportRun.Text = "Run";
        btnReportRun.Width = 120;
        btnReportRun.Click += ReportRunClicked;

        btnReportClear.Text = "Clear";
        btnReportClear.Width = 120;
        btnReportClear.Click += ReportClearClicked;

        btnExportPdf.Text = "Export PDF";
        btnExportPdf.Width = 130;
        btnExportPdf.Click += ExportPdfClicked;

        btnExportExcel.Text = "Export Excel";
        btnExportExcel.Width = 130;
        btnExportExcel.Click += ExportExcelClicked;

        StyleButton(btnReportRun);
        StyleButton(btnReportClear);
        StyleButton(btnExportPdf);
        StyleButton(btnExportExcel);

        flpReportButtons.Controls.Add(btnReportRun);
        flpReportButtons.Controls.Add(btnReportClear);
        flpReportButtons.Controls.Add(btnExportPdf);
        flpReportButtons.Controls.Add(btnExportExcel);

        tlpReportFilters.Controls.Add(lblReportHint, 0, 0);
        tlpReportFilters.SetColumnSpan(lblReportHint, 2);

        tlpReportFilters.Controls.Add(lblReportFrom, 0, 1);
        tlpReportFilters.Controls.Add(dtpReportFrom, 1, 1);

        tlpReportFilters.Controls.Add(lblReportTo, 0, 2);
        tlpReportFilters.Controls.Add(dtpReportTo, 1, 2);

        tlpReportFilters.Controls.Add(lblReportMin, 0, 3);
        tlpReportFilters.Controls.Add(nudReportMin, 1, 3);

        tlpReportFilters.Controls.Add(lblReportMax, 0, 4);
        tlpReportFilters.Controls.Add(nudReportMax, 1, 4);

        tlpReportFilters.Controls.Add(lblReportSearch, 0, 5);
        tlpReportFilters.Controls.Add(txtReportSearch, 1, 5);

        tlpReportFilters.Controls.Add(lblReportScope, 0, 6);
        tlpReportFilters.Controls.Add(cbReportScope, 1, 6);

        tlpReportFilters.Controls.Add(flpReportButtons, 0, 7);
        tlpReportFilters.SetColumnSpan(flpReportButtons, 2);

        gbReportFilters.Controls.Add(tlpReportFilters);

        gbReportResults.Dock = DockStyle.Fill;
        gbReportResults.Text = "Results";
        StyleGroupBox(gbReportResults);

        dgvReportResults.Dock = DockStyle.Fill;
        dgvReportResults.AllowUserToAddRows = false;
        dgvReportResults.AllowUserToDeleteRows = false;
        dgvReportResults.ReadOnly = true;
        dgvReportResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        ApplyDataGridTheme(dgvReportResults);

        gbReportResults.Controls.Add(dgvReportResults);

        scReports.Panel1.Controls.Add(gbReportFilters);
        scReports.Panel2.Controls.Add(gbReportResults);

        tabReports.Controls.Add(scReports);

        #endregion

        #region Settings Tab

        tabSettings.Text = "Settings";
        tabSettings.BackColor = AppConfig.ThemeBack;
        tabSettings.ForeColor = AppConfig.ThemeText;
        tabSettings.Padding = new Padding(12);
        tabSettings.UseVisualStyleBackColor = false;

        pnlSettingsHeader.Dock = DockStyle.Top;
        pnlSettingsHeader.Height = 74;
        pnlSettingsHeader.BackColor = AppConfig.ThemePanel;

        lblSettingsTitle.AutoSize = true;
        lblSettingsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblSettingsTitle.Text = "Settings";
        lblSettingsTitle.Location = new Point(16, 12);

        lblSettingsHint.AutoSize = true;
        lblSettingsHint.ForeColor = AppConfig.ThemeMuted;
        lblSettingsHint.Text = "Add categories here. They will show up in Income/Expenses dropdowns.";
        lblSettingsHint.Location = new Point(18, 44);

        pnlSettingsHeader.Controls.Add(lblSettingsTitle);
        pnlSettingsHeader.Controls.Add(lblSettingsHint);

        tlpSettingsRoot.Dock = DockStyle.Fill;
        tlpSettingsRoot.ColumnCount = 2;
        tlpSettingsRoot.RowCount = 3;
        tlpSettingsRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tlpSettingsRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tlpSettingsRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpSettingsRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
        tlpSettingsRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
        tlpSettingsRoot.Padding = new Padding(0, 10, 0, 0);

        gbIncomeTypes.Dock = DockStyle.Fill;
        gbIncomeTypes.Text = "Income Categories";
        StyleGroupBox(gbIncomeTypes);

        gbExpenseTypes.Dock = DockStyle.Fill;
        gbExpenseTypes.Text = "Expense Categories";
        StyleGroupBox(gbExpenseTypes);

        // Income types layout
        tlpIncomeTypes.Dock = DockStyle.Fill;
        tlpIncomeTypes.Padding = new Padding(12);
        tlpIncomeTypes.ColumnCount = 1;
        tlpIncomeTypes.RowCount = 3;
        tlpIncomeTypes.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpIncomeTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        tlpIncomeTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

        lbIncomeTypes.Dock = DockStyle.Fill;
        StyleListBox(lbIncomeTypes);

        var pnlIncomeAddRow = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(0),
            Margin = new Padding(0),
        };
        pnlIncomeAddRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        pnlIncomeAddRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));

        txtNewIncomeType.PlaceholderText = "New income category...";
        txtNewIncomeType.BorderStyle = BorderStyle.FixedSingle;
        txtNewIncomeType.Dock = DockStyle.Fill;
        txtNewIncomeType.Margin = new Padding(0, 8, 8, 0);
        txtNewIncomeType.AutoSize = false;
        txtNewIncomeType.Height = 32;
        StyleTextBox(txtNewIncomeType);

        btnAddIncomeType.Text = "Add Category";
        btnAddIncomeType.Dock = DockStyle.Fill;
        btnAddIncomeType.Margin = new Padding(0, 6, 0, 0);
        btnAddIncomeType.Click += AddIncomeTypeClicked;
        StyleButton(btnAddIncomeType);

        pnlIncomeAddRow.Controls.Add(txtNewIncomeType, 0, 0);
        pnlIncomeAddRow.Controls.Add(btnAddIncomeType, 1, 0);

        btnRemoveIncomeType.Text = "Remove Selected";
        btnRemoveIncomeType.Width = 180;
        btnRemoveIncomeType.Click += RemoveIncomeTypeClicked;
        StyleButton(btnRemoveIncomeType);

        var pnlIncomeRemoveRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0, 6, 0, 0),
        };
        pnlIncomeRemoveRow.Controls.Add(btnRemoveIncomeType);

        tlpIncomeTypes.Controls.Add(lbIncomeTypes, 0, 0);
        tlpIncomeTypes.Controls.Add(pnlIncomeAddRow, 0, 1);
        tlpIncomeTypes.Controls.Add(pnlIncomeRemoveRow, 0, 2);

        gbIncomeTypes.Controls.Add(tlpIncomeTypes);

        tlpExpenseTypes.Dock = DockStyle.Fill;
        tlpExpenseTypes.Padding = new Padding(12);
        tlpExpenseTypes.ColumnCount = 1;
        tlpExpenseTypes.RowCount = 3;
        tlpExpenseTypes.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpExpenseTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        tlpExpenseTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

        lbExpenseTypes.Dock = DockStyle.Fill;
        StyleListBox(lbExpenseTypes);

        var pnlExpenseAddRow = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(0),
            Margin = new Padding(0),
        };
        pnlExpenseAddRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        pnlExpenseAddRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));

        txtNewExpenseType.PlaceholderText = "New expense category...";
        txtNewExpenseType.BorderStyle = BorderStyle.FixedSingle;
        txtNewExpenseType.Dock = DockStyle.Fill;
        txtNewExpenseType.Margin = new Padding(0, 8, 8, 0);
        txtNewExpenseType.AutoSize = false;
        txtNewExpenseType.Height = 32;
        StyleTextBox(txtNewExpenseType);

        btnAddExpenseType.Text = "Add Category";
        btnAddExpenseType.Dock = DockStyle.Fill;
        btnAddExpenseType.Margin = new Padding(0, 6, 0, 0);
        btnAddExpenseType.Click += AddExpenseTypeClicked;
        StyleButton(btnAddExpenseType);

        pnlExpenseAddRow.Controls.Add(txtNewExpenseType, 0, 0);
        pnlExpenseAddRow.Controls.Add(btnAddExpenseType, 1, 0);

        btnRemoveExpenseType.Text = "Remove Selected";
        btnRemoveExpenseType.Width = 180;
        btnRemoveExpenseType.Click += RemoveExpenseTypeClicked;
        StyleButton(btnRemoveExpenseType);

        var pnlExpenseRemoveRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0, 6, 0, 0),
        };
        pnlExpenseRemoveRow.Controls.Add(btnRemoveExpenseType);

        tlpExpenseTypes.Controls.Add(lbExpenseTypes, 0, 0);
        tlpExpenseTypes.Controls.Add(pnlExpenseAddRow, 0, 1);
        tlpExpenseTypes.Controls.Add(pnlExpenseRemoveRow, 0, 2);

        gbExpenseTypes.Controls.Add(tlpExpenseTypes);

        gbSavingsSettings.Dock = DockStyle.Fill;
        gbSavingsSettings.Text = "Savings";
        StyleGroupBox(gbSavingsSettings);

        gbSavingsSettings.Dock = DockStyle.Fill;
        gbSavingsSettings.Text = "Savings";
        StyleGroupBox(gbSavingsSettings);

        tlpSavingsSettings.Dock = DockStyle.Fill;
        tlpSavingsSettings.Padding = new Padding(12);
        tlpSavingsSettings.ColumnCount = 3;
        tlpSavingsSettings.RowCount = 1;

        tlpSavingsSettings.ColumnStyles.Clear();
        tlpSavingsSettings.RowStyles.Clear();

        tlpSavingsSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120)); // label
        tlpSavingsSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120)); // numeric
        tlpSavingsSettings.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));      // button

        tlpSavingsSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

        lblSavingsPercent.Text = "Savings %:";
        lblSavingsPercent.TextAlign = ContentAlignment.MiddleLeft;
        lblSavingsPercent.Dock = DockStyle.Fill;

        // Numeric
        nudSavingsPercent.DecimalPlaces = 1;
        nudSavingsPercent.Minimum = 0;
        nudSavingsPercent.Maximum = 100;
        nudSavingsPercent.Width = 120;
        nudSavingsPercent.Height = 32;
        nudSavingsPercent.Dock = DockStyle.None;
        nudSavingsPercent.Anchor = AnchorStyles.Left;
        nudSavingsPercent.Margin = new Padding(0);
        StyleNumeric(nudSavingsPercent);

        // Button
        btnSavingsSave.Text = "Save";
        btnSavingsSave.Width = 120;
        btnSavingsSave.Dock = DockStyle.None;
        btnSavingsSave.Anchor = AnchorStyles.Left;
        btnSavingsSave.Margin = new Padding(8, 0, 0, 0);
        btnSavingsSave.Click += SavingsSaveClicked;
        StyleButton(btnSavingsSave);

        tlpSavingsSettings.Controls.Add(lblSavingsPercent, 0, 0);
        tlpSavingsSettings.Controls.Add(nudSavingsPercent, 1, 0);
        tlpSavingsSettings.Controls.Add(btnSavingsSave, 2, 0);

        gbSavingsSettings.Controls.Add(tlpSavingsSettings);

        // Actions row
        pnlSettingsActions.Dock = DockStyle.Fill;
        pnlSettingsActions.BackColor = AppConfig.ThemePanel;

        flpSettingsActions.Dock = DockStyle.Fill;
        flpSettingsActions.FlowDirection = FlowDirection.LeftToRight;
        flpSettingsActions.WrapContents = false;
        flpSettingsActions.Padding = new Padding(12, 14, 12, 12);

        pnlSettingsActions.Controls.Add(flpSettingsActions);

        // Layout adds
        tlpSettingsRoot.Controls.Add(gbIncomeTypes, 0, 0);
        tlpSettingsRoot.Controls.Add(gbExpenseTypes, 1, 0);

        tlpSettingsRoot.Controls.Add(gbSavingsSettings, 0, 1);
        tlpSettingsRoot.SetColumnSpan(gbSavingsSettings, 2);

        tlpSettingsRoot.Controls.Add(pnlSettingsActions, 0, 2);
        tlpSettingsRoot.SetColumnSpan(pnlSettingsActions, 2);

        tabSettings.Controls.Add(tlpSettingsRoot);
        tabSettings.Controls.Add(pnlSettingsHeader);

        #endregion

        #region Final Form Composition

        Controls.Add(tabMain);
        Controls.Add(menuMain);

        #endregion

        #region ResumeLayout

        ResumeLayout(false);
        PerformLayout();

        #endregion
    }

    #endregion
}
