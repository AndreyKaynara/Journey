namespace JourneyGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ParametersGV = new System.Windows.Forms.DataGridView();
            this.gvclParName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvclParValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProblemGV = new System.Windows.Forms.DataGridView();
            this.gvclKeyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvclValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mmiClearLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiTSP = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiShowDemands = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiShowDepots = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiShowDisplayCoords = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiShowCostMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiTour = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiTourLength = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiNodesCount = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiShowNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiShowTour = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mmiSaveTour = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiLoadTour = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiSaveTourAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiLoadTourAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiSolution = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiRandom = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiNearestNeigbor = new System.Windows.Forms.ToolStripMenuItem();
            this.mmi2Opt = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiLinKernighan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mmiShowBestDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mmiDrawCurrentTour = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mmiTesting = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenTourDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveTourDialog = new System.Windows.Forms.SaveFileDialog();
            this.MapControl = new JourneyGUI.MapControl();
            ((System.ComponentModel.ISupportInitialize)(this.ParametersGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProblemGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParametersGV
            // 
            this.ParametersGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParametersGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gvclParName,
            this.gvclParValue});
            this.ParametersGV.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParametersGV.Location = new System.Drawing.Point(0, 0);
            this.ParametersGV.Name = "ParametersGV";
            this.ParametersGV.Size = new System.Drawing.Size(243, 327);
            this.ParametersGV.TabIndex = 6;
            // 
            // gvclParName
            // 
            this.gvclParName.HeaderText = "Название";
            this.gvclParName.Name = "gvclParName";
            this.gvclParName.ReadOnly = true;
            // 
            // gvclParValue
            // 
            this.gvclParValue.HeaderText = "Значение";
            this.gvclParValue.Name = "gvclParValue";
            this.gvclParValue.ReadOnly = true;
            // 
            // ProblemGV
            // 
            this.ProblemGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProblemGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gvclKeyword,
            this.gvclValue});
            this.ProblemGV.Dock = System.Windows.Forms.DockStyle.Right;
            this.ProblemGV.Location = new System.Drawing.Point(866, 0);
            this.ProblemGV.Name = "ProblemGV";
            this.ProblemGV.Size = new System.Drawing.Size(243, 327);
            this.ProblemGV.TabIndex = 7;
            // 
            // gvclKeyword
            // 
            this.gvclKeyword.HeaderText = "Параметр";
            this.gvclKeyword.Name = "gvclKeyword";
            this.gvclKeyword.ReadOnly = true;
            // 
            // gvclValue
            // 
            this.gvclValue.HeaderText = "Значение";
            this.gvclValue.Name = "gvclValue";
            this.gvclValue.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.splitter2);
            this.splitContainer1.Panel1.Controls.Add(this.splitter1);
            this.splitContainer1.Panel1.Controls.Add(this.ParametersGV);
            this.splitContainer1.Panel1.Controls.Add(this.ProblemGV);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbLogs);
            this.splitContainer1.Panel2.Controls.Add(this.StatusBar);
            this.splitContainer1.Size = new System.Drawing.Size(1109, 519);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MapControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(246, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 327);
            this.panel1.TabIndex = 10;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(863, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 327);
            this.splitter2.TabIndex = 9;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(243, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 327);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // tbLogs
            // 
            this.tbLogs.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Font = new System.Drawing.Font("Verdana", 11F);
            this.tbLogs.Location = new System.Drawing.Point(0, 0);
            this.tbLogs.MaxLength = 500000;
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogs.Size = new System.Drawing.Size(1109, 166);
            this.tbLogs.TabIndex = 0;
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 166);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1109, 22);
            this.StatusBar.TabIndex = 1;
            this.StatusBar.Text = "StatusBar";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.mmiTSP,
            this.mmiTour,
            this.mmiSolution,
            this.mmiHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1109, 24);
            this.MainMenu.TabIndex = 10;
            this.MainMenu.Text = "MainMenu";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmiOpen,
            this.mmiSave,
            this.mmiSaveAs,
            this.mmiClose,
            this.toolStripSeparator1,
            this.mmiClearLogs,
            this.toolStripMenuItem4,
            this.mmiExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(48, 20);
            this.miFile.Text = "Файл";
            // 
            // mmiOpen
            // 
            this.mmiOpen.Name = "mmiOpen";
            this.mmiOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mmiOpen.Size = new System.Drawing.Size(226, 22);
            this.mmiOpen.Text = "Открыть...";
            this.mmiOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // mmiSave
            // 
            this.mmiSave.Name = "mmiSave";
            this.mmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mmiSave.Size = new System.Drawing.Size(226, 22);
            this.mmiSave.Text = "Сохранить";
            this.mmiSave.Click += new System.EventHandler(this.mmiSave_Click);
            // 
            // mmiSaveAs
            // 
            this.mmiSaveAs.Name = "mmiSaveAs";
            this.mmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.mmiSaveAs.Size = new System.Drawing.Size(226, 22);
            this.mmiSaveAs.Text = "Сохранить как...";
            this.mmiSaveAs.Click += new System.EventHandler(this.mmiSaveAs_Click);
            // 
            // mmiClose
            // 
            this.mmiClose.Name = "mmiClose";
            this.mmiClose.Size = new System.Drawing.Size(226, 22);
            this.mmiClose.Text = "Закрыть";
            this.mmiClose.Click += new System.EventHandler(this.mmiClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // mmiClearLogs
            // 
            this.mmiClearLogs.Name = "mmiClearLogs";
            this.mmiClearLogs.ShortcutKeyDisplayString = "";
            this.mmiClearLogs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.mmiClearLogs.Size = new System.Drawing.Size(226, 22);
            this.mmiClearLogs.Text = "Очистить консоль";
            this.mmiClearLogs.Click += new System.EventHandler(this.mmiClearLogs_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(223, 6);
            // 
            // mmiExit
            // 
            this.mmiExit.Name = "mmiExit";
            this.mmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mmiExit.Size = new System.Drawing.Size(226, 22);
            this.mmiExit.Text = "Выход";
            this.mmiExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // mmiTSP
            // 
            this.mmiTSP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmiShowDemands,
            this.mmiShowDepots,
            this.mmiShowDisplayCoords,
            this.mmiShowCostMatrix});
            this.mmiTSP.Name = "mmiTSP";
            this.mmiTSP.Size = new System.Drawing.Size(39, 20);
            this.mmiTSP.Text = "TSP";
            // 
            // mmiShowDemands
            // 
            this.mmiShowDemands.Name = "mmiShowDemands";
            this.mmiShowDemands.Size = new System.Drawing.Size(168, 22);
            this.mmiShowDemands.Text = "DEMANDS";
            this.mmiShowDemands.Click += new System.EventHandler(this.mmiShowDemands_Click);
            // 
            // mmiShowDepots
            // 
            this.mmiShowDepots.Name = "mmiShowDepots";
            this.mmiShowDepots.Size = new System.Drawing.Size(168, 22);
            this.mmiShowDepots.Text = "DEPOT";
            this.mmiShowDepots.Click += new System.EventHandler(this.mmiShowDepots_Click);
            // 
            // mmiShowDisplayCoords
            // 
            this.mmiShowDisplayCoords.Name = "mmiShowDisplayCoords";
            this.mmiShowDisplayCoords.Size = new System.Drawing.Size(168, 22);
            this.mmiShowDisplayCoords.Text = "DISPLAY COORDS";
            this.mmiShowDisplayCoords.Click += new System.EventHandler(this.mmiShowDisplayCoords_Click);
            // 
            // mmiShowCostMatrix
            // 
            this.mmiShowCostMatrix.Name = "mmiShowCostMatrix";
            this.mmiShowCostMatrix.Size = new System.Drawing.Size(168, 22);
            this.mmiShowCostMatrix.Text = "COST MATRIX";
            this.mmiShowCostMatrix.Click += new System.EventHandler(this.mmiShowCostMatrix_Click);
            // 
            // mmiTour
            // 
            this.mmiTour.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmiTourLength,
            this.mmiNodesCount,
            this.mmiShowNodes,
            this.mmiShowTour,
            this.toolStripMenuItem2,
            this.mmiSaveTour,
            this.mmiLoadTour,
            this.mmiSaveTourAs,
            this.mmiLoadTourAs});
            this.mmiTour.Name = "mmiTour";
            this.mmiTour.Size = new System.Drawing.Size(39, 20);
            this.mmiTour.Text = "Тур";
            // 
            // mmiTourLength
            // 
            this.mmiTourLength.Name = "mmiTourLength";
            this.mmiTourLength.Size = new System.Drawing.Size(226, 22);
            this.mmiTourLength.Text = "Длина";
            this.mmiTourLength.Click += new System.EventHandler(this.mmiTourLength_Click);
            // 
            // mmiNodesCount
            // 
            this.mmiNodesCount.Name = "mmiNodesCount";
            this.mmiNodesCount.Size = new System.Drawing.Size(226, 22);
            this.mmiNodesCount.Text = "Количество узлов";
            this.mmiNodesCount.Click += new System.EventHandler(this.mmiNodesCount_Click);
            // 
            // mmiShowNodes
            // 
            this.mmiShowNodes.Name = "mmiShowNodes";
            this.mmiShowNodes.Size = new System.Drawing.Size(226, 22);
            this.mmiShowNodes.Text = "Вывести узлы";
            this.mmiShowNodes.Click += new System.EventHandler(this.mmiShowNodes_Click);
            // 
            // mmiShowTour
            // 
            this.mmiShowTour.Name = "mmiShowTour";
            this.mmiShowTour.Size = new System.Drawing.Size(226, 22);
            this.mmiShowTour.Text = "Вывести тур";
            this.mmiShowTour.Click += new System.EventHandler(this.mmiShowTour_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(223, 6);
            // 
            // mmiSaveTour
            // 
            this.mmiSaveTour.Name = "mmiSaveTour";
            this.mmiSaveTour.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.mmiSaveTour.Size = new System.Drawing.Size(226, 22);
            this.mmiSaveTour.Text = "Сохранить тур";
            this.mmiSaveTour.Click += new System.EventHandler(this.mmiSaveTour_Click);
            // 
            // mmiLoadTour
            // 
            this.mmiLoadTour.Name = "mmiLoadTour";
            this.mmiLoadTour.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mmiLoadTour.Size = new System.Drawing.Size(226, 22);
            this.mmiLoadTour.Text = "Загрузить тур";
            this.mmiLoadTour.Click += new System.EventHandler(this.mmiLoadTour_Click);
            // 
            // mmiSaveTourAs
            // 
            this.mmiSaveTourAs.Name = "mmiSaveTourAs";
            this.mmiSaveTourAs.Size = new System.Drawing.Size(226, 22);
            this.mmiSaveTourAs.Text = "Сохранить тур как...";
            this.mmiSaveTourAs.Click += new System.EventHandler(this.mmiSaveTourAs_Click);
            // 
            // mmiLoadTourAs
            // 
            this.mmiLoadTourAs.Name = "mmiLoadTourAs";
            this.mmiLoadTourAs.Size = new System.Drawing.Size(226, 22);
            this.mmiLoadTourAs.Text = "Загрузить тур...";
            this.mmiLoadTourAs.Click += new System.EventHandler(this.mmiLoadTourAs_Click);
            // 
            // mmiSolution
            // 
            this.mmiSolution.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmiRandom,
            this.mmiNearestNeigbor,
            this.mmi2Opt,
            this.mmiLinKernighan,
            this.toolStripMenuItem1,
            this.mmiShowBestDistance,
            this.toolStripMenuItem3,
            this.mmiDrawCurrentTour,
            this.toolStripMenuItem5,
            this.mmiTesting});
            this.mmiSolution.Name = "mmiSolution";
            this.mmiSolution.Size = new System.Drawing.Size(69, 20);
            this.mmiSolution.Text = "Решение";
            // 
            // mmiRandom
            // 
            this.mmiRandom.Name = "mmiRandom";
            this.mmiRandom.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.mmiRandom.Size = new System.Drawing.Size(264, 22);
            this.mmiRandom.Text = "Random";
            this.mmiRandom.Click += new System.EventHandler(this.mmiRandom_Click);
            // 
            // mmiNearestNeigbor
            // 
            this.mmiNearestNeigbor.Name = "mmiNearestNeigbor";
            this.mmiNearestNeigbor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.mmiNearestNeigbor.Size = new System.Drawing.Size(264, 22);
            this.mmiNearestNeigbor.Text = "Nearest Neigbor";
            this.mmiNearestNeigbor.Click += new System.EventHandler(this.mmiNearestNeigbor_Click);
            // 
            // mmi2Opt
            // 
            this.mmi2Opt.Name = "mmi2Opt";
            this.mmi2Opt.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.mmi2Opt.Size = new System.Drawing.Size(264, 22);
            this.mmi2Opt.Text = "2-Opt";
            this.mmi2Opt.Click += new System.EventHandler(this.mmi2Opt_Click);
            // 
            // mmiLinKernighan
            // 
            this.mmiLinKernighan.Name = "mmiLinKernighan";
            this.mmiLinKernighan.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.mmiLinKernighan.Size = new System.Drawing.Size(264, 22);
            this.mmiLinKernighan.Text = "Lin Kernighan";
            this.mmiLinKernighan.Click += new System.EventHandler(this.mmiLinKernighan_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(261, 6);
            // 
            // mmiShowBestDistance
            // 
            this.mmiShowBestDistance.Name = "mmiShowBestDistance";
            this.mmiShowBestDistance.Size = new System.Drawing.Size(264, 22);
            this.mmiShowBestDistance.Text = "Лучшая стоимость";
            this.mmiShowBestDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mmiShowBestDistance.Click += new System.EventHandler(this.mmiShowBestDistance_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(261, 6);
            // 
            // mmiDrawCurrentTour
            // 
            this.mmiDrawCurrentTour.Name = "mmiDrawCurrentTour";
            this.mmiDrawCurrentTour.Size = new System.Drawing.Size(264, 22);
            this.mmiDrawCurrentTour.Text = "Нарисовать график текущего тура";
            this.mmiDrawCurrentTour.Click += new System.EventHandler(this.mmiDrawCurrentTour_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(261, 6);
            // 
            // mmiTesting
            // 
            this.mmiTesting.Name = "mmiTesting";
            this.mmiTesting.Size = new System.Drawing.Size(264, 22);
            this.mmiTesting.Text = "Тестирование";
            this.mmiTesting.Click += new System.EventHandler(this.mmiTesting_Click);
            // 
            // mmiHelp
            // 
            this.mmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmiAbout});
            this.mmiHelp.Name = "mmiHelp";
            this.mmiHelp.Size = new System.Drawing.Size(65, 20);
            this.mmiHelp.Text = "Справка";
            // 
            // mmiAbout
            // 
            this.mmiAbout.Name = "mmiAbout";
            this.mmiAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mmiAbout.Size = new System.Drawing.Size(168, 22);
            this.mmiAbout.Text = "О программе";
            this.mmiAbout.Click += new System.EventHandler(this.mmiAbout_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "Все файлы|*.*|Файлы проекта Journey|*.jproj";
            this.OpenFileDialog.FilterIndex = 2;
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "Все файлы|*.*|Файлы проекта Journey|*.jproj";
            this.SaveFileDialog.FilterIndex = 2;
            // 
            // OpenTourDialog
            // 
            this.OpenTourDialog.Filter = "Все файлы|*.*|Файлы TSP туров|*.tour";
            this.OpenTourDialog.FilterIndex = 2;
            // 
            // SaveTourDialog
            // 
            this.SaveTourDialog.Filter = "Все файлы|*.*|Файлы TSP туров|*.tour";
            this.SaveTourDialog.FilterIndex = 2;
            // 
            // MapControl
            // 
            this.MapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapControl.Location = new System.Drawing.Point(0, 0);
            this.MapControl.Map = null;
            this.MapControl.Name = "MapControl";
            this.MapControl.Path = null;
            this.MapControl.Size = new System.Drawing.Size(617, 327);
            this.MapControl.TabIndex = 25;
            this.MapControl.Tag = "";
            this.MapControl.Text = "MapControl";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 543);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Journey";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.ParametersGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProblemGV)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView ParametersGV;
        private System.Windows.Forms.DataGridView ProblemGV;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripMenuItem mmiOpen;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvclParName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvclParValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvclKeyword;
        private System.Windows.Forms.DataGridViewTextBoxColumn gvclValue;
        private MapControl MapControl;
        private System.Windows.Forms.ToolStripMenuItem mmiExit;
        private System.Windows.Forms.ToolStripMenuItem mmiTSP;
        private System.Windows.Forms.ToolStripMenuItem mmiShowDemands;
        private System.Windows.Forms.ToolStripMenuItem mmiShowDepots;
        private System.Windows.Forms.ToolStripMenuItem mmiShowDisplayCoords;
        private System.Windows.Forms.ToolStripMenuItem mmiTour;
        private System.Windows.Forms.ToolStripMenuItem mmiTourLength;
        private System.Windows.Forms.ToolStripMenuItem mmiShowTour;
        private System.Windows.Forms.ToolStripMenuItem mmiSaveTour;
        private System.Windows.Forms.ToolStripMenuItem mmiSolution;
        private System.Windows.Forms.ToolStripMenuItem mmiRandom;
        private System.Windows.Forms.ToolStripMenuItem mmiNearestNeigbor;
        private System.Windows.Forms.ToolStripMenuItem mmi2Opt;
        private System.Windows.Forms.ToolStripMenuItem mmiShowCostMatrix;
        private System.Windows.Forms.ToolStripMenuItem mmiSaveAs;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem mmiClose;
        private System.Windows.Forms.ToolStripMenuItem mmiSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mmiShowBestDistance;
        private System.Windows.Forms.ToolStripMenuItem mmiNodesCount;
        private System.Windows.Forms.ToolStripMenuItem mmiLinKernighan;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mmiDrawCurrentTour;
        private System.Windows.Forms.ToolStripMenuItem mmiShowNodes;
        private System.Windows.Forms.ToolStripMenuItem mmiHelp;
        private System.Windows.Forms.ToolStripMenuItem mmiAbout;
        private System.Windows.Forms.ToolStripMenuItem mmiLoadTour;
        private System.Windows.Forms.ToolStripMenuItem mmiSaveTourAs;
        private System.Windows.Forms.ToolStripMenuItem mmiLoadTourAs;
        private System.Windows.Forms.OpenFileDialog OpenTourDialog;
        private System.Windows.Forms.SaveFileDialog SaveTourDialog;
        private System.Windows.Forms.ToolStripMenuItem mmiClearLogs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mmiTesting;
    }
}

