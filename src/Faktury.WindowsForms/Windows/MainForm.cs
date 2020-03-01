using Faktury.Data.Xml;
using Faktury.Print_Framework;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Faktury.Domain.Classes;
using Faktury.Domain.Data;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class MainForm : Form
    {
        private int _childFormNumber;

        public MainForm(string applicationDirectory)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            Instance = this;

            ModelStore = new ModelStore();
            ModelStoreLoader = new ModelStoreLoader(SettingsAccessor, ModelStore, applicationDirectory);
            BackupManager = new BackupManager(SettingsAccessor, ModelStoreLoader,
                Path.Combine(applicationDirectory, "Backup"));

            if (!ModelStoreLoader.ConfigExists())
            {
                RunFirstUseWizard();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #region Properties

        //singleton
        public static MainForm Instance;

        //data

        private BackupManager BackupManager { get; }

        private ModelStore ModelStore { get; }

        private SettingsAccessor SettingsAccessor { get; } = new SettingsAccessor();

        private ModelStoreLoader ModelStoreLoader { get; }

        //printing
        private PrintEngine PrintEngine { get; } = new PrintEngine();

        #region Actions

        public void EditCompany(Company companyToEdit)
        {
            var childForm = new CompanyWindow(ModelStore);
            var editorSettings = SettingsAccessor.GetSettings();
            if (companyToEdit == editorSettings.OwnerCompany &&
                editorSettings.OwnerCompany != null)
            {
                childForm.AddToCollection = false;
            }
            else
            {
                childForm.AddToCollection = true;
            }

            childForm.MdiParent = this;

            childForm.Text = _childFormNumber++ + ": Edycja " + companyToEdit.ShortName;
            childForm.Company = companyToEdit;
            childForm.Show(MainDockPanel);
        }

        public void DeleteService(Service serviceToRemove)
        {
            if (MessageBox.Show("Na pewno?", "Usuwanie usługi...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                ModelStore.ServiceRepository.RemoveService(serviceToRemove);
            }
        }

        public void DeleteCompany(Company companyToRemove)
        {
            if (MessageBox.Show("Na pewno?", "Usuwanie firmy...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                ModelStore.CompanyRepository.RemoveCompany(companyToRemove);
                ReloadCompanyComboboxesInChildWindows();
            }
        }

        public void ReloadCompanyCombobox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            foreach (var currentCompany in ModelStore.CompanyRepository.Companies.OrderBy(n => n.ShortNameStripped))
            {
                comboBox.Items.Add(new ComboBoxItem(currentCompany.ShortName, currentCompany.Id));
            }

            if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
        }

        public void ReloadCompanyComboboxesInChildWindows()
        {
            _documentListWindow?.UpdateCompanyCombobox();

            //update w child window
            foreach (var currentChild in MdiChildren)
            {
                if (currentChild is DocumentWindow window)
                {
                    window.ReloadCompanyCombobox();
                }
            }
        }

        public void AddService()
        {
            var childForm = new ServiceWindow(ModelStore, SettingsAccessor)
            {
                MdiParent = this,
                Text = _childFormNumber++ + ": Nowa usługa"
            };


            childForm.Show(MainDockPanel);
        }

        public void EditService(Service serviceToEdit)
        {
            var childForm = new ServiceWindow(ModelStore, SettingsAccessor)
            {
                MdiParent = this,
                Text = _childFormNumber++ + ": Edycja " + serviceToEdit.Name,
                Service = serviceToEdit
            };

            childForm.Show(MainDockPanel);
        }

        public void AddCompany()
        {
            var childForm = new CompanyWindow(ModelStore)
            {
                MdiParent = this,
                Text = _childFormNumber++ + ": Nowa firma"
            };

            childForm.Show(MainDockPanel);
        }

        public void OpenDocument(Document document)
        {
            if (document != null)
            {
                var childForm = new DocumentWindow(ModelStore, PrintEngine, SettingsAccessor)
                {
                    MdiParent = this,
                    Text = document.Number + "//" + document.Year,
                    Document = document
                };

                childForm.Show(MainDockPanel);
            }
            else
            {
                throw new Exception();
            }
        }

        public void RemoveAllData()
        {
            CleanCompanies();
            CleanDocuments();
            CleanServices();
            ReloadCompanyComboboxesInChildWindows();
        }

        private void CleanCompanies()
        {
            foreach (var childForm in MdiChildren)
            {
                if (childForm is CompanyWindow)
                {
                    childForm.Close();
                }
            }

            ModelStore.CompanyRepository.ClearCompanies();
        }

        private void CleanServices()
        {
            foreach (var childForm in MdiChildren)
            {
                if (childForm is ServiceWindow)
                {
                    childForm.Close();
                }
            }

            ModelStore.ServiceRepository.ClearServices();
        }

        private void CleanDocuments()
        {
            foreach (var childForm in MdiChildren)
            {
                if (childForm is DocumentWindow window)
                {
                    window.ForceClose = true;
                    window.Close();
                }
            }

            ModelStore.DocumentRepository.Documents.Clear();
        }

        public bool SaveDocument(DocumentWindow window)
        {
            var document = window.Document;

            try
            {
                var check = ModelStore.DocumentRepository.Documents.Find(n =>
                    n.Number == window.nUDNumber.Value && n.Year == window.nUDYear.Value);
                if (check == null)
                {
                    if (ModelStore.DocumentRepository.Documents.Find(n => n == window.Document) == null)
                        ModelStore.DocumentRepository.Documents.Add(document);
                }
                else
                {
                    if (check != window.Document)
                    {
                        throw new Exception("Dokument o danym numerze już istnieje!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //save document if no problems
            window.SaveDataFromControls(document);
            ModelStore.DocumentRepository.UpdateHighestDocumentId();

            return true;
        }

        #endregion


        #region Windows

        //options
        private OptionsWindow _optionsWindow;

        //document list
        private DocumentListWindow _documentListWindow;

        //company list
        private CompanyListWindow _companyListWindow;

        //Services list
        public ServicesListWindow ServicesListWindow;

        #endregion

        #region Functions

        #endregion

        private bool CanEndApplication()
        {
            return MdiChildren.Length <= 0;
        }

        public void RunFirstUseWizard()
        {
            if (MessageBox.Show(
                    "Program faktury został uruchomiony po raz pierwszy. Kreator pierwszego uruchomienia przeprowadzi Cię przez proces konfigurowania aplikacji. Kontynuować?",
                    "Kreator pierwszego uruchomienia", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
                DialogResult.Yes)
            {
                //Import data
                if (MessageBox.Show(
                        "Chcesz zaimportować dane?\nWybierz nie, jeśli nie posiadasz danych stworzonych przez tą aplikację.",
                        "Kreator pierwszego uruchomienia", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _optionsWindow.DataImport_Click(null, null);
                }

                //Owner data
                MessageBox.Show(
                    "Dane wystawiającego są to dane osoby, na którą wystawiane będą faktury. Dane te można zmienić w menu opcje.",
                    "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new OptionsWindow(ModelStore, BackupManager, SettingsAccessor, ModelStoreLoader).BSetOwnerData_Click(
                    null, null);

                //Backup
                MessageBox.Show(
                    "Kopia zapasowa, to mechanizm, który zabezpicza Cię przed utratą danych, na wskutek awarii sprzętu lub systemu. Dostępne są dwie metody tworzenia kopii - lokalna (na dysku twardym) i na zewnętrznym nośniku/komputerze(zabezpiecza ona dane w przypadku awarii dysku).",
                    "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new BackupSettings(SettingsAccessor).ShowDialog();
                if (SettingsAccessor.GetSettings().DeviceBackup)
                {
                    MessageBox.Show(
                        "Włączono kopię na zewnętrznym nośniku - po upłynięciu wyznaczonego okresu zostanie wyświetlona proźba o włożenie nośinka.",
                        "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Finish
                MessageBox.Show(
                    "Dziękuję za wybranie aplikacji Faktury. Naciśnij OK, aby zakończyć działanie kreatora.",
                    "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region EventHandlesrs

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            ModelStoreLoader.LoadEverythingFromDirectory();
            ReloadCompanyComboboxesInChildWindows();

            OpenDocumentList(null, null);
            OpenCompaniesWindow();

            _companyListWindow.Hide();
            _companyListWindow.Show(MainDockPanel, DockState.DockRight);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Zapisać dane firm i dokumentów?", "Zamykanie...", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                BackupManager.ExitingApplication = true;
                ModelStoreLoader.SaveEverythingToDirectoryWithBackup(BackupManager);
                BackupManager.ExitingApplication = false;
            }
        }

        #region View

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionsToolStripMenuItem.Checked)
            {
                _optionsWindow = new OptionsWindow(ModelStore, BackupManager, SettingsAccessor, ModelStoreLoader);
                _optionsWindow.Show(MainDockPanel);
            }
            else
            {
                _optionsWindow.Close();
            }
        }

        private void documentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (documentListToolStripMenuItem.Checked)
            {
                _documentListWindow = new DocumentListWindow(ModelStore, PrintEngine, SettingsAccessor);
                _documentListWindow.Show(MainDockPanel);
                documentListToolStripMenuItem.Checked = true;
            }
            else
            {
                _documentListWindow.Close();
            }
        }

        private void companiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (companiesToolStripMenuItem.Checked)
            {
                OpenCompaniesWindow();
            }
            else
            {
                _companyListWindow.Close();
            }
        }

        private void OpenCompaniesWindow()
        {
            _companyListWindow = new CompanyListWindow(ModelStore);
            _companyListWindow.Show(MainDockPanel);
            companiesToolStripMenuItem.Checked = true;
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servicesToolStripMenuItem.Checked)
            {
                ServicesListWindow = new ServicesListWindow(ModelStore);
                ServicesListWindow.Show(MainDockPanel);
            }
            else
            {
                ServicesListWindow.Close();
            }
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        #endregion

        #endregion

        #region Windows

        private void closeCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveMdiChild?.Close();
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        #region File

        public void NewDocument(object sender, EventArgs e)
        {
            var childForm = new DocumentWindow(ModelStore, PrintEngine, SettingsAccessor)
            { MdiParent = this, Text = "Nowy Dokument" };


            _childFormNumber++;
            if (_childFormNumber > 1)
            {
                childForm.Text += " (" + _childFormNumber + ")";
            }

            childForm.Show(MainDockPanel);
        }

        private void OpenDocumentList(object sender, EventArgs e)
        {
            if (_documentListWindow != null && !_documentListWindow.IsDisposed)
            {
                _documentListWindow.Activate();
            }
            else
            {
                documentListToolStripMenuItem.Checked = true;
                documentListToolStripMenuItem_Click(null, null);
            }
        }

        private void openCompanyToolStripButton_Click(object sender, EventArgs e)
        {
            if (_companyListWindow != null && !_companyListWindow.IsDisposed)
            {
                _companyListWindow.Activate();
            }
            else
            {
                companiesToolStripMenuItem.Checked = true;
                companiesToolStripMenuItem_Click(null, null);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is DocumentWindow window)
            {
                SaveDocument(window);
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var currentChild in MdiChildren)
            {
                currentChild.Close();
            }

            if (CanEndApplication()) Close();
        }

        private void newCompanyToolStripButton_Click(object sender, EventArgs e)
        {
            AddCompany();
        }

        #endregion

        #region Printing

        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintEngine.ShowPageSettings();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is DocumentListWindow listWindow)
            {
                listWindow.ShowPreview();
            }

            if (ActiveMdiChild != null && ActiveMdiChild is DocumentWindow window)
            {
                window.ShowPreview();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is DocumentWindow window)
            {
                SaveDocument(window);
                PrintEngine.AddPrintObject(new DocumentPrinter(window.Document));
            }

            PrintEngine.ShowPrintDialog();
            PrintEngine.ClearPrintingObjects();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is DocumentListWindow listWindow)
            {
                listWindow.Print();
            }

            if (ActiveMdiChild != null && ActiveMdiChild is DocumentWindow window)
            {
                window.Print();
            }
        }

        #endregion
    }
}