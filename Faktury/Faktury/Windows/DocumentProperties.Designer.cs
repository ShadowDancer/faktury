namespace Faktury.Windows
{
    partial class DocumentProperties
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.columnHeader1,
            this.columnHeader2,
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
            this.LVEServices.MultiSelect = false;
            this.LVEServices.Name = "LVEServices";
            this.LVEServices.Size = new System.Drawing.Size(718, 356);
            this.LVEServices.TabIndex = 0;
            this.LVEServices.UseCompatibleStateImageBehavior = false;
            this.LVEServices.View = System.Windows.Forms.View.Details;
            this.LVEServices.SubItemClicked += new ListViewEx.SubItemEventHandler(this.LVEServices_SubItemClicked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "LP";
            this.columnHeader1.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nazwa";
            this.columnHeader2.Width = 176;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cena";
            this.columnHeader3.Width = 46;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ilość";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "j.m.";
            this.columnHeader5.Width = 39;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Wartość bez VAT";
            this.columnHeader6.Width = 113;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Kwota VAT";
            this.columnHeader7.Width = 92;
            // 
            // columnHeader8
            // 
            this.columnHeader8.DisplayIndex = 8;
            this.columnHeader8.Text = "Wartość z VAT";
            this.columnHeader8.Width = 110;
            // 
            // VAT
            // 
            this.VAT.DisplayIndex = 7;
            this.VAT.Text = "VAT %";
            this.VAT.Width = 53;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CBService,
            this.dodajToolStripMenuItem,
            this.toolStripSeparator2,
            this.resetToolStripMenuItem,
            this.usuńToolStripMenuItem,
            this.toolStripSeparator1,
            this.usuńWszystkieToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(225, 131);
            // 
            // CBService
            // 
            this.CBService.Name = "CBService";
            this.CBService.Size = new System.Drawing.Size(160, 23);
            // 
            // dodajToolStripMenuItem
            // 
            this.dodajToolStripMenuItem.Name = "dodajToolStripMenuItem";
            this.dodajToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.dodajToolStripMenuItem.Text = "Dodaj";
            this.dodajToolStripMenuItem.Click += new System.EventHandler(this.RecordAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(221, 6);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.resetToolStripMenuItem.Text = "Przywróć wartości domyślne";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.RecordEdit_Click);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.usuńToolStripMenuItem.Text = "Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.RecordDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // usuńWszystkieToolStripMenuItem
            // 
            this.usuńWszystkieToolStripMenuItem.Name = "usuńWszystkieToolStripMenuItem";
            this.usuńWszystkieToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.usuńWszystkieToolStripMenuItem.Text = "Usuń wszystkie";
            this.usuńWszystkieToolStripMenuItem.Click += new System.EventHandler(this.RecordDeleteAll_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(295, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
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
            this.panel3.Location = new System.Drawing.Point(0, 356);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(718, 51);
            this.panel3.TabIndex = 2;
            // 
            // TBSlownie
            // 
            this.TBSlownie.Location = new System.Drawing.Point(298, 23);
            this.TBSlownie.Name = "TBSlownie";
            this.TBSlownie.Size = new System.Drawing.Size(417, 20);
            this.TBSlownie.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Wartość z VAT";
            // 
            // TBTotalBrutto
            // 
            this.TBTotalBrutto.BackColor = System.Drawing.SystemColors.Window;
            this.TBTotalBrutto.Location = new System.Drawing.Point(213, 23);
            this.TBTotalBrutto.Name = "TBTotalBrutto";
            this.TBTotalBrutto.ReadOnly = true;
            this.TBTotalBrutto.Size = new System.Drawing.Size(76, 20);
            this.TBTotalBrutto.TabIndex = 5;
            // 
            // TBTotalVAT
            // 
            this.TBTotalVAT.BackColor = System.Drawing.SystemColors.Window;
            this.TBTotalVAT.Location = new System.Drawing.Point(146, 23);
            this.TBTotalVAT.Name = "TBTotalVAT";
            this.TBTotalVAT.ReadOnly = true;
            this.TBTotalVAT.Size = new System.Drawing.Size(61, 20);
            this.TBTotalVAT.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Kwota VAT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Wartość bez VAT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Razem:";
            // 
            // TBTotalNetto
            // 
            this.TBTotalNetto.BackColor = System.Drawing.SystemColors.Window;
            this.TBTotalNetto.Location = new System.Drawing.Point(52, 23);
            this.TBTotalNetto.Name = "TBTotalNetto";
            this.TBTotalNetto.ReadOnly = true;
            this.TBTotalNetto.Size = new System.Drawing.Size(88, 20);
            this.TBTotalNetto.TabIndex = 0;
            // 
            // TBListViewExTB
            // 
            this.TBListViewExTB.Location = new System.Drawing.Point(309, 241);
            this.TBListViewExTB.Name = "TBListViewExTB";
            this.TBListViewExTB.Size = new System.Drawing.Size(153, 20);
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
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 356);
            this.panel2.TabIndex = 3;
            // 
            // DocumentProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "DocumentProperties";
            this.Size = new System.Drawing.Size(718, 407);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewEx.ListViewEx LVEServices;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader VAT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox TBSlownie;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TBTotalBrutto;
        private System.Windows.Forms.TextBox TBTotalVAT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBTotalNetto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TBListViewExTB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem usuńWszystkieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox CBService;
        private System.Windows.Forms.ToolStripMenuItem dodajToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
