using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Faktury.Data.Xml;

namespace Faktury.Windows
{
    public partial class LoadBackup : Form
    {
        private BackupManager BackupManager { get; }

        private class BackupNodeSorter : IComparer
        {
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
                return string.CompareOrdinal(x.Text, y.Text);
            }
        }

        public LoadBackup(BackupManager backupManager)
        {
            BackupManager = backupManager;
            InitializeComponent();
            treeView1.TreeViewNodeSorter = new BackupNodeSorter();
        }

        private void LoadBackup_Load(object sender, EventArgs e)
        {
            foreach (var dateDirectory in new DirectoryInfo(BackupManager.BackupPath).GetDirectories())
            {
                TreeNode day = treeView1.Nodes.Add(dateDirectory.Name);
                foreach (var timeDirectory in dateDirectory.GetDirectories())
                {
                    TreeNode time = day.Nodes.Add(timeDirectory.Name.Replace('-',':'));
                    time.Tag = time.FullPath;
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
                if(BackupManager.LoadLocalBackup((string)treeView1.SelectedNode.Tag))
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
