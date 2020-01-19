using System.ComponentModel;
using System.Windows.Forms;

namespace Faktury.Windows
{
    partial class DocumentListWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentListWindow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CxBCompanyTagFilter = new System.Windows.Forms.CheckBox();
            this.CBCompanyTag = new System.Windows.Forms.ComboBox();
            this.groupBoxDateFilter = new System.Windows.Forms.GroupBox();
            this.DTPDateFilter = new System.Windows.Forms.DateTimePicker();
            this.RBOlderThan = new System.Windows.Forms.RadioButton();
            this.RBFromDay = new System.Windows.Forms.RadioButton();
            this.RBYoungerThan = new System.Windows.Forms.RadioButton();
            this.cBDateFilter = new System.Windows.Forms.CheckBox();
            this.nUDYear = new System.Windows.Forms.NumericUpDown();
            this.cBYearFilter = new System.Windows.Forms.CheckBox();
            this.LVDocuments = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nowyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edytujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.odświeżToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cBAutoRefreshList = new System.Windows.Forms.ToolStripMenuItem();
            this.pokażFiltryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reverseVAT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBoxDateFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDYear)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CxBCompanyTagFilter);
            this.groupBox1.Controls.Add(this.CBCompanyTag);
            this.groupBox1.Controls.Add(this.groupBoxDateFilter);
            this.groupBox1.Controls.Add(this.cBDateFilter);
            this.groupBox1.Controls.Add(this.nUDYear);
            this.groupBox1.Controls.Add(this.cBYearFilter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 505);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtry";
            // 
            // CxBCompanyTagFilter
            // 
            this.CxBCompanyTagFilter.AutoSize = true;
            this.CxBCompanyTagFilter.Location = new System.Drawing.Point(6, 165);
            this.CxBCompanyTagFilter.Name = "CxBCompanyTagFilter";
            this.CxBCompanyTagFilter.Size = new System.Drawing.Size(78, 17);
            this.CxBCompanyTagFilter.TabIndex = 12;
            this.CxBCompanyTagFilter.Text = "Kontrahent";
            this.CxBCompanyTagFilter.UseVisualStyleBackColor = true;
            this.CxBCompanyTagFilter.CheckedChanged += new System.EventHandler(this.CxBCompanyTagFilter_CheckedChanged);
            // 
            // CBCompanyTag
            // 
            this.CBCompanyTag.Enabled = false;
            this.CBCompanyTag.FormattingEnabled = true;
            this.CBCompanyTag.Location = new System.Drawing.Point(106, 163);
            this.CBCompanyTag.Name = "CBCompanyTag";
            this.CBCompanyTag.Size = new System.Drawing.Size(190, 21);
            this.CBCompanyTag.TabIndex = 8;
            this.CBCompanyTag.SelectedIndexChanged += new System.EventHandler(this.CBCompanyTag_SelectedIndexChanged);
            // 
            // groupBoxDateFilter
            // 
            this.groupBoxDateFilter.Controls.Add(this.DTPDateFilter);
            this.groupBoxDateFilter.Controls.Add(this.RBOlderThan);
            this.groupBoxDateFilter.Controls.Add(this.RBFromDay);
            this.groupBoxDateFilter.Controls.Add(this.RBYoungerThan);
            this.groupBoxDateFilter.Location = new System.Drawing.Point(64, 44);
            this.groupBoxDateFilter.Name = "groupBoxDateFilter";
            this.groupBoxDateFilter.Size = new System.Drawing.Size(244, 115);
            this.groupBoxDateFilter.TabIndex = 4;
            this.groupBoxDateFilter.TabStop = false;
            this.groupBoxDateFilter.Text = "Data";
            // 
            // DTPDateFilter
            // 
            this.DTPDateFilter.Location = new System.Drawing.Point(6, 88);
            this.DTPDateFilter.Name = "DTPDateFilter";
            this.DTPDateFilter.Size = new System.Drawing.Size(232, 20);
            this.DTPDateFilter.TabIndex = 9;
            this.DTPDateFilter.ValueChanged += new System.EventHandler(this.DTPDateFilter_ValueChanged);
            // 
            // RBOlderThan
            // 
            this.RBOlderThan.AutoSize = true;
            this.RBOlderThan.Location = new System.Drawing.Point(6, 65);
            this.RBOlderThan.Name = "RBOlderThan";
            this.RBOlderThan.Size = new System.Drawing.Size(79, 17);
            this.RBOlderThan.TabIndex = 8;
            this.RBOlderThan.Text = "Starsze niż:";
            this.RBOlderThan.UseVisualStyleBackColor = true;
            this.RBOlderThan.CheckedChanged += new System.EventHandler(this.RBOlderThan_CheckedChanged);
            // 
            // RBFromDay
            // 
            this.RBFromDay.AutoSize = true;
            this.RBFromDay.Checked = true;
            this.RBFromDay.Location = new System.Drawing.Point(6, 42);
            this.RBFromDay.Name = "RBFromDay";
            this.RBFromDay.Size = new System.Drawing.Size(58, 17);
            this.RBFromDay.TabIndex = 7;
            this.RBFromDay.TabStop = true;
            this.RBFromDay.Text = "Z dnia:";
            this.RBFromDay.UseVisualStyleBackColor = true;
            this.RBFromDay.CheckedChanged += new System.EventHandler(this.RBFromDay_CheckedChanged);
            // 
            // RBYoungerThan
            // 
            this.RBYoungerThan.AutoSize = true;
            this.RBYoungerThan.Location = new System.Drawing.Point(6, 19);
            this.RBYoungerThan.Name = "RBYoungerThan";
            this.RBYoungerThan.Size = new System.Drawing.Size(85, 17);
            this.RBYoungerThan.TabIndex = 6;
            this.RBYoungerThan.Text = "Młodsze niż:";
            this.RBYoungerThan.UseVisualStyleBackColor = true;
            this.RBYoungerThan.CheckedChanged += new System.EventHandler(this.RBYoungerThan_CheckedChanged);
            // 
            // cBDateFilter
            // 
            this.cBDateFilter.AutoSize = true;
            this.cBDateFilter.Location = new System.Drawing.Point(6, 44);
            this.cBDateFilter.Name = "cBDateFilter";
            this.cBDateFilter.Size = new System.Drawing.Size(52, 17);
            this.cBDateFilter.TabIndex = 3;
            this.cBDateFilter.Text = "Data:";
            this.cBDateFilter.UseVisualStyleBackColor = true;
            this.cBDateFilter.CheckedChanged += new System.EventHandler(this.cBDate_CheckedChanged);
            // 
            // nUDYear
            // 
            this.nUDYear.Enabled = false;
            this.nUDYear.Location = new System.Drawing.Point(64, 19);
            this.nUDYear.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nUDYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nUDYear.Name = "nUDYear";
            this.nUDYear.Size = new System.Drawing.Size(126, 20);
            this.nUDYear.TabIndex = 1;
            this.nUDYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nUDYear.ValueChanged += new System.EventHandler(this.nUDYear_ValueChanged);
            // 
            // cBYearFilter
            // 
            this.cBYearFilter.AutoSize = true;
            this.cBYearFilter.Location = new System.Drawing.Point(6, 20);
            this.cBYearFilter.Name = "cBYearFilter";
            this.cBYearFilter.Size = new System.Drawing.Size(49, 17);
            this.cBYearFilter.TabIndex = 0;
            this.cBYearFilter.Text = "Rok:";
            this.cBYearFilter.UseVisualStyleBackColor = true;
            this.cBYearFilter.CheckedChanged += new System.EventHandler(this.cBYearFilter_CheckedChanged);
            // 
            // LVDocuments
            // 
            this.LVDocuments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader3,
            this.reverseVAT});
            this.LVDocuments.ContextMenuStrip = this.contextMenuStrip;
            this.LVDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LVDocuments.FullRowSelect = true;
            this.LVDocuments.HideSelection = false;
            this.LVDocuments.Location = new System.Drawing.Point(0, 0);
            this.LVDocuments.Name = "LVDocuments";
            this.LVDocuments.Size = new System.Drawing.Size(703, 505);
            this.LVDocuments.TabIndex = 1;
            this.LVDocuments.UseCompatibleStateImageBehavior = false;
            this.LVDocuments.View = System.Windows.Forms.View.Details;
            this.LVDocuments.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LVDocuments_ColumnClick);
            this.LVDocuments.DoubleClick += new System.EventHandler(this.BOpenSelected_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nr";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader1.Width = 44;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Rok";
            this.columnHeader2.Width = 63;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Kontrahent";
            this.columnHeader4.Width = 283;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Data Wystawienia";
            this.columnHeader6.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Suma Netto";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 96;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nowyToolStripMenuItem,
            this.edytujToolStripMenuItem,
            this.usuńToolStripMenuItem,
            this.toolStripSeparator3,
            this.odświeżToolStripMenuItem,
            this.cBAutoRefreshList,
            this.pokażFiltryToolStripMenuItem,
            this.toolStripSeparator2,
            this.zamknijToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(209, 170);
            // 
            // nowyToolStripMenuItem
            // 
            this.nowyToolStripMenuItem.Name = "nowyToolStripMenuItem";
            this.nowyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.nowyToolStripMenuItem.Text = "Nowy";
            this.nowyToolStripMenuItem.Click += new System.EventHandler(this.nowyToolStripMenuItem_Click);
            // 
            // edytujToolStripMenuItem
            // 
            this.edytujToolStripMenuItem.Name = "edytujToolStripMenuItem";
            this.edytujToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.edytujToolStripMenuItem.Text = "Edytuj";
            this.edytujToolStripMenuItem.Click += new System.EventHandler(this.BOpenSelected_Click);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.usuńToolStripMenuItem.Text = "Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.BRemoveSelected_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // odświeżToolStripMenuItem
            // 
            this.odświeżToolStripMenuItem.Name = "odświeżToolStripMenuItem";
            this.odświeżToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.odświeżToolStripMenuItem.Text = "Odśwież";
            this.odświeżToolStripMenuItem.Click += new System.EventHandler(this.BRefreshList_Click);
            // 
            // cBAutoRefreshList
            // 
            this.cBAutoRefreshList.CheckOnClick = true;
            this.cBAutoRefreshList.Name = "cBAutoRefreshList";
            this.cBAutoRefreshList.Size = new System.Drawing.Size(208, 22);
            this.cBAutoRefreshList.Text = "Odświeżaj automatycznie";
            this.cBAutoRefreshList.Click += new System.EventHandler(this.cBAutoRefreshList_Click);
            // 
            // pokażFiltryToolStripMenuItem
            // 
            this.pokażFiltryToolStripMenuItem.CheckOnClick = true;
            this.pokażFiltryToolStripMenuItem.Name = "pokażFiltryToolStripMenuItem";
            this.pokażFiltryToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.pokażFiltryToolStripMenuItem.Text = "Pokaż filtry";
            this.pokażFiltryToolStripMenuItem.Click += new System.EventHandler(this.pokażFiltryToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // zamknijToolStripMenuItem
            // 
            this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
            this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.zamknijToolStripMenuItem.Text = "Zamknij";
            this.zamknijToolStripMenuItem.Click += new System.EventHandler(this.BZamknij_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 505);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LVDocuments);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(316, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(703, 505);
            this.panel2.TabIndex = 3;
            // 
            // reverseVAT
            // 
            this.reverseVAT.Text = "Odwr. VAT";
            this.reverseVAT.Width = 80;
            // 
            // DocumentListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 505);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentListWindow";
            this.Text = "Dokumenty";
            this.Activated += new System.EventHandler(this.DocumentList_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DocumentListWindow_FormClosed);
            this.Load += new System.EventHandler(this.OpenDocument_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxDateFilter.ResumeLayout(false);
            this.groupBoxDateFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDYear)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown nUDYear;
        private CheckBox cBYearFilter;
        private GroupBox groupBoxDateFilter;
        private CheckBox cBDateFilter;
        private ComboBox CBCompanyTag;
        private ListView LVDocuments;
        private Panel panel1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader6;
        private Panel panel2;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem edytujToolStripMenuItem;
        private ToolStripMenuItem usuńToolStripMenuItem;
        private ToolStripMenuItem odświeżToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem zamknijToolStripMenuItem;
        private ToolStripMenuItem nowyToolStripMenuItem;
        private ToolStripMenuItem cBAutoRefreshList;
        private ToolStripMenuItem pokażFiltryToolStripMenuItem;
        private RadioButton RBOlderThan;
        private RadioButton RBFromDay;
        private RadioButton RBYoungerThan;
        private CheckBox CxBCompanyTagFilter;
        private ToolStripSeparator toolStripSeparator3;
        private DateTimePicker DTPDateFilter;
        private ColumnHeader columnHeader3;
        private ColumnHeader reverseVAT;
    }
}