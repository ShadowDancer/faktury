using System;
using System.Windows.Forms;
using Faktury.Classes;
using Faktury.Data.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class OptionsWindow : DockContent
    {
        private readonly SettingsAccessor _settingsAccessor;
        private readonly ModelStoreLoader _storeLoader;
        private ModelStore ModelStore { get; }
        private BackupManager BackupManager { get; }

        public OptionsWindow(ModelStore modelStore, BackupManager backupManager, SettingsAccessor settingsAccessor, ModelStoreLoader storeLoader)
        {
            _settingsAccessor = settingsAccessor;
            _storeLoader = storeLoader;
            ModelStore = modelStore;
            BackupManager = backupManager;
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
                _storeLoader.LoadDataFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.ReloadCompanyComboboxesInChildWindows();
            }
        }

        private void DataExport_Click(object sender, EventArgs e)
        {
            if (MainForm.Instance.OpenDataFolder.ShowDialog() == DialogResult.OK)
            {
                _storeLoader.SaveDataToDirectory(MainForm.Instance.OpenDataFolder.SelectedPath);
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

        #endregion

        private void OptionsWindow_Load(object sender, EventArgs e)
        {
            ReloadLists();
        }

        private void ReloadLists()
        {
            LBUnit.Items.Clear();
            LBVat.Items.Clear();
            var editorSettings = _settingsAccessor.GetSettings();
            foreach (var currentString in editorSettings.Properties_Vat)
            {
                LBVat.Items.Add(currentString);
            }
            foreach (var currentString in editorSettings.Properties_Unit)
            {
                LBUnit.Items.Add(currentString);
            }
        }


        private void LoadBackup_Click(object sender, EventArgs e)
        {
            new LoadBackup(BackupManager).ShowDialog();
        }

        private void BAddVat_Click(object sender, EventArgs e)
        {
            if (TBInput.Text.Length > 0)
            {
                var editorSettings = _settingsAccessor.GetSettings();
                if (editorSettings.Properties_Vat.Find(n => n.Equals(TBInput.Text)) != null)
                {
                    MessageBox.Show("Zbiór już zaweta tą wartość!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    editorSettings.Properties_Vat.Add(TBInput.Text);
                    ReloadLists();
                }
            }
            else MessageBox.Show("Wprowadź nową wartość do kontrolki!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BAddUnit_Click(object sender, EventArgs e)
        {
            if (TBInput.Text.Length > 0)
            {
                var editorSettings = _settingsAccessor.GetSettings();
                if (editorSettings.Properties_Unit.Find(n => n.Equals(TBInput.Text)) != null)
                {
                    MessageBox.Show("Zbiór już zaweta tą wartość!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    editorSettings.Properties_Unit.Add(TBInput.Text);
                    ReloadLists();
                }
            }
            else MessageBox.Show("Wprowadź nową wartość do kontrolki!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BDelVat_Click(object sender, EventArgs e)
        {
            if (LBVat.SelectedItem != null)
            {
                var editorSettings = _settingsAccessor.GetSettings();
                if (editorSettings.Properties_Vat.Find(n => n.Equals(LBVat.Text)) != null)
                {
                    editorSettings.Properties_Vat.Remove(LBVat.Text);
                    ReloadLists();
                }
                else
                {
                    MessageBox.Show("Zbiór nie zawiera wartości!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Wybierz z listy wartość do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BDelUnit_Click(object sender, EventArgs e)
        {
            if (LBUnit.SelectedItem != null)
            {
                var editorSettings = _settingsAccessor.GetSettings();
                if (editorSettings.Properties_Unit.Find(n => n.Equals(LBUnit.Text)) != null)
                {
                    editorSettings.Properties_Unit.Remove(LBUnit.Text);
                    ReloadLists();
                }
                else
                {
                    MessageBox.Show("Zbiór nie zawiera wartości!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var editorSettings = _settingsAccessor.GetSettings();
            if (editorSettings.OwnerCompany == null) editorSettings.OwnerCompany = new Company();
            CompanyWindow dialog = new CompanyWindow(ModelStore);
            editorSettings.OwnerCompany.Bank = true;
            dialog.Company = (editorSettings.OwnerCompany);

            dialog.AddToCollection = false;
            dialog.ShowDialog(); 
        }

        private void BBackupSettings_Click(object sender, EventArgs e)
        {
            new BackupSettings(_settingsAccessor).ShowDialog();
        }
        
        private void OptionsReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Na pewno?", "Przywróć ustawienia domyślne", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _settingsAccessor.SetSettings(new EditorSettings());
                MainForm.Instance.RunFirstUseWizard();
            }   

        }

        private void OptionsExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _storeLoader.SaveSettingsToFile(saveFileDialog1.FileName);
            }
        }

        private void OptionsImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _storeLoader.LoadSettingsFromFile(openFileDialog1.FileName);
            }
        }
    }
}