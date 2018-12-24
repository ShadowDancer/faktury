using System.ComponentModel;
using System.Windows.Forms;

namespace Faktury.Windows
{
    partial class LoadBackup
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.BLoad = new System.Windows.Forms.Button();
            this.BCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(623, 319);
            this.treeView1.TabIndex = 0;
            // 
            // BLoad
            // 
            this.BLoad.Location = new System.Drawing.Point(483, 341);
            this.BLoad.Name = "BLoad";
            this.BLoad.Size = new System.Drawing.Size(73, 30);
            this.BLoad.TabIndex = 1;
            this.BLoad.Text = "Wczytaj";
            this.BLoad.UseVisualStyleBackColor = true;
            this.BLoad.Click += new System.EventHandler(this.BLoad_Click);
            // 
            // BCancel
            // 
            this.BCancel.Location = new System.Drawing.Point(562, 341);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(73, 30);
            this.BCancel.TabIndex = 2;
            this.BCancel.Text = "Zamknij";
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // LoadBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 383);
            this.Controls.Add(this.BCancel);
            this.Controls.Add(this.BLoad);
            this.Controls.Add(this.treeView1);
            this.Name = "LoadBackup";
            this.Text = "Wczytaj kopię zapasową";
            this.Load += new System.EventHandler(this.LoadBackup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TreeView treeView1;
        private Button BLoad;
        private Button BCancel;
    }
}