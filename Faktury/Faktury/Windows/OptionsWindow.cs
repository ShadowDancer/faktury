using System;
using System.Windows.Forms;
using Faktury.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class OptionsWindow : DockContent
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        #region LoadingManaging
        private void OptionsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Instance.opcjeToolStripMenuItem.Checked = false;
        }

        public void DataImport_Click(object sender, EventArgs e)
        {
            if (MainForm.Instance.OpenDataFolder.ShowDialog() == DialogResult.OK)
            {
                MainForm.Instance.LoadCompaniesFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.LoadDocumentsFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.LoadServicesFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.ReloadCompanyComboboxesInChildWindows();
            }
        }

        private void DataExport_Click(object sender, EventArgs e)
        {
            if (MainForm.Instance.OpenDataFolder.ShowDialog() == DialogResult.OK)
            {
                MainForm.Instance.SaveCompaniesToFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.SaveDocumentsToFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.SaveServicesToFile(MainForm.Instance.OpenDataFolder.SelectedPath);
            }
        }

        private void DataClean_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno?\r\n", "Wyczyść dane", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                MainForm.Instance.CleanCompanies();
                MainForm.Instance.CleanDocuments();
                MainForm.Instance.CleanServices();
                MainForm.Instance.ReloadCompanyComboboxesInChildWindows();
                MessageBox.Show("Aby przywrócić dane użyj przycisku \"Wczytaj kopię bezpieczeństwa\" w menu opcji.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DocumentsClean_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno?\r\n", "Wyczyść dokumenty", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                MainForm.Instance.CleanDocuments();
                MessageBox.Show("Aby przywrócić dane użyj przycisku \"Wczytaj kopię bezpieczeństwa\" w menu opcji.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void OptionsWindow_Load(object sender, EventArgs e)
        {
            ReloadLists();
        }

        private void ReloadLists()
        {
            LBUnit.Items.Clear();
            LBVat.Items.Clear();
            foreach (var currentString in MainForm.Instance.Settings.PropertiesVat)
            {
                LBVat.Items.Add(currentString);
            }
            foreach (var currentString in MainForm.Instance.Settings.PropertiesUnit)
            {
                LBUnit.Items.Add(currentString);
            }
        }


        private void LoadBackup_Click(object sender, EventArgs e)
        {
            new LoadBackup().ShowDialog();
        }

        private void BAddVat_Click(object sender, EventArgs e)
        {
            if (TBInput.Text.Length > 0)
            {
                if (MainForm.Instance.Settings.PropertiesVat.Find(n => n.Equals(TBInput.Text)) != null)
                {
                    MessageBox.Show("Zbiór już zaweta tą wartość!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MainForm.Instance.Settings.PropertiesVat.Add(TBInput.Text);
                    ReloadLists();
                }
            }
            else MessageBox.Show("Wprowadź nową wartość do kontrolki!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BAddUnit_Click(object sender, EventArgs e)
        {
            if (TBInput.Text.Length > 0)
            {
                if (MainForm.Instance.Settings.PropertiesUnit.Find(n => n.Equals(TBInput.Text)) != null)
                {
                    MessageBox.Show("Zbiór już zaweta tą wartość!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MainForm.Instance.Settings.PropertiesUnit.Add(TBInput.Text);
                    ReloadLists();
                }
            }
            else MessageBox.Show("Wprowadź nową wartość do kontrolki!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BDelVat_Click(object sender, EventArgs e)
        {
            if (LBVat.SelectedItem != null)
            {
                if (MainForm.Instance.Settings.PropertiesVat.Find(n => n.Equals(LBVat.Text)) != null)
                {
                    MainForm.Instance.Settings.PropertiesVat.Remove(LBVat.Text);
                    ReloadLists();
                }
                else
                {
                    MessageBox.Show("Zbiór nie zawiera wartości!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else MessageBox.Show("Wybierz z listy wartość do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BDelUnit_Click(object sender, EventArgs e)
        {
            if (LBUnit.SelectedItem != null)
            {
                if (MainForm.Instance.Settings.PropertiesUnit.Find(n => n.Equals(LBUnit.Text)) != null)
                {
                    MainForm.Instance.Settings.PropertiesUnit.Remove(LBUnit.Text);
                    ReloadLists();
                }
                else
                {
                    MessageBox.Show("Zbiór nie zawiera wartości!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else MessageBox.Show("Wybierz z listy wartość do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void BSetOwnerData_Click(object sender, EventArgs e)
        {
            if (MainForm.Instance.Settings.OwnerCompany == null) MainForm.Instance.Settings.OwnerCompany = new Company();
            CompanyWindow dialog = new CompanyWindow();
            MainForm.Instance.Settings.OwnerCompany.Bank = true;
            dialog.Company = (MainForm.Instance.Settings.OwnerCompany);

            dialog.AddToCollection = false;
            dialog.ShowDialog(); 
        }

        private void BBackupSettings_Click(object sender, EventArgs e)
        {
            new BackupSettings().ShowDialog();
        }
        
        private void OptionsReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Na pewno?", "Przywróć ustawienia domyślne", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                MainForm.Instance.Settings = new EditorSettings();
                MainForm.Instance.RunFirstUseWizard();
            }   

        }

        private void OptionsExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MainForm.Instance.SaveSettingsToFile(saveFileDialog1.FileName);
            }
        }

        private void OptionsImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MainForm.Instance.LoadSettingsFromFile(openFileDialog1.FileName);
            }
        }
    }
}