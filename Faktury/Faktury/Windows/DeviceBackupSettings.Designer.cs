using System.ComponentModel;
using System.Windows.Forms;

namespace Faktury.Windows
{
    partial class BackupSettings
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
            this.CxBDeviceBackup = new System.Windows.Forms.CheckBox();
            this.GBDevice = new System.Windows.Forms.GroupBox();
            this.CBSelectDevice = new System.Windows.Forms.ComboBox();
            this.TBDeviceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BChangeDevice = new System.Windows.Forms.Button();
            this.BReset = new System.Windows.Forms.Button();
            this.LElapsedTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NuDPeriod = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.CxBLocalBackup = new System.Windows.Forms.CheckBox();
            this.CxBLocalBackupOnlyWhenExit = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GBLocalCopy = new System.Windows.Forms.GroupBox();
            this.BClose = new System.Windows.Forms.Button();
            this.OpenBackupDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.GBDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NuDPeriod)).BeginInit();
            this.GBLocalCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // CxBDeviceBackup
            // 
            this.CxBDeviceBackup.AutoSize = true;
            this.CxBDeviceBackup.Location = new System.Drawing.Point(12, 105);
            this.CxBDeviceBackup.Name = "CxBDeviceBackup";
            this.CxBDeviceBackup.Size = new System.Drawing.Size(247, 17);
            this.CxBDeviceBackup.TabIndex = 0;
            this.CxBDeviceBackup.Text = "Włącz mechanizm kopii zapasowej na nośniku";
            this.CxBDeviceBackup.UseVisualStyleBackColor = true;
            this.CxBDeviceBackup.CheckedChanged += new System.EventHandler(this.CxBDeviceBackup_CheckedChanged);
            // 
            // GBDevice
            // 
            this.GBDevice.Controls.Add(this.CBSelectDevice);
            this.GBDevice.Controls.Add(this.TBDeviceName);
            this.GBDevice.Controls.Add(this.label4);
            this.GBDevice.Controls.Add(this.BChangeDevice);
            this.GBDevice.Controls.Add(this.BReset);
            this.GBDevice.Controls.Add(this.LElapsedTime);
            this.GBDevice.Controls.Add(this.label3);
            this.GBDevice.Controls.Add(this.NuDPeriod);
            this.GBDevice.Controls.Add(this.label2);
            this.GBDevice.Location = new System.Drawing.Point(12, 128);
            this.GBDevice.Name = "GBDevice";
            this.GBDevice.Size = new System.Drawing.Size(480, 86);
            this.GBDevice.TabIndex = 1;
            this.GBDevice.TabStop = false;
            this.GBDevice.Text = "Kopia na urządzeniu";
            // 
            // CBSelectDevice
            // 
            this.CBSelectDevice.FormattingEnabled = true;
            this.CBSelectDevice.Location = new System.Drawing.Point(311, 12);
            this.CBSelectDevice.Name = "CBSelectDevice";
            this.CBSelectDevice.Size = new System.Drawing.Size(153, 21);
            this.CBSelectDevice.TabIndex = 10;
            this.CBSelectDevice.DropDown += new System.EventHandler(this.CBSelectDevice_DropDown);
            // 
            // TBDeviceName
            // 
            this.TBDeviceName.Location = new System.Drawing.Point(97, 13);
            this.TBDeviceName.Name = "TBDeviceName";
            this.TBDeviceName.ReadOnly = true;
            this.TBDeviceName.Size = new System.Drawing.Size(127, 20);
            this.TBDeviceName.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Aktualny nośnik:";
            // 
            // BChangeDevice
            // 
            this.BChangeDevice.Location = new System.Drawing.Point(230, 12);
            this.BChangeDevice.Name = "BChangeDevice";
            this.BChangeDevice.Size = new System.Drawing.Size(75, 21);
            this.BChangeDevice.TabIndex = 7;
            this.BChangeDevice.Text = "Zmień";
            this.BChangeDevice.UseVisualStyleBackColor = true;
            this.BChangeDevice.Click += new System.EventHandler(this.BChange_Click);
            // 
            // BReset
            // 
            this.BReset.Location = new System.Drawing.Point(340, 55);
            this.BReset.Name = "BReset";
            this.BReset.Size = new System.Drawing.Size(66, 22);
            this.BReset.TabIndex = 6;
            this.BReset.Text = "Reset";
            this.BReset.UseVisualStyleBackColor = true;
            this.BReset.Click += new System.EventHandler(this.BReset_Click);
            // 
            // LElapsedTime
            // 
            this.LElapsedTime.AutoSize = true;
            this.LElapsedTime.Location = new System.Drawing.Point(241, 60);
            this.LElapsedTime.Name = "LElapsedTime";
            this.LElapsedTime.Size = new System.Drawing.Size(93, 13);
            this.LElapsedTime.TabIndex = 5;
            this.LElapsedTime.Text = "Pozostało 666 dni";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "dni";
            // 
            // NuDPeriod
            // 
            this.NuDPeriod.Location = new System.Drawing.Point(81, 58);
            this.NuDPeriod.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.NuDPeriod.Name = "NuDPeriod";
            this.NuDPeriod.Size = new System.Drawing.Size(57, 20);
            this.NuDPeriod.TabIndex = 3;
            this.NuDPeriod.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Archiwizuj co";
            // 
            // CxBLocalBackup
            // 
            this.CxBLocalBackup.AutoSize = true;
            this.CxBLocalBackup.Location = new System.Drawing.Point(12, 12);
            this.CxBLocalBackup.Name = "CxBLocalBackup";
            this.CxBLocalBackup.Size = new System.Drawing.Size(180, 17);
            this.CxBLocalBackup.TabIndex = 2;
            this.CxBLocalBackup.Text = "Włącz mechanizm kopii lokalniej";
            this.CxBLocalBackup.UseVisualStyleBackColor = true;
            this.CxBLocalBackup.CheckedChanged += new System.EventHandler(this.CxBLocalBackup_CheckedChanged);
            // 
            // CxBLocalBackupOnlyWhenExit
            // 
            this.CxBLocalBackupOnlyWhenExit.AutoSize = true;
            this.CxBLocalBackupOnlyWhenExit.Location = new System.Drawing.Point(9, 19);
            this.CxBLocalBackupOnlyWhenExit.Name = "CxBLocalBackupOnlyWhenExit";
            this.CxBLocalBackupOnlyWhenExit.Size = new System.Drawing.Size(267, 17);
            this.CxBLocalBackupOnlyWhenExit.TabIndex = 3;
            this.CxBLocalBackupOnlyWhenExit.Text = "Archiwizuj dane tylko przy wychodzeniu z programu";
            this.CxBLocalBackupOnlyWhenExit.UseVisualStyleBackColor = true;
            this.CxBLocalBackupOnlyWhenExit.CheckedChanged += new System.EventHandler(this.CxBLocalBackupOnlyWhenExit_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ta opcja pozwala zaoszczędzić miejsce na dysku";
            // 
            // GBLocalCopy
            // 
            this.GBLocalCopy.Controls.Add(this.CxBLocalBackupOnlyWhenExit);
            this.GBLocalCopy.Controls.Add(this.label1);
            this.GBLocalCopy.Location = new System.Drawing.Point(12, 35);
            this.GBLocalCopy.Name = "GBLocalCopy";
            this.GBLocalCopy.Size = new System.Drawing.Size(305, 64);
            this.GBLocalCopy.TabIndex = 5;
            this.GBLocalCopy.TabStop = false;
            this.GBLocalCopy.Text = "Kopia lokalna";
            // 
            // BClose
            // 
            this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BClose.Location = new System.Drawing.Point(416, 221);
            this.BClose.Name = "BClose";
            this.BClose.Size = new System.Drawing.Size(73, 32);
            this.BClose.TabIndex = 6;
            this.BClose.Text = "Zamknij";
            this.BClose.UseVisualStyleBackColor = true;
            this.BClose.Click += new System.EventHandler(this.BClose_Click);
            // 
            // OpenBackupDirectory
            // 
            this.OpenBackupDirectory.Description = "Wybierz folder w którym zapisywane będą kopie zapasowe.";
            this.OpenBackupDirectory.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // BackupSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 265);
            this.Controls.Add(this.BClose);
            this.Controls.Add(this.GBLocalCopy);
            this.Controls.Add(this.CxBLocalBackup);
            this.Controls.Add(this.GBDevice);
            this.Controls.Add(this.CxBDeviceBackup);
            this.Name = "BackupSettings";
            this.Text = "Ustawienia kopii zapasowej";
            this.Load += new System.EventHandler(this.BackupSettings_Load);
            this.GBDevice.ResumeLayout(false);
            this.GBDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NuDPeriod)).EndInit();
            this.GBLocalCopy.ResumeLayout(false);
            this.GBLocalCopy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox CxBDeviceBackup;
        private GroupBox GBDevice;
        private CheckBox CxBLocalBackup;
        private Label label3;
        private NumericUpDown NuDPeriod;
        private Label label2;
        private CheckBox CxBLocalBackupOnlyWhenExit;
        private Label label1;
        private GroupBox GBLocalCopy;
        private Button BClose;
        private Button BReset;
        private Label LElapsedTime;
        private FolderBrowserDialog OpenBackupDirectory;
        private Button BChangeDevice;
        private ComboBox CBSelectDevice;
        private TextBox TBDeviceName;
        private Label label4;

    }
}