﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Faktury.Windows
{
    public partial class LoadBackup : Form
    {
        public class BackupNodeSorter : System.Collections.IComparer
        {
            // Compare the length of the strings, or the strings
            // themselves, if they are the same length.
            public int Compare(object obj1, object obj2)
            {
                TreeNode x = (TreeNode)obj1;
                TreeNode y = (TreeNode)obj2;

                try
                {
                       return Convert.ToDateTime(x.Text).CompareTo(Convert.ToDateTime(y.Text));
                }
                catch
                {
                    
                }
                return string.Compare(x.Text, y.Text);
            }
        }

        public LoadBackup()
        {
            InitializeComponent();
            treeView1.TreeViewNodeSorter = new BackupNodeSorter();
        }

        private void LoadBackup_Load(object sender, EventArgs e)
        {
            foreach (var DateDirectory in new DirectoryInfo(MainForm.Instance.BackupPath).GetDirectories())
            {
                TreeNode Day = treeView1.Nodes.Add(DateDirectory.Name);
                foreach (var TimeDirectory in DateDirectory.GetDirectories())
                {
                    TreeNode Time = Day.Nodes.Add(TimeDirectory.Name.Replace('-',':'));
                    Time.Tag = Time.FullPath;
                }
            }
            treeView1.Sort();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BLoad_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null)
            {
                if(MainForm.Instance.BackupManager.LoadLocalBackup((string)treeView1.SelectedNode.Tag))
                    MessageBox.Show("Poprawnie wczytano kopię zapasową!", "Wczytywanie kopii zapasowej", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Archiwum jest uszkodzone lub nie istnieje!", "Wczytywanie kopii zapasowej", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Wybierz poprawną kopię bezpieczeństwa!", "Wczytywanie kopii bezpieczeństwa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

    }
}