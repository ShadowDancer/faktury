namespace Faktury.Windows
{
    partial class OptionsWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsWindow));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BSetOwnerData = new System.Windows.Forms.Button();
            this.CompaniesClean = new System.Windows.Forms.Button();
            this.CompaniesExport = new System.Windows.Forms.Button();
            this.CompaniesImport = new System.Windows.Forms.Button();
            this.LoadBackup = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BDelUnit = new System.Windows.Forms.Button();
            this.BDelVat = new System.Windows.Forms.Button();
            this.BAddUnit = new System.Windows.Forms.Button();
            this.BAddVat = new System.Windows.Forms.Button();
            this.TBInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LBUnit = new System.Windows.Forms.ListBox();
            this.LBVat = new System.Windows.Forms.ListBox();
            this.BClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BBackupSettings = new System.Windows.Forms.Button();
            this.OpenDataDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.BSQLSettings = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.OptionsReset = new System.Windows.Forms.Button();
            this.OptionsExport = new System.Windows.Forms.Button();
            this.OptionsImport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BSetOwnerData);
            this.groupBox2.Controls.Add(this.CompaniesClean);
            this.groupBox2.Controls.Add(this.CompaniesExport);
            this.groupBox2.Controls.Add(this.CompaniesImport);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 64);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dane";
            // 
            // BSetOwnerData
            // 
            this.BSetOwnerData.Location = new System.Drawing.Point(264, 19);
            this.BSetOwnerData.Name = "BSetOwnerData";
            this.BSetOwnerData.Size = new System.Drawing.Size(92, 37);
            this.BSetOwnerData.TabIndex = 6;
            this.BSetOwnerData.Text = "Ustaw dane wystawiającego";
            this.BSetOwnerData.UseVisualStyleBackColor = true;
            this.BSetOwnerData.Click += new System.EventHandler(this.BSetOwnerData_Click);
            // 
            // CompaniesClean
            // 
            this.CompaniesClean.Location = new System.Drawing.Point(176, 19);
            this.CompaniesClean.Name = "CompaniesClean";
            this.CompaniesClean.Size = new System.Drawing.Size(82, 37);
            this.CompaniesClean.TabIndex = 3;
            this.CompaniesClean.Text = "Wyczyść";
            this.CompaniesClean.UseVisualStyleBackColor = true;
            this.CompaniesClean.Click += new System.EventHandler(this.DataClean_Click);
            // 
            // CompaniesExport
            // 
            this.CompaniesExport.Location = new System.Drawing.Point(91, 19);
            this.CompaniesExport.Name = "CompaniesExport";
            this.CompaniesExport.Size = new System.Drawing.Size(79, 37);
            this.CompaniesExport.TabIndex = 1;
            this.CompaniesExport.Text = "Eksportuj";
            this.CompaniesExport.UseVisualStyleBackColor = true;
            this.CompaniesExport.Click += new System.EventHandler(this.DataExport_Click);
            // 
            // CompaniesImport
            // 
            this.CompaniesImport.Location = new System.Drawing.Point(6, 19);
            this.CompaniesImport.Name = "CompaniesImport";
            this.CompaniesImport.Size = new System.Drawing.Size(79, 37);
            this.CompaniesImport.TabIndex = 0;
            this.CompaniesImport.Text = "Importuj";
            this.CompaniesImport.UseVisualStyleBackColor = true;
            this.CompaniesImport.Click += new System.EventHandler(this.DataImport_Click);
            // 
            // LoadBackup
            // 
            this.LoadBackup.Location = new System.Drawing.Point(6, 19);
            this.LoadBackup.Name = "LoadBackup";
            this.LoadBackup.Size = new System.Drawing.Size(126, 37);
            this.LoadBackup.TabIndex = 2;
            this.LoadBackup.Text = "Wczytaj kopie bezpieczeństwa";
            this.LoadBackup.UseVisualStyleBackColor = true;
            this.LoadBackup.Click += new System.EventHandler(this.LoadBackup_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BDelUnit);
            this.groupBox1.Controls.Add(this.BDelVat);
            this.groupBox1.Controls.Add(this.BAddUnit);
            this.groupBox1.Controls.Add(this.BAddVat);
            this.groupBox1.Controls.Add(this.TBInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LBUnit);
            this.groupBox1.Controls.Add(this.LBVat);
            this.groupBox1.Location = new System.Drawing.Point(12, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 200);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inne";
            // 
            // BDelUnit
            // 
            this.BDelUnit.Location = new System.Drawing.Point(230, 137);
            this.BDelUnit.Name = "BDelUnit";
            this.BDelUnit.Size = new System.Drawing.Size(60, 27);
            this.BDelUnit.TabIndex = 8;
            this.BDelUnit.Text = "Usuń";
            this.BDelUnit.UseVisualStyleBackColor = true;
            this.BDelUnit.Click += new System.EventHandler(this.BDelUnit_Click);
            // 
            // BDelVat
            // 
            this.BDelVat.Location = new System.Drawing.Point(72, 137);
            this.BDelVat.Name = "BDelVat";
            this.BDelVat.Size = new System.Drawing.Size(60, 27);
            this.BDelVat.TabIndex = 7;
            this.BDelVat.Text = "Usuń";
            this.BDelVat.UseVisualStyleBackColor = true;
            this.BDelVat.Click += new System.EventHandler(this.BDelVat_Click);
            // 
            // BAddUnit
            // 
            this.BAddUnit.Location = new System.Drawing.Point(164, 137);
            this.BAddUnit.Name = "BAddUnit";
            this.BAddUnit.Size = new System.Drawing.Size(60, 27);
            this.BAddUnit.TabIndex = 6;
            this.BAddUnit.Text = "Dodaj";
            this.BAddUnit.UseVisualStyleBackColor = true;
            this.BAddUnit.Click += new System.EventHandler(this.BAddUnit_Click);
            // 
            // BAddVat
            // 
            this.BAddVat.Location = new System.Drawing.Point(6, 137);
            this.BAddVat.Name = "BAddVat";
            this.BAddVat.Size = new System.Drawing.Size(60, 27);
            this.BAddVat.TabIndex = 5;
            this.BAddVat.Text = "Dodaj";
            this.BAddVat.UseVisualStyleBackColor = true;
            this.BAddVat.Click += new System.EventHandler(this.BAddVat_Click);
            // 
            // TBInput
            // 
            this.TBInput.Location = new System.Drawing.Point(6, 170);
            this.TBInput.Name = "TBInput";
            this.TBInput.Size = new System.Drawing.Size(303, 20);
            this.TBInput.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Jednostki";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "VAT:";
            // 
            // LBUnit
            // 
            this.LBUnit.FormattingEnabled = true;
            this.LBUnit.Location = new System.Drawing.Point(164, 36);
            this.LBUnit.Name = "LBUnit";
            this.LBUnit.Size = new System.Drawing.Size(145, 95);
            this.LBUnit.Sorted = true;
            this.LBUnit.TabIndex = 1;
            // 
            // LBVat
            // 
            this.LBVat.FormattingEnabled = true;
            this.LBVat.Location = new System.Drawing.Point(6, 36);
            this.LBVat.Name = "LBVat";
            this.LBVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LBVat.Size = new System.Drawing.Size(152, 95);
            this.LBVat.Sorted = true;
            this.LBVat.TabIndex = 0;
            // 
            // BClose
            // 
            this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BClose.Location = new System.Drawing.Point(533, 309);
            this.BClose.Name = "BClose";
            this.BClose.Size = new System.Drawing.Size(107, 45);
            this.BClose.TabIndex = 5;
            this.BClose.Text = "Zamknij";
            this.BClose.UseVisualStyleBackColor = true;
            this.BClose.Click += new System.EventHandler(this.BClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BBackupSettings);
            this.groupBox3.Controls.Add(this.LoadBackup);
            this.groupBox3.Location = new System.Drawing.Point(12, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 64);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kopie bezpieczeństwa";
            // 
            // BBackupSettings
            // 
            this.BBackupSettings.Location = new System.Drawing.Point(138, 19);
            this.BBackupSettings.Name = "BBackupSettings";
            this.BBackupSettings.Size = new System.Drawing.Size(126, 37);
            this.BBackupSettings.TabIndex = 3;
            this.BBackupSettings.Text = "Ustawienia";
            this.BBackupSettings.UseVisualStyleBackColor = true;
            this.BBackupSettings.Click += new System.EventHandler(this.BBackupSettings_Click);
            // 
            // OpenDataDirectory
            // 
            this.OpenDataDirectory.Description = "Otwórz folder z danymi";
            this.OpenDataDirectory.ShowNewFolderButton = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.OptionsReset);
            this.groupBox4.Controls.Add(this.OptionsExport);
            this.groupBox4.Controls.Add(this.OptionsImport);
            this.groupBox4.Location = new System.Drawing.Point(380, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(269, 64);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Opcje";
            // 
            // OptionsReset
            // 
            this.OptionsReset.Location = new System.Drawing.Point(176, 19);
            this.OptionsReset.Name = "OptionsReset";
            this.OptionsReset.Size = new System.Drawing.Size(82, 37);
            this.OptionsReset.TabIndex = 3;
            this.OptionsReset.Text = "Przywróć domyślne";
            this.OptionsReset.UseVisualStyleBackColor = true;
            this.OptionsReset.Click += new System.EventHandler(this.OptionsReset_Click);
            // 
            // OptionsExport
            // 
            this.OptionsExport.Location = new System.Drawing.Point(91, 19);
            this.OptionsExport.Name = "OptionsExport";
            this.OptionsExport.Size = new System.Drawing.Size(79, 37);
            this.OptionsExport.TabIndex = 1;
            this.OptionsExport.Text = "Eksportuj";
            this.OptionsExport.UseVisualStyleBackColor = true;
            this.OptionsExport.Click += new System.EventHandler(this.OptionsExport_Click);
            // 
            // OptionsImport
            // 
            this.OptionsImport.Location = new System.Drawing.Point(6, 19);
            this.OptionsImport.Name = "OptionsImport";
            this.OptionsImport.Size = new System.Drawing.Size(79, 37);
            this.OptionsImport.TabIndex = 0;
            this.OptionsImport.Text = "Importuj";
            this.OptionsImport.UseVisualStyleBackColor = true;
            this.OptionsImport.Click += new System.EventHandler(this.OptionsImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 366);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.BSQLSettings);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.BClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsWindow";
            this.Text = "Opcje";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsWindow_FormClosed);
            this.Load += new System.EventHandler(this.OptionsWindow_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button LoadBackup;
        private System.Windows.Forms.Button CompaniesExport;
        private System.Windows.Forms.Button CompaniesImport;
        private System.Windows.Forms.Button CompaniesClean;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox LBUnit;
        private System.Windows.Forms.ListBox LBVat;
        private System.Windows.Forms.Button BDelUnit;
        private System.Windows.Forms.Button BDelVat;
        private System.Windows.Forms.Button BAddUnit;
        private System.Windows.Forms.Button BAddVat;
        private System.Windows.Forms.TextBox TBInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BClose;
        private System.Windows.Forms.Button BSetOwnerData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BBackupSettings;
        private System.Windows.Forms.FolderBrowserDialog OpenDataDirectory;
        private System.Windows.Forms.Button BSQLSettings;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button OptionsReset;
        private System.Windows.Forms.Button OptionsExport;
        private System.Windows.Forms.Button OptionsImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}