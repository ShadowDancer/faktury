using System.ComponentModel;
using System.Windows.Forms;

namespace Faktury.Windows
{
    partial class DocumentWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentWindow));
            this.nUDNumber = new System.Windows.Forms.NumericUpDown();
            this.nUDYear = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CxBSimilarDates = new System.Windows.Forms.CheckBox();
            this.DTPSellDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.DTPIssueDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TBPaynamentTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CBPaynament = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbCompanyInfoText = new System.Windows.Forms.TextBox();
            this.CBCompanyTag = new System.Windows.Forms.ComboBox();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.documentProperties = new Faktury.Windows.DocumentProperties();
            ((System.ComponentModel.ISupportInitialize)(this.nUDNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDYear)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // nUDNumber
            // 
            this.nUDNumber.Location = new System.Drawing.Point(9, 32);
            this.nUDNumber.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUDNumber.Name = "nUDNumber";
            this.nUDNumber.Size = new System.Drawing.Size(70, 20);
            this.nUDNumber.TabIndex = 0;
            this.nUDNumber.ValueChanged += new System.EventHandler(this.nUDNumber_ValueChanged);
            // 
            // nUDYear
            // 
            this.nUDYear.Location = new System.Drawing.Point(85, 32);
            this.nUDYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nUDYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nUDYear.Name = "nUDYear";
            this.nUDYear.Size = new System.Drawing.Size(75, 20);
            this.nUDYear.TabIndex = 1;
            this.nUDYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nUDYear.ValueChanged += new System.EventHandler(this.nUDYear_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Numer faktury:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rok:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nUDNumber);
            this.groupBox1.Controls.Add(this.nUDYear);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 179);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Daty";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CxBSimilarDates);
            this.groupBox6.Controls.Add(this.DTPSellDate);
            this.groupBox6.Location = new System.Drawing.Point(6, 115);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 60);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Data Sprzedaży";
            // 
            // CxBSimilarDates
            // 
            this.CxBSimilarDates.AutoSize = true;
            this.CxBSimilarDates.Location = new System.Drawing.Point(6, 17);
            this.CxBSimilarDates.Name = "CxBSimilarDates";
            this.CxBSimilarDates.Size = new System.Drawing.Size(124, 17);
            this.CxBSimilarDates.TabIndex = 12;
            this.CxBSimilarDates.Text = "jak data wystawienia";
            this.CxBSimilarDates.UseVisualStyleBackColor = true;
            this.CxBSimilarDates.CheckedChanged += new System.EventHandler(this.CxBSimilarDates_CheckedChanged);
            // 
            // DTPSellDate
            // 
            this.DTPSellDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DTPSellDate.Location = new System.Drawing.Point(3, 37);
            this.DTPSellDate.Name = "DTPSellDate";
            this.DTPSellDate.Size = new System.Drawing.Size(194, 20);
            this.DTPSellDate.TabIndex = 12;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.DTPIssueDate);
            this.groupBox5.Location = new System.Drawing.Point(6, 58);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 51);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data Wystawienia";
            // 
            // DTPIssueDate
            // 
            this.DTPIssueDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DTPIssueDate.Location = new System.Drawing.Point(3, 16);
            this.DTPIssueDate.Name = "DTPIssueDate";
            this.DTPIssueDate.Size = new System.Drawing.Size(194, 20);
            this.DTPIssueDate.TabIndex = 0;
            this.DTPIssueDate.ValueChanged += new System.EventHandler(this.DTPIssueDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TBPaynamentTime);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CBPaynament);
            this.groupBox2.Location = new System.Drawing.Point(224, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 59);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Płatność";
            // 
            // TBPaynamentTime
            // 
            this.TBPaynamentTime.Location = new System.Drawing.Point(107, 32);
            this.TBPaynamentTime.Name = "TBPaynamentTime";
            this.TBPaynamentTime.Size = new System.Drawing.Size(95, 20);
            this.TBPaynamentTime.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Termin zapłaty:";
            // 
            // CBPaynament
            // 
            this.CBPaynament.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPaynament.FormattingEnabled = true;
            this.CBPaynament.Location = new System.Drawing.Point(6, 31);
            this.CBPaynament.Name = "CBPaynament";
            this.CBPaynament.Size = new System.Drawing.Size(95, 21);
            this.CBPaynament.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbCompanyInfoText);
            this.groupBox4.Controls.Add(this.CBCompanyTag);
            this.groupBox4.Location = new System.Drawing.Point(436, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 124);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Odbiorca";
            // 
            // tbCompanyInfoText
            // 
            this.tbCompanyInfoText.Location = new System.Drawing.Point(9, 43);
            this.tbCompanyInfoText.Multiline = true;
            this.tbCompanyInfoText.Name = "tbCompanyInfoText";
            this.tbCompanyInfoText.ReadOnly = true;
            this.tbCompanyInfoText.Size = new System.Drawing.Size(286, 75);
            this.tbCompanyInfoText.TabIndex = 4;
            // 
            // CBCompanyTag
            // 
            this.CBCompanyTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBCompanyTag.FormattingEnabled = true;
            this.CBCompanyTag.Location = new System.Drawing.Point(9, 19);
            this.CBCompanyTag.Name = "CBCompanyTag";
            this.CBCompanyTag.Size = new System.Drawing.Size(286, 21);
            this.CBCompanyTag.TabIndex = 3;
            this.CBCompanyTag.SelectedIndexChanged += new System.EventHandler(this.CBCompanyTag_SelectedIndexChanged);
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(549, 142);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(91, 49);
            this.Ok.TabIndex = 8;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(646, 142);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(91, 49);
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "Anuluj";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // documentProperties
            // 
            this.documentProperties.Location = new System.Drawing.Point(12, 197);
            this.documentProperties.ModelStore = null;
            this.documentProperties.Name = "documentProperties";
            this.documentProperties.Size = new System.Drawing.Size(725, 284);
            this.documentProperties.TabIndex = 12;
            // 
            // DocumentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 479);
            this.Controls.Add(this.documentProperties);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentWindow";
            this.Text = "DocumentWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentWindow_FormClosing);
            this.Load += new System.EventHandler(this.DocumentWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUDNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDYear)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox TBPaynamentTime;
        private Label label3;
        private ComboBox CBPaynament;
        private GroupBox groupBox4;
        private ComboBox CBCompanyTag;
        private GroupBox groupBox6;
        private GroupBox groupBox5;
        private Button Ok;
        private Button Cancel;
        public NumericUpDown nUDNumber;
        public NumericUpDown nUDYear;
        private DateTimePicker DTPIssueDate;
        private CheckBox CxBSimilarDates;
        private DateTimePicker DTPSellDate;
        private DocumentProperties documentProperties;
        private TextBox tbCompanyInfoText;
    }
}