namespace Faktury.Windows
{
    partial class ServiceWindow
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
            this.CBJm = new System.Windows.Forms.ComboBox();
            this.CBVat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TBName = new System.Windows.Forms.TextBox();
            this.nUDPrice = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BOk = new System.Windows.Forms.Button();
            this.BCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TBTag = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nUDPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // CBJm
            // 
            this.CBJm.FormattingEnabled = true;
            this.CBJm.Location = new System.Drawing.Point(12, 103);
            this.CBJm.Name = "CBJm";
            this.CBJm.Size = new System.Drawing.Size(135, 21);
            this.CBJm.TabIndex = 0;
            // 
            // CBVat
            // 
            this.CBVat.FormattingEnabled = true;
            this.CBVat.Location = new System.Drawing.Point(12, 182);
            this.CBVat.Name = "CBVat";
            this.CBVat.Size = new System.Drawing.Size(135, 21);
            this.CBVat.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nazwa:";
            // 
            // TBName
            // 
            this.TBName.Location = new System.Drawing.Point(13, 25);
            this.TBName.Name = "TBName";
            this.TBName.Size = new System.Drawing.Size(134, 20);
            this.TBName.TabIndex = 3;
            // 
            // nUDPrice
            // 
            this.nUDPrice.DecimalPlaces = 2;
            this.nUDPrice.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDPrice.Location = new System.Drawing.Point(12, 143);
            this.nUDPrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nUDPrice.Name = "nUDPrice";
            this.nUDPrice.Size = new System.Drawing.Size(135, 20);
            this.nUDPrice.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Jm:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cena:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "VAT:";
            // 
            // BOk
            // 
            this.BOk.Location = new System.Drawing.Point(153, 12);
            this.BOk.Name = "BOk";
            this.BOk.Size = new System.Drawing.Size(71, 39);
            this.BOk.TabIndex = 8;
            this.BOk.Text = "Ok";
            this.BOk.UseVisualStyleBackColor = true;
            this.BOk.Click += new System.EventHandler(this.BOk_Click);
            // 
            // BCancel
            // 
            this.BCancel.Location = new System.Drawing.Point(153, 57);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(71, 39);
            this.BCancel.TabIndex = 9;
            this.BCancel.Text = "Anuluj";
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Wyświetlana:";
            // 
            // textBox1
            // 
            this.TBTag.Location = new System.Drawing.Point(13, 64);
            this.TBTag.Name = "textBox1";
            this.TBTag.Size = new System.Drawing.Size(134, 20);
            this.TBTag.TabIndex = 11;
            // 
            // ServiceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 215);
            this.Controls.Add(this.TBTag);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BCancel);
            this.Controls.Add(this.BOk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nUDPrice);
            this.Controls.Add(this.TBName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBVat);
            this.Controls.Add(this.CBJm);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ServiceWindow";
            this.Text = "Usługa";
            this.Load += new System.EventHandler(this.ServiceWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUDPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CBJm;
        private System.Windows.Forms.ComboBox CBVat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBName;
        private System.Windows.Forms.NumericUpDown nUDPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BOk;
        private System.Windows.Forms.Button BCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TBTag;

    }
}