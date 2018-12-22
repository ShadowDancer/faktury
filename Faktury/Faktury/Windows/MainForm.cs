using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Faktury.Classes;
using Faktury.Data.Xml;


namespace Faktury.Windows
{
    public partial class MainForm : Form
    {
        private int _childFormNumber;

        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Instance = this;

            DataPath = Path.Combine(AppDataDirectoryPath, "Data");
            BackupPath = Path.Combine(AppDataDirectoryPath, "Backup");
        }

        #region Properties
            //singleton
            public static MainForm Instance;



            //data
            public string AppDataDirectoryPath = Path.Combine(CommonDocuments.GetCommonDocumentsFolder(), "Faktury");
            public string DataPath;
            public string BackupPath;

            public EditorSettings Settings;
            public BackupManager BackupManager = new BackupManager();

            public List<Company> Companies = new List<Company>();
            public List<Document> Documents = new List<Document>();
            public List<Service> Services = new List<Service>();

            public Dictionary<int, int> HigestDocumentId = new Dictionary<int, int>();//get higest ID for year
            public int HigestCompanyId;
            public int GetNewCompanyId { get { return ++HigestCompanyId; } }
            public int HigestServiceId;
            public int GetNewServiceId { get { return ++HigestServiceId; } }

            #region Windows
                //options
                public OptionsWindow OptionsWindow;

                //document list
                public DocumentListWindow DocumentListWindow;

                //company list
                public CompanyListWindow CompanyListWindow;

                //Services list
                public ServicesListWindow ServicesListWindow;

            #endregion

            //printing
            public Print_Framework.PrintEngine PrintEngine = new Print_Framework.PrintEngine(); 

        #endregion

        #region Functions

        public void UpdateHigestDocumentId()
        {
            foreach(var currentDocument in Documents)
            {
                if (HigestDocumentId.ContainsKey(currentDocument.Year))
                {
                    if (HigestDocumentId[currentDocument.Year] < currentDocument.Number)
                    {
                        HigestDocumentId[currentDocument.Year] = currentDocument.Number;
                    }
                }
                else 
                {
                    if (currentDocument.Number > 0) HigestDocumentId.Add(currentDocument.Year, currentDocument.Number);
                    else HigestDocumentId.Add(currentDocument.Year, 1);
                }
            }
        }

        public void UpdateHigestCompanyId()
        {
            foreach (Company currentComapny in Companies)
            {
                if (currentComapny.Id > HigestCompanyId) HigestCompanyId = currentComapny.Id;
            }
        }

        public void UpdateHigestServiceId()
        {
            foreach (Service currentService in Services)
            {
                if (currentService.Id > HigestServiceId) HigestServiceId = currentService.Id;
            }
        }

        public void ReloadCompanyCombobox(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            foreach (Company currentCompany in Companies)
            {
                comboBox.Items.Add(new ComboBoxItem(currentCompany.Tag, currentCompany.Id));
            }
            if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
        }

        public void ReloadCompanyComboboxesInChildWindows()
        {
            if (DocumentListWindow != null)
            {
                DocumentListWindow.UpdateCompanyCombobox();
            }

            //update w child window
            foreach (var currentChild in MdiChildren)
            {
                if (currentChild is DocumentWindow)
                {
                    ((DocumentWindow)currentChild).ReloadCompanyCombobox();
                }
            }
        }

        #region Services
        public void AddService()
        {
            ServiceWindow childForm = new ServiceWindow();

            childForm.MdiParent = this;

            childForm.Text = _childFormNumber++.ToString() + ": Nowa usługa";
            childForm.Show(MainDockPanel);
        }

        public void EditService(Service serviceToEdit)
        {
            ServiceWindow childForm = new ServiceWindow();
            childForm.MdiParent = this;

            childForm.Text = _childFormNumber++.ToString() + ": Edycja " + serviceToEdit.Name;
            childForm.Service = serviceToEdit;
            childForm.Show(MainDockPanel);            
        }

        public void DeleteService(Service serviceToRemove)
        {
            if (MessageBox.Show("Na pewno?", "Usuwanie usługi...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Services.Remove(serviceToRemove);
            }
        }

        public void CleanServices()
        {
            foreach (var childForm in MdiChildren)
            {
                if (childForm is ServiceWindow)
                {
                    childForm.Close();
                }
            }
            Services.Clear();
        }

        #endregion

        #region Companies
        public void AddCompany()
        {
            CompanyWindow childForm = new CompanyWindow();
          
            childForm.MdiParent = this;

            childForm.Text = _childFormNumber++.ToString() + ": Nowa firma";
            childForm.Show(MainDockPanel);
        }

        public void EditCompany(Company companyToEdit)
        {
                CompanyWindow childForm = new CompanyWindow();
                if (companyToEdit == Settings.OwnerCompany && Settings.OwnerCompany != null) 
                    childForm.AddToCollection = false;
                else childForm.AddToCollection = true;

                childForm.MdiParent = this;

                childForm.Text = _childFormNumber++.ToString() + ": Edycja " + companyToEdit.Tag;
                childForm.Company = companyToEdit;
                childForm.Show(MainDockPanel);
        }

        public void DeleteCompany(Company companyToRemove)
        {
                if (MessageBox.Show("Na pewno?", "Usuwanie firmy...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                        Companies.Remove(companyToRemove);
                        ReloadCompanyComboboxesInChildWindows();
                }
        }

        public void CleanCompanies()
        {
            foreach (var childForm in MdiChildren)
            {
                if (childForm is CompanyWindow)
                {
                    childForm.Close();
                }
            }
            Companies.Clear();
        }

        #endregion

        #region Documents

        public void CleanDocuments()
        {
            foreach (var childForm in MdiChildren)
            {
                if (childForm is DocumentWindow)
                {
                    //TODO
                    ((DocumentWindow)childForm).ForceClose = true;
                    childForm.Close();
                }
            }
            Documents.Clear();
        }

        public void OpenDocument(Document document)
        {
            if (document != null)
            {

                DocumentWindow childForm = new DocumentWindow();
                childForm.MdiParent = this;

                childForm.Text = document.Number.ToString() + "//" + document.Year.ToString() + " " + document.Name;
                childForm.Document = document;
                childForm.Show(MainDockPanel);
            }
            else throw new Exception();
        }

        public bool SaveDocument(DocumentWindow window)
        {
            Document document = window.Document;

            try
            {
                Document check = Documents.Find(n => (n.Number == window.nUDNumber.Value && n.Year == window.nUDYear.Value ));
                if (check == null)
                {
                    if(Documents.Find(n => n == window.Document) == null)
                    Documents.Add(document);
                }
                else
                {
                    if(check != window.Document)
                    throw new Exception("Dokument o danym numerze już istnieje!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //save document if no problems
            window.SaveDataFromControls(document);
            UpdateHigestDocumentId();

            return true;
        }

        public Document FindDocument(int number, int year)
        {
            return Documents.Find(n => (n.Number == number && n.Year == year));
        }

        #endregion
        public bool EndApplication()
            {
                if (MdiChildren.Length > 0) return false;
                else return true;
            }

        public void RunFirstUseWizard()
        {
            if (MessageBox.Show("Program faktury został uruchomiony po raz pierwszy. Kreator pierwszego uruchomienia przeprowadzi Cię przez proces konfigurowania aplikacji. Kontynuować?", "Kreator pierwszego uruchomienia", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //Import data
                if (MessageBox.Show("Chcesz zaimportować dane?\nWybierz nie, jeśli nie posiadasz danych stworzonych przez tą aplikację.", "Kreator pierwszego uruchomienia", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    OptionsWindow.DataImport_Click(null, null);
                }

                //Owner data
                MessageBox.Show("Dane wystawiającego są to dane osoby, na którą wystawiane będą faktury. Dane te można zmienić w menu opcje.", "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new OptionsWindow().BSetOwnerData_Click(null, null);

                //Backup
                MessageBox.Show("Kopia zapasowa, to mechanizm, który zabezpicza Cię przed utratą danych, na wskutek awarii sprzętu lub systemu. Dostępne są dwie metody tworzenia kopii - lokalna (na dysku twardym) i na zewnętrznym nośniku/komputerze(zabezpiecza ona dane w przypadku awarii dysku).", "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new BackupSettings().ShowDialog();
                if (Settings.DeviceBackup)
                    MessageBox.Show("Włączono kopię na zewnętrznym nośniku - po upłynięciu wyznaczonego okresu zostanie wyświetlona proźba o włożenie nośinka.", "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //Finish
                MessageBox.Show("Dziękuję za wybranie aplikacji Faktury. Naciśnij OK, aby zakończyć działanie kreatora.", "Kreator pierwszego uruchomienia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Loading/Saving Data



            public void LoadFromDirectory()
            {
                LoadSettingsFromFile(Path.Combine(AppDataDirectoryPath, "Config.xml"));

                LoadCompaniesFromFile(DataPath);
                LoadDocumentsFromFile(DataPath);
                LoadServicesFromFile(DataPath);

                ReloadCompanyComboboxesInChildWindows();
            }
            #region Loading
            public void LoadSettingsFromFile(string configFilePath)
                {
                    Settings = new EditorSettings();
                    if (File.Exists(configFilePath))
                    {
                        using (StreamReader reader = new StreamReader(configFilePath))
                        {
                            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(EditorSettings));
                            Settings = (EditorSettings)serializer.Deserialize(reader);
                        }
                    }
                    else
                    {
                        RunFirstUseWizard();
                    }
                }
                public void LoadCompaniesFromFile(string filepath)
                {
                    if (filepath.Length == 0) return;
                    try
                    {
                        filepath = Path.Combine(filepath, "Companies.xml");

                        XmlDocument doc = new XmlDocument();
                        doc.Load(filepath);

                        foreach (XmlNode currentNode in doc["Companies"])
                        {
                            if (currentNode.Name == "IssueCompany") continue;

                            Company newCompany = CompanyToXmlSerializer.GetCompanyFromXml(currentNode);
                            Company check = new Company();
                            check = Companies.Find(n => n.Id == newCompany.Id);
                            if (check == null)
                            {
                                Companies.Add(newCompany);
                            }
                            else MessageBox.Show("Kolekcja zawiera już element o ID " + newCompany.Id.ToString() + "!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Instance.UpdateHigestCompanyId();
                    }
                    catch
                    {
                        MessageBox.Show("Nie można wczytać pliku z kontrahentami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                public void LoadDocumentsFromFile(string filepath)
                {
                    if (filepath.Length == 0) return;
                    try
                    {
                        filepath = Path.Combine(filepath, "Documents.xml");

                        XmlDocument doc = new XmlDocument();
                        doc.Load(filepath);
                        foreach (XmlNode currentNode in doc["Documents"])
                        {
                            Document newDocument = DocumentXmlSerializer.GetDocumentFromXml(currentNode);
                                Documents.Add(newDocument);
                        }
                        UpdateHigestDocumentId();
                    }
                    catch
                    {
                        MessageBox.Show("Nie można wczytać pliku z dokumentami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                public void LoadServicesFromFile(string filepath)
                {
                    if (filepath.Length == 0) return;
                    try
                    {
                        filepath = Path.Combine(filepath, "Services.xml");

                        XmlDocument doc = new XmlDocument();
                        doc.Load(filepath);
                        foreach (XmlNode currentNode in doc["Services"])
                        {
                            Services.Add(ServiceToXmlSerializer.GetServiceFromXml(currentNode));
                        }
                        Instance.UpdateHigestServiceId();
                    }
                    catch
                    {
                        MessageBox.Show("Nie można wczytać pliku z usługami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            #endregion

            public void SaveToDirectory()
            {
                try
                {

                    if (!Directory.Exists(AppDataDirectoryPath)) Directory.CreateDirectory(AppDataDirectoryPath);
                    if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);

                    SaveCompaniesToFile(DataPath);
                    SaveDocumentsToFile(DataPath);
                    SaveServicesToFile(DataPath);
                    BackupManager.SaveLocalBackup();
                    BackupManager.SaveDeviceBackup();
                    SaveSettingsToFile(Path.Combine(AppDataDirectoryPath, "Config.xml"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas zapisywania plików:\n" + ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            #region Saving
                public void SaveSettingsToFile(string configFilePath)
                {
                    using(StreamWriter writer = new StreamWriter(configFilePath))
                    {
                        
                        System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                        ns.Add("", "");

                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(EditorSettings));
                        serializer.Serialize(writer, Settings, ns) ;
                    }
                }

                public void SaveCompaniesToFile(string filepath)
                {
                    filepath = Path.Combine(filepath, "Companies.xml");

                    XmlDocument doc = new XmlDocument();

                    XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

                    doc.LoadXml("<Companies></Companies>");
                    doc.InsertBefore(xmlHeader, doc.DocumentElement); 


                    foreach (var currentCompany in Companies)
                    {
                        doc["Companies"].AppendChild(CompanyToXmlSerializer.GetXmlElement(currentCompany, doc));
                    }

                    doc.Save(filepath);
                }
                public void SaveDocumentsToFile(string filepath)
                {
                    filepath = Path.Combine(filepath, "Documents.xml");

                    XmlDocument doc = new XmlDocument();

                    XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

                    doc.LoadXml("<Documents></Documents>");
                    doc.InsertBefore(xmlHeader, doc.DocumentElement); 


                    foreach (var currentDocument in Documents)
                    {
                        doc["Documents"].AppendChild(DocumentXmlSerializer.GetXmlElement(currentDocument, doc));
                    }

                    doc.Save(filepath);
                }
                public void SaveServicesToFile(string filepath)
                {
                    filepath = Path.Combine(filepath, "Services.xml");

                    XmlDocument doc = new XmlDocument();

                    XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

                    doc.LoadXml("<Services></Services>");
                    doc.InsertBefore(xmlHeader, doc.DocumentElement);


                    foreach (var currentService in Services)
                    {
                        doc["Services"].AppendChild(ServiceToXmlSerializer.GetXmlElement(currentService, doc));
                    }

                    doc.Save(filepath);
                }
            #endregion
        #endregion

        #endregion

        #region EventHandlesrs

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            LoadFromDirectory();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(MessageBox.Show("Zapisać dane firm i dokumentów?", "Zamykanie...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                BackupManager.ExitingApplication = true;
                SaveToDirectory();
                BackupManager.ExitingApplication = false;
            }
        }

        //Widok
        #region View

        private void opcjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opcjeToolStripMenuItem.Checked == true)
            {
                OptionsWindow = new OptionsWindow();
                OptionsWindow.Show(MainDockPanel);
            }
            else
                OptionsWindow.Close();
        }

        public void dokumentyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dokumentyToolStripMenuItem.Checked == true)
            {
                    DocumentListWindow = new DocumentListWindow();
                    DocumentListWindow.Show(MainDockPanel);
            }
            else
                DocumentListWindow.Close();
        }

        private void kontrahenciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kontrahenciToolStripMenuItem.Checked == true)
            {
                CompanyListWindow = new CompanyListWindow();
                CompanyListWindow.Show(MainDockPanel);
            }
            else
                CompanyListWindow.Close();
        }

        private void usługiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usługiToolStripMenuItem.Checked == true)
            {
                ServicesListWindow = new ServicesListWindow();
                ServicesListWindow.Show(MainDockPanel);
            }
            else
                ServicesListWindow.Close();
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

        //Okna
        #region Windows

        private void closeCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        //Plik
        #region File

        public void NewDocument(object sender, EventArgs e)
        {
            DocumentWindow childForm = new DocumentWindow();
            childForm.MdiParent = this;


            childForm.Text = "Nowy Dokument";
            _childFormNumber++;
            if (_childFormNumber > 1)
                childForm.Text += " (" + _childFormNumber.ToString() + ")";

            childForm.Show(MainDockPanel);
        }

        private void OpenDocumentList(object sender, EventArgs e)
        {
            if (DocumentListWindow != null && !DocumentListWindow.IsDisposed)
            {
                DocumentListWindow.Activate();
            }
            else
            {
                dokumentyToolStripMenuItem.Checked = true;
                dokumentyToolStripMenuItem_Click(null, null);
            }
        }
        
        private void openCompanyToolStripButton_Click(object sender, EventArgs e)
        {
            if (CompanyListWindow != null && !CompanyListWindow.IsDisposed)
            {
                CompanyListWindow.Activate();
            }
            else
            {
                kontrahenciToolStripMenuItem.Checked = true;
                kontrahenciToolStripMenuItem_Click(null, null);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                if (ActiveMdiChild is DocumentWindow)
                {
                    SaveDocument((DocumentWindow)ActiveMdiChild);
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var currentChild in MdiChildren)
            {
                currentChild.Close();
            }
            if (EndApplication() == true) Close();
        }

        private void newCompanyToolStripButton_Click(object sender, EventArgs e)
        {
            AddCompany();
        }
        #region Printing

        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintEngine.ShowPageSettings();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is DocumentListWindow)
            {
                ((DocumentListWindow)ActiveMdiChild).ShowPreview();
            }

            if (ActiveMdiChild != null && ActiveMdiChild is DocumentWindow)
            {
                ((DocumentWindow)ActiveMdiChild).ShowPreview();
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is DocumentWindow)
            {
                SaveDocument((DocumentWindow)ActiveMdiChild);
                PrintEngine.AddPrintObject(((DocumentWindow)ActiveMdiChild).Document);
            }
            PrintEngine.ShowPrintDialog();
            PrintEngine.ClearPrintingObjects();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {

            if (ActiveMdiChild != null && ActiveMdiChild is DocumentListWindow)
            {
                ((DocumentListWindow)ActiveMdiChild).Print();
            }

            if (ActiveMdiChild != null && ActiveMdiChild is DocumentWindow)
            {
                ((DocumentWindow)ActiveMdiChild).ShowPreview();
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lubie placki");
        }



        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
