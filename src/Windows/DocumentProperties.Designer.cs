using System.ComponentModel;
using System.Windows.Forms;

namespace Faktury.Windows
{
    partial class DocumentProperties
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LVEServices = new ListViewEx.ListViewEx();
            this.Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PKWiU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VAT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CBService = new System.Windows.Forms.ToolStripComboBox();
            this.dodajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.usuńWszystkieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TBSlownie = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TBTotalBrutto = new System.Windows.Forms.TextBox();
            this.TBTotalVAT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TBTotalNetto = new System.Windows.Forms.TextBox();
            this.TBListViewExTB = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contextMenuStrip.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LVEServices
            // 
            this.LVEServices.AllowColumnReorder = true;
            this.LVEServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Number,
            this.NameColumn,
            this.PKWiU,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.VAT});
            this.LVEServices.ContextMenuStrip = this.contextMenuStrip;
            this.LVEServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LVEServices.DoubleClickActivation = true;
            this.LVEServices.FullRowSelect = true;
            this.LVEServices.Location = new System.Drawing.Point(0, 0);
            this.LVEServices.Margin = new System.Windows.Forms.Padding(6);
            this.LVEServices.MultiSelect = false;
            this.LVEServices.Name = "LVEServices";
            this.LVEServices.Size = new System.Drawing.Size(1436, 685);
            this.LVEServices.TabIndex = 0;
            this.LVEServices.UseCompatibleStateImageBehavior = false;
            this.LVEServices.View = System.Windows.Forms.View.Details;
            this.LVEServices.SubItemClicked += new ListViewEx.SubItemEventHandler(this.LVEServices_SubItemClicked);
            // 
            // Number
            // 
            this.Number.Text = "#";
            this.Number.Width = 35;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Nazwa";
            this.NameColumn.Width = 176;
            // 
            // PKWiU
            // 
            this.PKWiU.Text = "PKWiU";
            this.PKWiU.Width = 128;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cena";
            this.columnHeader3.Width = 112;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ilość";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "j.m.";
            this.columnHeader5.Width = 85;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Wartość bez VAT";
            this.columnHeader6.Width = 203;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Kwota VAT";
            this.columnHeader7.Width = 158;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Wartość z VAT";
            this.columnHeader8.Width = 166;
            // 
            // VAT
            // 
            this.VAT.Text = "VAT %";
            this.VAT.Width = 117;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CBService,
            this.dodajToolStripMenuItem,
            this.toolStripSeparator2,
            this.resetToolStripMenuItem,
            this.usuńToolStripMenuItem,
            this.toolStripSeparator1,
            this.usuńWszystkieToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(389, 204);
            // 
            // CBService
            // 
            this.CBService.Name = "CBService";
            this.CBService.Size = new System.Drawing.Size(160, 40);
            // 
            // dodajToolStripMenuItem
            // 
            this.dodajToolStripMenuItem.Name = "dodajToolStripMenuItem";
            this.dodajToolStripMenuItem.Size = new System.Drawing.Size(388, 36);
            this.dodajToolStripMenuItem.Text = "Dodaj";
            this.dodajToolStripMenuItem.Click += new System.EventHandler(this.RecordAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(385, 6);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(388, 36);
            this.resetToolStripMenuItem.Text = "Przywróć wartości domyślne";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.RecordEdit_Click);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(388, 36);
            this.usuńToolStripMenuItem.Text = "Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.RecordDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(385, 6);
            // 
            // usuńWszystkieToolStripMenuItem
            // 
            this.usuńWszystkieToolStripMenuItem.Name = "usuńWszystkieToolStripMenuItem";
            this.usuńWszystkieToolStripMenuItem.Size = new System.Drawing.Size(388, 36);
            this.usuńWszystkieToolStripMenuItem.Text = "Usuń wszystkie";
            this.usuńWszystkieToolStripMenuItem.Click += new System.EventHandler(this.RecordDeleteAll_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(590, 13);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "Słownie:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.TBSlownie);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.TBTotalBrutto);
            this.panel3.Controls.Add(this.TBTotalVAT);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.TBTotalNetto);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 685);
            this.panel3.Margin = new System.Windows.Forms.Padding(6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1436, 98);
            this.panel3.TabIndex = 2;
            // 
            // TBSlownie
            // 
            this.TBSlownie.Location = new System.Drawing.Point(596, 44);
            this.TBSlownie.Margin = new System.Windows.Forms.Padding(6);
            this.TBSlownie.Name = "TBSlownie";
            this.TBSlownie.Size = new System.Drawing.Size(830, 31);
            this.TBSlownie.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(420, 13);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "Wartość z VAT";
            // 
            // TBTotalBrutto
            // 
            this.TBTotalBrutto.BackColor = System.Drawing.SystemColors.Window;
            this.TBTotalBrutto.Location = new System.Drawing.Point(426, 44);
            this.TBTotalBrutto.Margin = new System.Windows.Forms.Padding(6);
            this.TBTotalBrutto.Name = "TBTotalBrutto";
            this.TBTotalBrutto.ReadOnly = true;
            this.TBTotalBrutto.Size = new System.Drawing.Size(148, 31);
            this.TBTotalBrutto.TabIndex = 5;
            // 
            // TBTotalVAT
            // 
            this.TBTotalVAT.BackColor = System.Drawing.SystemColors.Window;
            this.TBTotalVAT.Location = new System.Drawing.Point(292, 44);
            this.TBTotalVAT.Margin = new System.Windows.Forms.Padding(6);
            this.TBTotalVAT.Name = "TBTotalVAT";
            this.TBTotalVAT.ReadOnly = true;
            this.TBTotalVAT.Size = new System.Drawing.Size(118, 31);
            this.TBTotalVAT.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 13);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Kwota VAT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Wartość bez VAT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Razem:";
            // 
            // TBTotalNetto
            // 
            this.TBTotalNetto.BackColor = System.Drawing.SystemColors.Window;
            this.TBTotalNetto.Location = new System.Drawing.Point(104, 44);
            this.TBTotalNetto.Margin = new System.Windows.Forms.Padding(6);
            this.TBTotalNetto.Name = "TBTotalNetto";
            this.TBTotalNetto.ReadOnly = true;
            this.TBTotalNetto.Size = new System.Drawing.Size(172, 31);
            this.TBTotalNetto.TabIndex = 0;
            // 
            // TBListViewExTB
            // 
            this.TBListViewExTB.Location = new System.Drawing.Point(618, 463);
            this.TBListViewExTB.Margin = new System.Windows.Forms.Padding(6);
            this.TBListViewExTB.Name = "TBListViewExTB";
            this.TBListViewExTB.Size = new System.Drawing.Size(302, 31);
            this.TBListViewExTB.TabIndex = 7;
            this.TBListViewExTB.Visible = false;
            this.TBListViewExTB.VisibleChanged += new System.EventHandler(this.TBListViewExTB_VisibleChanged);
            this.TBListViewExTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBListViewExTB_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LVEServices);
            this.panel2.Controls.Add(this.TBListViewExTB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1436, 685);
            this.panel2.TabIndex = 3;
            // 
            // DocumentProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "DocumentProperties";
            this.Size = new System.Drawing.Size(1436, 783);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewEx.ListViewEx LVEServices;
        private ColumnHeader Number;
        private ColumnHeader NameColumn;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader VAT;
        private Label label7;
        private Panel panel3;
        private TextBox TBSlownie;
        private Label label6;
        private TextBox TBTotalBrutto;
        private TextBox TBTotalVAT;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox TBTotalNetto;
        private Panel panel2;
        private TextBox TBListViewExTB;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem usuńToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem usuńWszystkieToolStripMenuItem;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripComboBox CBService;
        private ToolStripMenuItem dodajToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ColumnHeader PKWiU;
    }
}
