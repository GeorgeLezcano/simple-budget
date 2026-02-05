﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using App.Constants;
using App.Utils;

namespace App;

partial class MainForm
{
    private IContainer components = null;

    // Theme (tuned dark theme - less "all black")
    private static readonly Color ThemeBack = Color.FromArgb(32, 32, 32);
    private static readonly Color ThemePanel = Color.FromArgb(45, 45, 48);
    private static readonly Color ThemeInput = Color.FromArgb(55, 55, 58);
    private static readonly Color ThemeText = Color.Gainsboro;
    private static readonly Color ThemeMuted = Color.FromArgb(170, 170, 170);
    private static readonly Color ThemeBorder = Color.FromArgb(80, 80, 80);

    // Accents
    private static readonly Color ThemeAccent = Color.FromArgb(33, 140, 79);      // green buttons
    private static readonly Color ThemeAccentHover = Color.FromArgb(40, 160, 92);
    private static readonly Color ThemeAccentDown = Color.FromArgb(28, 120, 68);
    private static readonly Color ThemeTabActive = Color.FromArgb(0, 122, 204);   // blue selected tab header
    private static readonly Color ThemeTabInactive = Color.FromArgb(55, 55, 58);

    // App chrome
    private MenuStrip menuMain;
    private ToolStripMenuItem menuFile;
    private ToolStripMenuItem menuFileExit;
    private ToolStripMenuItem menuView;
    private ToolStripMenuItem menuViewRefresh;
    private ToolStripMenuItem menuHelp;
    private ToolStripMenuItem menuHelpDocs;
    private ToolStripMenuItem menuHelpAbout;

    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusSpacer;
    private ToolStripStatusLabel statusVersion;

    private ToolTip toolTip;

    // Tabs (WinForms)
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

    // Settings (redesigned)
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

    private Panel pnlSettingsActions;
    private FlowLayoutPanel flpSettingsActions;
    private Button btnSettingsSave;
    private Button btnSettingsResetDefaults;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
        components = new Container();
        toolTip = new ToolTip(components);

        menuMain = new MenuStrip();
        menuFile = new ToolStripMenuItem();
        menuFileExit = new ToolStripMenuItem();
        menuView = new ToolStripMenuItem();
        menuViewRefresh = new ToolStripMenuItem();
        menuHelp = new ToolStripMenuItem();
        menuHelpDocs = new ToolStripMenuItem();
        menuHelpAbout = new ToolStripMenuItem();

        statusStrip = new StatusStrip();
        statusSpacer = new ToolStripStatusLabel();
        statusVersion = new ToolStripStatusLabel();

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

        pnlSettingsActions = new Panel();
        flpSettingsActions = new FlowLayoutPanel();
        btnSettingsSave = new Button();
        btnSettingsResetDefaults = new Button();

        SuspendLayout();

        // ======================
        // MainForm
        // ======================
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 800);
        MinimumSize = new Size(1100, 720);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Simple Budget";
        Icon = (Icon)resources.GetObject("$this.Icon");
        BackColor = ThemeBack;
        ForeColor = ThemeText;

        // ======================
        // MenuStrip
        // ======================
        menuMain.Dock = DockStyle.Top;
        menuMain.RenderMode = ToolStripRenderMode.System;

        menuFile.Text = "&File";
        menuFileExit.Text = "E&xit";
        menuFileExit.Click += FileExitClicked;
        menuFile.DropDownItems.Add(menuFileExit);

        menuView.Text = "&View";
        menuViewRefresh.Text = "&Refresh";
        menuViewRefresh.Click += ViewRefreshClicked;
        menuView.DropDownItems.Add(menuViewRefresh);

        menuHelp.Text = "&Help";
        menuHelpDocs.Text = "&Documentation";
        menuHelpDocs.Click += HelpDocsClicked;
        menuHelpAbout.Text = "&About";
        menuHelpAbout.Click += HelpAboutClicked;
        menuHelp.DropDownItems.Add(menuHelpDocs);
        menuHelp.DropDownItems.Add(new ToolStripSeparator());
        menuHelp.DropDownItems.Add(menuHelpAbout);

        menuMain.Items.AddRange(new ToolStripItem[] { menuFile, menuView, menuHelp });
        MainMenuStrip = menuMain;

        // ======================
        // StatusStrip
        // ======================
        statusStrip.Dock = DockStyle.Bottom;
        statusStrip.SizingGrip = false;

        statusSpacer.Spring = true;
        statusVersion.Text = LabelFormatter.AppShellText(AppConfig.ShellText);
        statusStrip.Items.AddRange(new ToolStripItem[] { statusSpacer, statusVersion });

        // ======================
        // Tabs
        // ======================
        tabMain.Dock = DockStyle.Fill;
        tabMain.DrawMode = TabDrawMode.OwnerDrawFixed;
        tabMain.SizeMode = TabSizeMode.Fixed;
        tabMain.ItemSize = new Size(140, 32);
        tabMain.Padding = new Point(16, 4);
        tabMain.DrawItem += TabMain_DrawItem;

        tabMain.Controls.Add(tabDashboard);
        tabMain.Controls.Add(tabIncome);
        tabMain.Controls.Add(tabExpenses);
        tabMain.Controls.Add(tabReports);
        tabMain.Controls.Add(tabSettings);

        // ======================
        // Dashboard Tab
        // ======================
        tabDashboard.Text = "Dashboard";
        tabDashboard.BackColor = ThemeBack;
        tabDashboard.ForeColor = ThemeText;
        tabDashboard.Padding = new Padding(12);
        tabDashboard.UseVisualStyleBackColor = false;

        tlpDashboard.Dock = DockStyle.Fill;
        tlpDashboard.RowCount = 3;
        tlpDashboard.ColumnCount = 1;
        tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));
        tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        pnlDashHeader.Dock = DockStyle.Fill;
        pnlDashHeader.BackColor = ThemePanel;

        lblDashTitle.AutoSize = true;
        lblDashTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblDashTitle.Text = "Simple Budget";
        lblDashTitle.Location = new Point(16, 12);

        lblDashSubtitle.AutoSize = true;
        lblDashSubtitle.ForeColor = ThemeMuted;
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

        toolTip.SetToolTip(cbDashRange,
            "View scope:\n" +
            "• Week = week containing the anchor date\n" +
            "• Month = month of the anchor date\n" +
            "• Year = calendar year of the anchor date\n\n" +
            "TODO: update dashboard calculations");

        lblDashMonth.AutoSize = true;
        lblDashMonth.Text = "Anchor date:";
        lblDashMonth.Location = new Point(780, 18);

        dtpDashMonth.Format = DateTimePickerFormat.Custom;
        dtpDashMonth.CustomFormat = "MMM dd, yyyy";
        dtpDashMonth.Width = 160;
        dtpDashMonth.Location = new Point(870, 14);
        dtpDashMonth.ValueChanged += SummaryMonthChanged;
        toolTip.SetToolTip(dtpDashMonth, "Pick a date to anchor Week/Month/Year views.");

        pnlDashHeader.Controls.Add(lblDashTitle);
        pnlDashHeader.Controls.Add(lblDashSubtitle);
        pnlDashHeader.Controls.Add(lblDashRange);
        pnlDashHeader.Controls.Add(cbDashRange);
        pnlDashHeader.Controls.Add(lblDashMonth);
        pnlDashHeader.Controls.Add(dtpDashMonth);

        gbDashTotals.Dock = DockStyle.Fill;
        gbDashTotals.Text = "Summary";
        gbDashTotals.BackColor = ThemePanel;
        gbDashTotals.ForeColor = ThemeText;

        tlpDashTotals.Dock = DockStyle.Fill;
        tlpDashTotals.Padding = new Padding(12);
        tlpDashTotals.ColumnCount = 2;
        tlpDashTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
        tlpDashTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        tlpDashTotals.RowCount = 4;
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        tlpDashTotals.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

        lblIncomeTotalTitle.AutoSize = true;
        lblIncomeTotalTitle.Text = "Total Income";
        lblIncomeTotalValue.AutoSize = true;
        lblIncomeTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblIncomeTotalValue.Text = "$0.00";

        lblExpenseTotalTitle.AutoSize = true;
        lblExpenseTotalTitle.Text = "Total Expenses";
        lblExpenseTotalValue.AutoSize = true;
        lblExpenseTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblExpenseTotalValue.Text = "$0.00";

        lblSavingsTotalTitle.AutoSize = true;
        lblSavingsTotalTitle.Text = "Savings";
        lblSavingsTotalValue.AutoSize = true;
        lblSavingsTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblSavingsTotalValue.Text = "$0.00";

        lblNetTotalTitle.AutoSize = true;
        lblNetTotalTitle.Text = "Net";
        lblNetTotalValue.AutoSize = true;
        lblNetTotalValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblNetTotalValue.Text = "$0.00";

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
        gbDashNextSteps.BackColor = ThemePanel;
        gbDashNextSteps.ForeColor = ThemeText;

        lblDashHint.Dock = DockStyle.Fill;
        lblDashHint.ForeColor = ThemeMuted;
        lblDashHint.Text =
            "• Use Income to add money coming in.\n" +
            "• Use Expenses to log spending (recurring or one-time).\n" +
            "• Use Reports to filter/export.\n" +
            "• Use Settings to manage categories (Rent, Groceries, etc.).";
        lblDashHint.Padding = new Padding(12);

        gbDashNextSteps.Controls.Add(lblDashHint);

        tlpDashboard.Controls.Add(pnlDashHeader, 0, 0);
        tlpDashboard.Controls.Add(gbDashTotals, 0, 1);
        tlpDashboard.Controls.Add(gbDashNextSteps, 0, 2);

        tabDashboard.Controls.Add(tlpDashboard);

        // ======================
        // Income Tab
        // ======================
        tabIncome.Text = "Income";
        tabIncome.BackColor = ThemeBack;
        tabIncome.ForeColor = ThemeText;
        tabIncome.Padding = new Padding(12);
        tabIncome.UseVisualStyleBackColor = false;

        scIncome.Dock = DockStyle.Fill;
        scIncome.Orientation = Orientation.Horizontal;
        scIncome.SplitterDistance = 210; // ✅ more space for grid

        gbIncomeEntry.Dock = DockStyle.Fill;
        gbIncomeEntry.Text = "Add Income";
        gbIncomeEntry.BackColor = ThemePanel;
        gbIncomeEntry.ForeColor = ThemeText;

        tlpIncomeEntry.Dock = DockStyle.Fill;
        tlpIncomeEntry.Padding = new Padding(12);
        tlpIncomeEntry.ColumnCount = 2;
        tlpIncomeEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        tlpIncomeEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpIncomeEntry.RowCount = 5;
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // category
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // amount
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // date
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 90)); // ✅ notes smaller
        tlpIncomeEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); // buttons

        lblIncomeCategory.Text = "Category:";
        lblIncomeCategory.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeCategory.Dock = DockStyle.Fill;

        cbIncomeCategory.Dock = DockStyle.Fill;
        cbIncomeCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cbIncomeCategory.Items.AddRange(new object[] { "Salary", "Bonus", "Other" });

        lblIncomeAmount.Text = "Amount:";
        lblIncomeAmount.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeAmount.Dock = DockStyle.Fill;

        nudIncomeAmount.Dock = DockStyle.Fill;
        nudIncomeAmount.DecimalPlaces = 2;
        nudIncomeAmount.Maximum = 100000000;

        lblIncomeDate.Text = "Date:";
        lblIncomeDate.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeDate.Dock = DockStyle.Fill;

        dtpIncomeDate.Dock = DockStyle.Fill;

        lblIncomeNotes.Text = "Notes:";
        lblIncomeNotes.TextAlign = ContentAlignment.MiddleLeft;
        lblIncomeNotes.Dock = DockStyle.Fill;

        txtIncomeNotes.Dock = DockStyle.Fill;
        txtIncomeNotes.BorderStyle = BorderStyle.FixedSingle;
        txtIncomeNotes.Multiline = true;

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

        // ✅ explicit button styling (beats overrides)
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

        tlpIncomeEntry.Controls.Add(lblIncomeNotes, 0, 3);
        tlpIncomeEntry.Controls.Add(txtIncomeNotes, 1, 3);

        tlpIncomeEntry.Controls.Add(flpIncomeButtons, 0, 4);
        tlpIncomeEntry.SetColumnSpan(flpIncomeButtons, 2);

        gbIncomeEntry.Controls.Add(tlpIncomeEntry);

        gbIncomeList.Dock = DockStyle.Fill;
        gbIncomeList.Text = "Income List";
        gbIncomeList.BackColor = ThemePanel;
        gbIncomeList.ForeColor = ThemeText;

        dgvIncome.Dock = DockStyle.Fill;
        dgvIncome.AllowUserToAddRows = false;
        dgvIncome.AllowUserToDeleteRows = false;
        dgvIncome.ReadOnly = true;
        dgvIncome.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        gbIncomeList.Controls.Add(dgvIncome);

        scIncome.Panel1.Controls.Add(gbIncomeEntry);
        scIncome.Panel2.Controls.Add(gbIncomeList);

        tabIncome.Controls.Add(scIncome);

        // ======================
        // Expenses Tab
        // ======================
        tabExpenses.Text = "Expenses";
        tabExpenses.BackColor = ThemeBack;
        tabExpenses.ForeColor = ThemeText;
        tabExpenses.Padding = new Padding(12);
        tabExpenses.UseVisualStyleBackColor = false;

        scExpenses.Dock = DockStyle.Fill;
        scExpenses.Orientation = Orientation.Horizontal;
        scExpenses.SplitterDistance = 240; // ✅ more space for grid

        gbExpenseEntry.Dock = DockStyle.Fill;
        gbExpenseEntry.Text = "Add Expense";
        gbExpenseEntry.BackColor = ThemePanel;
        gbExpenseEntry.ForeColor = ThemeText;

        tlpExpenseEntry.Dock = DockStyle.Fill;
        tlpExpenseEntry.Padding = new Padding(12);
        tlpExpenseEntry.ColumnCount = 2;
        tlpExpenseEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        tlpExpenseEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpExpenseEntry.RowCount = 6;
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // category
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // amount
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // date
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // recurring
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 90)); // ✅ notes smaller
        tlpExpenseEntry.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); // buttons

        lblExpenseCategory.Text = "Category:";
        lblExpenseCategory.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseCategory.Dock = DockStyle.Fill;

        cbExpenseCategory.Dock = DockStyle.Fill;
        cbExpenseCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cbExpenseCategory.Items.AddRange(new object[] { "Rent", "Groceries", "Utilities", "Other" });

        lblExpenseAmount.Text = "Amount:";
        lblExpenseAmount.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseAmount.Dock = DockStyle.Fill;

        nudExpenseAmount.Dock = DockStyle.Fill;
        nudExpenseAmount.DecimalPlaces = 2;
        nudExpenseAmount.Maximum = 100000000;

        lblExpenseDate.Text = "Date:";
        lblExpenseDate.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseDate.Dock = DockStyle.Fill;

        dtpExpenseDate.Dock = DockStyle.Fill;

        chkExpenseRecurring.Text = "Recurring";
        chkExpenseRecurring.AutoSize = true;
        chkExpenseRecurring.CheckedChanged += ExpenseRecurringChanged;

        lblExpenseFrequency.Text = "Frequency:";
        lblExpenseFrequency.AutoSize = true;

        cbExpenseFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
        cbExpenseFrequency.Items.AddRange(new object[] { "Weekly", "Bi-Weekly", "Monthly", "Quarterly", "Yearly" });
        cbExpenseFrequency.Enabled = false;
        cbExpenseFrequency.Width = 150;

        var pnlRecurring = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Padding = new Padding(0, 6, 0, 0),
            AutoScroll = false
        };
        pnlRecurring.Controls.Add(chkExpenseRecurring);
        pnlRecurring.Controls.Add(new Label { Width = 12, Text = "" });
        pnlRecurring.Controls.Add(lblExpenseFrequency);
        pnlRecurring.Controls.Add(cbExpenseFrequency);

        lblExpenseNotes.Text = "Notes:";
        lblExpenseNotes.TextAlign = ContentAlignment.MiddleLeft;
        lblExpenseNotes.Dock = DockStyle.Fill;

        txtExpenseNotes.Dock = DockStyle.Fill;
        txtExpenseNotes.BorderStyle = BorderStyle.FixedSingle;
        txtExpenseNotes.Multiline = true;

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

        // ✅ explicit button styling (beats overrides)
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
        gbExpenseList.BackColor = ThemePanel;
        gbExpenseList.ForeColor = ThemeText;

        dgvExpenses.Dock = DockStyle.Fill;
        dgvExpenses.AllowUserToAddRows = false;
        dgvExpenses.AllowUserToDeleteRows = false;
        dgvExpenses.ReadOnly = true;
        dgvExpenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        gbExpenseList.Controls.Add(dgvExpenses);

        scExpenses.Panel1.Controls.Add(gbExpenseEntry);
        scExpenses.Panel2.Controls.Add(gbExpenseList);

        tabExpenses.Controls.Add(scExpenses);

        // ======================
        // Reports Tab
        // ======================
        tabReports.Text = "Reports";
        tabReports.BackColor = ThemeBack;
        tabReports.ForeColor = ThemeText;
        tabReports.Padding = new Padding(12);
        tabReports.UseVisualStyleBackColor = false;

        scReports.Dock = DockStyle.Fill;
        scReports.Orientation = Orientation.Vertical;
        scReports.SplitterDistance = 380;

        gbReportFilters.Dock = DockStyle.Fill;
        gbReportFilters.Text = "Filters";
        gbReportFilters.BackColor = ThemePanel;
        gbReportFilters.ForeColor = ThemeText;

        tlpReportFilters.Dock = DockStyle.Fill;
        tlpReportFilters.Padding = new Padding(12);
        tlpReportFilters.ColumnCount = 2;
        tlpReportFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
        tlpReportFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpReportFilters.RowCount = 8;

        tlpReportFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 34)); // hint
        for (int i = 0; i < 6; i++)
            tlpReportFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        tlpReportFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        lblReportHint.Dock = DockStyle.Fill;
        lblReportHint.ForeColor = ThemeMuted;
        lblReportHint.Text = "Filters stack. Leave blank to ignore.";
        lblReportHint.TextAlign = ContentAlignment.MiddleLeft;

        lblReportFrom.Text = "From:";
        lblReportFrom.TextAlign = ContentAlignment.MiddleLeft;
        lblReportFrom.Dock = DockStyle.Fill;
        dtpReportFrom.Dock = DockStyle.Fill;

        lblReportTo.Text = "To:";
        lblReportTo.TextAlign = ContentAlignment.MiddleLeft;
        lblReportTo.Dock = DockStyle.Fill;
        dtpReportTo.Dock = DockStyle.Fill;

        lblReportMin.Text = "Amount Min:";
        lblReportMin.TextAlign = ContentAlignment.MiddleLeft;
        lblReportMin.Dock = DockStyle.Fill;
        nudReportMin.Dock = DockStyle.Fill;
        nudReportMin.DecimalPlaces = 2;
        nudReportMin.Maximum = 100000000;

        lblReportMax.Text = "Amount Max:";
        lblReportMax.TextAlign = ContentAlignment.MiddleLeft;
        lblReportMax.Dock = DockStyle.Fill;
        nudReportMax.Dock = DockStyle.Fill;
        nudReportMax.DecimalPlaces = 2;
        nudReportMax.Maximum = 100000000;

        lblReportSearch.Text = "Search:";
        lblReportSearch.TextAlign = ContentAlignment.MiddleLeft;
        lblReportSearch.Dock = DockStyle.Fill;

        txtReportSearch.Dock = DockStyle.Fill;
        txtReportSearch.BorderStyle = BorderStyle.FixedSingle;

        lblReportScope.Text = "Scope:";
        lblReportScope.TextAlign = ContentAlignment.MiddleLeft;
        lblReportScope.Dock = DockStyle.Fill;

        cbReportScope.Dock = DockStyle.Fill;
        cbReportScope.DropDownStyle = ComboBoxStyle.DropDownList;
        cbReportScope.Items.AddRange(new object[] { "All", "Income Only", "Expenses Only" });
        cbReportScope.SelectedIndex = 0;

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

        // ✅ explicit style
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
        gbReportResults.BackColor = ThemePanel;
        gbReportResults.ForeColor = ThemeText;

        dgvReportResults.Dock = DockStyle.Fill;
        dgvReportResults.AllowUserToAddRows = false;
        dgvReportResults.AllowUserToDeleteRows = false;
        dgvReportResults.ReadOnly = true;
        dgvReportResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        gbReportResults.Controls.Add(dgvReportResults);

        scReports.Panel1.Controls.Add(gbReportFilters);
        scReports.Panel2.Controls.Add(gbReportResults);

        tabReports.Controls.Add(scReports);

        // ======================
        // Settings Tab (same redesign)
        // ======================
        tabSettings.Text = "Settings";
        tabSettings.BackColor = ThemeBack;
        tabSettings.ForeColor = ThemeText;
        tabSettings.Padding = new Padding(12);
        tabSettings.UseVisualStyleBackColor = false;

        pnlSettingsHeader.Dock = DockStyle.Top;
        pnlSettingsHeader.Height = 74;
        pnlSettingsHeader.BackColor = ThemePanel;

        lblSettingsTitle.AutoSize = true;
        lblSettingsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblSettingsTitle.Text = "Settings";
        lblSettingsTitle.Location = new Point(16, 12);

        lblSettingsHint.AutoSize = true;
        lblSettingsHint.ForeColor = ThemeMuted;
        lblSettingsHint.Text = "Add categories here. They will show up in Income/Expenses dropdowns.";
        lblSettingsHint.Location = new Point(18, 44);

        pnlSettingsHeader.Controls.Add(lblSettingsTitle);
        pnlSettingsHeader.Controls.Add(lblSettingsHint);

        tlpSettingsRoot.Dock = DockStyle.Fill;
        tlpSettingsRoot.ColumnCount = 2;
        tlpSettingsRoot.RowCount = 2;
        tlpSettingsRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tlpSettingsRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tlpSettingsRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        tlpSettingsRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
        tlpSettingsRoot.Padding = new Padding(0, 10, 0, 0);

        gbIncomeTypes.Dock = DockStyle.Fill;
        gbIncomeTypes.Text = "Income Categories";
        gbIncomeTypes.BackColor = ThemePanel;
        gbIncomeTypes.ForeColor = ThemeText;

        gbExpenseTypes.Dock = DockStyle.Fill;
        gbExpenseTypes.Text = "Expense Categories";
        gbExpenseTypes.BackColor = ThemePanel;
        gbExpenseTypes.ForeColor = ThemeText;

        // Income types layout
        tlpIncomeTypes.Dock = DockStyle.Fill;
        tlpIncomeTypes.Padding = new Padding(12);
        tlpIncomeTypes.ColumnCount = 1;
        tlpIncomeTypes.RowCount = 3;
        tlpIncomeTypes.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // list
        tlpIncomeTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); // input + add
        tlpIncomeTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); // remove

        lbIncomeTypes.Dock = DockStyle.Fill;
        lbIncomeTypes.Items.AddRange(new object[] { "Salary", "Bonus", "Other" });
        lbIncomeTypes.BackColor = ThemeInput;
        lbIncomeTypes.ForeColor = ThemeText;

        var pnlIncomeAddRow = new Panel { Dock = DockStyle.Fill };
        txtNewIncomeType.PlaceholderText = "New income category...";
        txtNewIncomeType.BorderStyle = BorderStyle.FixedSingle;
        txtNewIncomeType.Width = 260;
        txtNewIncomeType.Location = new Point(0, 10);

        btnAddIncomeType.Text = "Add Category";
        btnAddIncomeType.Width = 150;
        btnAddIncomeType.Location = new Point(270, 8);
        btnAddIncomeType.Click += AddIncomeTypeClicked;
        StyleButton(btnAddIncomeType);

        pnlIncomeAddRow.Controls.Add(txtNewIncomeType);
        pnlIncomeAddRow.Controls.Add(btnAddIncomeType);

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

        // Expense types layout
        tlpExpenseTypes.Dock = DockStyle.Fill;
        tlpExpenseTypes.Padding = new Padding(12);
        tlpExpenseTypes.ColumnCount = 1;
        tlpExpenseTypes.RowCount = 3;
        tlpExpenseTypes.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // list
        tlpExpenseTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); // input + add
        tlpExpenseTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46)); // remove

        lbExpenseTypes.Dock = DockStyle.Fill;
        lbExpenseTypes.Items.AddRange(new object[] { "Rent", "Groceries", "Utilities", "Other" });
        lbExpenseTypes.BackColor = ThemeInput;
        lbExpenseTypes.ForeColor = ThemeText;

        var pnlExpenseAddRow = new Panel { Dock = DockStyle.Fill };
        txtNewExpenseType.PlaceholderText = "New expense category...";
        txtNewExpenseType.BorderStyle = BorderStyle.FixedSingle;
        txtNewExpenseType.Width = 260;
        txtNewExpenseType.Location = new Point(0, 10);

        btnAddExpenseType.Text = "Add Category";
        btnAddExpenseType.Width = 150;
        btnAddExpenseType.Location = new Point(270, 8);
        btnAddExpenseType.Click += AddExpenseTypeClicked;
        StyleButton(btnAddExpenseType);

        pnlExpenseAddRow.Controls.Add(txtNewExpenseType);
        pnlExpenseAddRow.Controls.Add(btnAddExpenseType);

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

        // Actions row
        pnlSettingsActions.Dock = DockStyle.Fill;
        pnlSettingsActions.BackColor = ThemePanel;

        flpSettingsActions.Dock = DockStyle.Fill;
        flpSettingsActions.FlowDirection = FlowDirection.LeftToRight;
        flpSettingsActions.WrapContents = false;
        flpSettingsActions.Padding = new Padding(12, 14, 12, 12);

        btnSettingsSave.Text = "Save";
        btnSettingsSave.Width = 140;
        btnSettingsSave.Click += SettingsSaveClicked;
        StyleButton(btnSettingsSave);

        btnSettingsResetDefaults.Text = "Reset to Defaults";
        btnSettingsResetDefaults.Width = 180;
        btnSettingsResetDefaults.Click += SettingsResetDefaultsClicked;
        StyleButton(btnSettingsResetDefaults);

        flpSettingsActions.Controls.Add(btnSettingsSave);
        flpSettingsActions.Controls.Add(btnSettingsResetDefaults);
        pnlSettingsActions.Controls.Add(flpSettingsActions);

        tlpSettingsRoot.Controls.Add(gbIncomeTypes, 0, 0);
        tlpSettingsRoot.Controls.Add(gbExpenseTypes, 1, 0);
        tlpSettingsRoot.Controls.Add(pnlSettingsActions, 0, 1);
        tlpSettingsRoot.SetColumnSpan(pnlSettingsActions, 2);

        tabSettings.Controls.Add(tlpSettingsRoot);
        tabSettings.Controls.Add(pnlSettingsHeader);

        // ======================
        // Theme pass (inputs + grids)
        // ======================
        ApplyDarkThemeToControls(this);

        // ======================
        // Final form composition
        // ======================
        Controls.Add(tabMain);
        Controls.Add(menuMain);
        Controls.Add(statusStrip);

        ResumeLayout(false);
        PerformLayout();
    }

    // Force consistent button styling (fixes "black buttons" from visual style overrides)
    private void StyleButton(Button btn)
    {
        btn.UseVisualStyleBackColor = false;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderColor = ThemeBorder;
        btn.FlatAppearance.BorderSize = 1;

        btn.BackColor = ThemeAccent;
        btn.ForeColor = Color.White;

        btn.FlatAppearance.MouseOverBackColor = ThemeAccentHover;
        btn.FlatAppearance.MouseDownBackColor = ThemeAccentDown;

        btn.AutoSize = false;
        btn.Height = 36;
        btn.Padding = new Padding(14, 0, 14, 0);
        btn.TextAlign = ContentAlignment.MiddleCenter;
    }

    private void ApplyDarkThemeToControls(Control root)
    {
        foreach (Control c in root.Controls)
        {
            if (c is TabPage)
                c.BackColor = ThemeBack;

            c.ForeColor = ThemeText;

            if (c is GroupBox gb)
            {
                gb.BackColor = ThemePanel;
                gb.ForeColor = ThemeText;
            }

            if (c is TextBox tb)
            {
                tb.BackColor = ThemeInput;
                tb.ForeColor = ThemeText;
            }

            if (c is ComboBox cb)
            {
                cb.BackColor = ThemeInput;
                cb.ForeColor = ThemeText;
                cb.FlatStyle = FlatStyle.Flat;
            }

            if (c is ListBox lb)
            {
                lb.BackColor = ThemeInput;
                lb.ForeColor = ThemeText;
            }

            if (c is NumericUpDown nud)
            {
                nud.BackColor = ThemeInput;
                nud.ForeColor = ThemeText;
            }

            if (c is DateTimePicker dtp)
            {
                dtp.CalendarMonthBackground = ThemeInput;
                dtp.CalendarForeColor = ThemeText;
            }

            if (c is Button btn)
            {
                // Ensure theme pass doesn't revert anything.
                StyleButton(btn);
            }

            if (c is CheckBox chk)
                chk.ForeColor = ThemeText;

            if (c is DataGridView grid)
                ApplyDataGridTheme(grid);

            if (c.HasChildren)
                ApplyDarkThemeToControls(c);
        }
    }

    private void ApplyDataGridTheme(DataGridView grid)
    {
        grid.BackgroundColor = ThemePanel;
        grid.GridColor = ThemeBorder;
        grid.BorderStyle = BorderStyle.None;

        grid.DefaultCellStyle.BackColor = ThemeBack;
        grid.DefaultCellStyle.ForeColor = ThemeText;
        grid.DefaultCellStyle.SelectionBackColor = ThemeTabActive;
        grid.DefaultCellStyle.SelectionForeColor = Color.White;

        grid.ColumnHeadersDefaultCellStyle.BackColor = ThemePanel;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeText;
        grid.EnableHeadersVisualStyles = false;
    }
}
