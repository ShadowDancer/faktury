namespace Faktury.Windows
{
    partial class SQLSettings
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
            this.GBDatabaseSettings = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TBName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TBUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TBPassword = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TBAdress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BDisconnect = new System.Windows.Forms.Button();
            this.BConnect = new System.Windows.Forms.Button();
            this.BRefreshStatus = new System.Windows.Forms.Button();
            this.LConectionState = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BClose = new System.Windows.Forms.Button();
            this.GBDatabaseSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBDatabaseSettings
            // 
            this.GBDatabaseSettings.Controls.Add(this.label6);
            this.GBDatabaseSettings.Controls.Add(this.TBName);
            this.GBDatabaseSettings.Controls.Add(this.label5);
            this.GBDatabaseSettings.Controls.Add(this.TBUser);
            this.GBDatabaseSettings.Controls.Add(this.label3);
            this.GBDatabaseSettings.Controls.Add(this.TBPassword);
            this.GBDatabaseSettings.Controls.Add(this.label2);
            this.GBDatabaseSettings.Controls.Add(this.TBAdress);
            this.GBDatabaseSettings.Controls.Add(this.label1);
            this.GBDatabaseSettings.Location = new System.Drawing.Point(12, 12);
            this.GBDatabaseSettings.Name = "GBDatabaseSettings";
            this.GBDatabaseSettings.Size = new System.Drawing.Size(293, 214);
            this.GBDatabaseSettings.TabIndex = 0;
            this.GBDatabaseSettings.TabStop = false;
            this.GBDatabaseSettings.Text = "Ustawienia";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Nazwa";
            // 
            // TBName
            // 
            this.TBName.Location = new System.Drawing.Point(9, 71);
            this.TBName.Name = "TBName";
            this.TBName.Size = new System.Drawing.Size(270, 20);
            this.TBName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Po ustawieniu danych spróbuj połączyć się z bazą!";
            // 
            // TBUser
            // 
            this.TBUser.Location = new System.Drawing.Point(6, 112);
            this.TBUser.Name = "TBUser";
            this.TBUser.Size = new System.Drawing.Size(270, 20);
            this.TBUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hasło";
            // 
            // TBPassword
            // 
            this.TBPassword.Location = new System.Drawing.Point(6, 149);
            this.TBPassword.Name = "TBPassword";
            this.TBPassword.Size = new System.Drawing.Size(270, 20);
            this.TBPassword.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Użytkownik";
            // 
            // TBAdress
            // 
            this.TBAdress.Location = new System.Drawing.Point(6, 32);
            this.TBAdress.Name = "TBAdress";
            this.TBAdress.Size = new System.Drawing.Size(270, 20);
            this.TBAdress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adres";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BDisconnect);
            this.groupBox1.Controls.Add(this.BConnect);
            this.groupBox1.Controls.Add(this.BRefreshStatus);
            this.groupBox1.Controls.Add(this.LConectionState);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(311, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // BDisconnect
            // 
            this.BDisconnect.Location = new System.Drawing.Point(108, 62);
            this.BDisconnect.Name = "BDisconnect";
            this.BDisconnect.Size = new System.Drawing.Size(82, 29);
            this.BDisconnect.TabIndex = 7;
            this.BDisconnect.Text = "Rozłącz";
            this.BDisconnect.UseVisualStyleBackColor = true;
            this.BDisconnect.Click += new System.EventHandler(this.BDisconnect_Click);
            // 
            // BConnect
            // 
            this.BConnect.Location = new System.Drawing.Point(20, 62);
            this.BConnect.Name = "BConnect";
            this.BConnect.Size = new System.Drawing.Size(82, 29);
            this.BConnect.TabIndex = 6;
            this.BConnect.Text = "Połącz";
            this.BConnect.UseVisualStyleBackColor = true;
            this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
            // 
            // BRefreshStatus
            // 
            this.BRefreshStatus.Location = new System.Drawing.Point(108, 16);
            this.BRefreshStatus.Name = "BRefreshStatus";
            this.BRefreshStatus.Size = new System.Drawing.Size(82, 29);
            this.BRefreshStatus.TabIndex = 5;
            this.BRefreshStatus.Text = "Odśwież";
            this.BRefreshStatus.UseVisualStyleBackColor = true;
            this.BRefreshStatus.Click += new System.EventHandler(this.BRefreshStatus_Click);
            // 
            // LConectionState
            // 
            this.LConectionState.AutoSize = true;
            this.LConectionState.Location = new System.Drawing.Point(6, 32);
            this.LConectionState.Name = "LConectionState";
            this.LConectionState.Size = new System.Drawing.Size(37, 13);
            this.LConectionState.TabIndex = 2;
            this.LConectionState.Text = "Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Status połączenia:";
            // 
            // BClose
            // 
            this.BClose.Location = new System.Drawing.Point(414, 193);
            this.BClose.Name = "BClose";
            this.BClose.Size = new System.Drawing.Size(102, 33);
            this.BClose.TabIndex = 2;
            this.BClose.Text = "Zamknij";
            this.BClose.UseVisualStyleBackColor = true;
            this.BClose.Click += new System.EventHandler(this.BClose_Click);
            // 
            // SQLSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 240);
            this.Controls.Add(this.BClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GBDatabaseSettings);
            this.Name = "SQLSettings";
            this.Text = "SQLSettings";
            this.Load += new System.EventHandler(this.SQLSettings_Load);
            this.GBDatabaseSettings.ResumeLayout(false);
            this.GBDatabaseSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBDatabaseSettings;
        private System.Windows.Forms.TextBox TBUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox TBPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBAdress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BDisconnect;
        private System.Windows.Forms.Button BConnect;
        private System.Windows.Forms.Button BRefreshStatus;
        private System.Windows.Forms.Label LConectionState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TBName;
        private System.Windows.Forms.Button BClose;
    }
}