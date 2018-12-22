using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using System.Text;


namespace Faktury.Windows
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Instance = this;

            DataPath = Path.Combine(AppDataDirectoryPath, "Data");
            BackupPath = Path.Combine(AppDataDirectoryPath, "Backup");
        }

        #region Properties
            //singleton
            public static MainForm Instance = null;



            //data
            public string AppDataDirectoryPath = Path.Combine(CommonDocuments.GetCommonDocumentsFolder(), "Faktury");
            public string DataPath;
            public string BackupPath;

            public EditorSettings Settings = null;
            public BackupManager BackupManager = new BackupManager();

            public List<Classes.Company> Companies = new List<Faktury.Classes.Company>();
            public List<Classes.Document> Documents = new List<Faktury.Classes.Document>();
            public List<Classes.Service> Services = new List<Faktury.Classes.Service>();

            public Dictionary<int, int> HigestDocumentID = new Dictionary<int, int>();//get higest ID for year
            public int HigestCompanyID = 0;
            public int GetNewCompanyID { get { return ++HigestCompanyID; } }
            public int HigestServiceID = 0;
            public int GetNewServiceID { get { return ++HigestServiceID; } }

            #region Windows
                //options
                public OptionsWindow OptionsWindow = null;

                //document list
                public DocumentListWindow DocumentListWindow = null;

                //company list
                public CompanyListWindow CompanyListWindow = null;

                //Services list
                public ServicesListWindow ServicesListWindow = null;

            #endregion

            //printing
            public Print_Framework.PrintEngine PrintEngine = new Print_Framework.PrintEngine(); 

        #endregion

        #region Functions

        public void UpdateHigestDocumentID()
        {
            foreach(var CurrentDocument in Documents)
            {
                if (HigestDocumentID.ContainsKey(CurrentDocument.Year))
                {
                    if (HigestDocumentID[CurrentDocument.Year] < CurrentDocument.Number)
                    {
                        HigestDocumentID[CurrentDocument.Year] = CurrentDocument.Number;
                    }
                }
                else 
                {
                    if (CurrentDocument.Number > 0) HigestDocumentID.Add(CurrentDocument.Year, CurrentDocument.Number);
                    else HigestDocumentID.Add(CurrentDocument.Year, 1);
                }
            }
        }

        public void UpdateHigestCompanyID()
        {
            foreach (Classes.Company CurrentComapny in Companies)
            {
                if (CurrentComapny.ID > HigestCompanyID) HigestCompanyID = CurrentComapny.ID;
            }
        }

        public void UpdateHigestServiceID()
        {
            foreach (Classes.Service CurrentService in Services)
            {
                if (CurrentService.ID > HigestServiceID) HigestServiceID = CurrentService.ID;
            }
        }

        public void ReloadCompanyCombobox(ComboBox ComboBox)
        {
            ComboBox.Items.Clear();
            foreach (Classes.Company CurrentCompany in Companies)
            {
                ComboBox.Items.Add(new ComboBoxItem(CurrentCompany.Tag, CurrentCompany.ID));
            }
            if (ComboBox.Items.Count > 0) ComboBox.SelectedIndex = 0;
        }

        public void ReloadCompanyComboboxesInChildWindows()
        {
            if (DocumentListWindow != null)
            {
                DocumentListWindow.UpdateCompanyCombobox();
            }

            //update w child window
            foreach (var CurrentChild in MdiChildren)
            {
                if (CurrentChild is DocumentWindow)
                {
                    ((DocumentWindow)CurrentChild).ReloadCompanyCombobox();
                }
            }
        }

        #region Services
        public void addService()
        {
            ServiceWindow childForm = new ServiceWindow();

            childForm.MdiParent = this;

            childForm.Text = childFormNumber++.ToString() + ": Nowa usługa";
            childForm.Show(MainDockPanel);
        }

        public void editService(Classes.Service ServiceToEdit)
        {
            ServiceWindow childForm = new ServiceWindow();
            childForm.MdiParent = this;

            childForm.Text = childFormNumber++.ToString() + ": Edycja " + ServiceToEdit.Name;
            childForm.Service = ServiceToEdit;
            childForm.Show(MainDockPanel);            
        }

        public void deleteService(Classes.Service ServiceToRemove)
        {
            if (MessageBox.Show("Na pewno?", "Usuwanie usługi...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Services.Remove(ServiceToRemove);
            }
        }

        public void CleanServices()
        {
            foreach (var ChildForm in MdiChildren)
            {
                if (ChildForm is ServiceWindow)
                {
                    ChildForm.Close();
                }
            }
            Services.Clear();
        }

        #endregion

        #region Companies
        public void addCompany()
        {
            CompanyWindow childForm = new CompanyWindow();
          
            childForm.MdiParent = this;

            childForm.Text = childFormNumber++.ToString() + ": Nowa firma";
            childForm.Show(MainDockPanel);
        }

        public void editCompany(Classes.Company CompanyToEdit)
        {
                CompanyWindow childForm = new CompanyWindow();
                if (CompanyToEdit == Settings.OwnerCompany && Settings.OwnerCompany != null) 
                    childForm.AddToCollection = false;
                else childForm.AddToCollection = true;

                childForm.MdiParent = this;

                childForm.Text = childFormNumber++.ToString() + ": Edycja " + CompanyToEdit.Tag;
                childForm.Company = CompanyToEdit;
                childForm.Show(MainDockPanel);
        }

        public void deleteCompany(Classes.Company CompanyToRemove)
        {
                if (MessageBox.Show("Na pewno?", "Usuwanie firmy...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                        Companies.Remove(CompanyToRemove);
                        ReloadCompanyComboboxesInChildWindows();
                }
        }

        public void CleanCompanies()
        {
            foreach (var ChildForm in MdiChildren)
            {
                if (ChildForm is CompanyWindow)
                {
                    ChildForm.Close();
                }
            }
            Companies.Clear();
        }

        #endregion

        #region Documents

        public void CleanDocuments()
        {
            foreach (var ChildForm in MdiChildren)
            {
                if (ChildForm is DocumentWindow)
                {
                    //TODO
                    ((DocumentWindow)ChildForm).ForceClose = true;
                    ChildForm.Close();
                }
            }
            Documents.Clear();
        }

        public void OpenDocument(Classes.Document Document)
        {
            if (Document != null)
            {

                DocumentWindow childForm = new DocumentWindow();
                childForm.MdiParent = this;

                childForm.Text = Document.Number.ToString() + "//" + Document.Year.ToString() + " " + Document.Name;
                childForm.Document = Document;
                childForm.Show(MainDockPanel);
            }
            else throw new Exception();
        }

        public bool SaveDocument(DocumentWindow Window)
        {
            Classes.Document Document = Window.Document;

            try
            {
                Classes.Document Check = Documents.Find(n => (n.Number == Window.nUDNumber.Value && n.Year == Window.nUDYear.Value ));
                if (Check == null)
                {
                    if(Documents.Find(n => n == Window.Document) == null)
                    Documents.Add(Document);
                }
                else
                {
                    if(Check != Window.Document)
                    throw new Exception("Dokument o danym numerze już istnieje!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //save document if no problems
            Window.SaveDataFromControls(Document);
            UpdateHigestDocumentID();

            return true;
        }

        public Classes.Document FindDocument(int Number, int Year)
        {
            return Documents.Find(n => (n.Number == Number && n.Year == Year));
        }

        #endregion
        public bool EndApplication()
            {
                if (MdiChildren.Length > 0) return false;
                else return true;
            }

        public void RunFirstUseWizard()
        {
            if (MessageBox.Show("Program faktury został uruchomiony po raz pierwszy. Kreator pierwszego uruchomienia przeprowadzi Cię przez proces konfigurowania aplikacji. Kontynuować?", "Kreator pierwszego uruchomienia", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
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
            public void LoadSettingsFromFile(string ConfigFilePath)
                {
                    Settings = new EditorSettings();
                    if (File.Exists(ConfigFilePath))
                    {
                        using (StreamReader Reader = new StreamReader(ConfigFilePath))
                        {
                            System.Xml.Serialization.XmlSerializer Serializer = new System.Xml.Serialization.XmlSerializer(typeof(EditorSettings));
                            Settings = (EditorSettings)Serializer.Deserialize(Reader);
                        }
                    }
                    else
                    {
                        RunFirstUseWizard();
                    }
                }
                public void LoadCompaniesFromFile(string Filepath)
                {
                    if (Filepath.Length == 0) return;
                    try
                    {
                        Filepath = Path.Combine(Filepath, "Companies.xml");

                        XmlDocument Doc = new XmlDocument();
                        Doc.Load(Filepath);

                        foreach (XmlNode CurrentNode in Doc["Companies"])
                        {
                            if (CurrentNode.Name == "IssueCompany") continue;

                            Classes.Company NewCompany = Classes.Company.GetCompanyFromXml(CurrentNode);
                            Classes.Company Check = new Faktury.Classes.Company();
                            Check = Companies.Find(n => n.ID == NewCompany.ID);
                            if (Check == null)
                            {
                                Companies.Add(NewCompany);
                            }
                            else MessageBox.Show("Kolekcja zawiera już element o ID " + NewCompany.ID.ToString() + "!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        MainForm.Instance.UpdateHigestCompanyID();
                    }
                    catch
                    {
                        MessageBox.Show("Nie można wczytać pliku z kontrahentami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                public void LoadDocumentsFromFile(string Filepath)
                {
                    if (Filepath.Length == 0) return;
                    try
                    {
                        Filepath = Path.Combine(Filepath, "Documents.xml");

                        XmlDocument Doc = new XmlDocument();
                        Doc.Load(Filepath);
                        foreach (XmlNode CurrentNode in Doc["Documents"])
                        {
                            Classes.Document NewDocument = Classes.Document.GetDocumentFromXml(CurrentNode);
                                Documents.Add(NewDocument);
                        }
                        UpdateHigestDocumentID();
                    }
                    catch
                    {
                        MessageBox.Show("Nie można wczytać pliku z dokumentami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                public void LoadServicesFromFile(string Filepath)
                {
                    if (Filepath.Length == 0) return;
                    try
                    {
                        Filepath = Path.Combine(Filepath, "Services.xml");

                        XmlDocument Doc = new XmlDocument();
                        Doc.Load(Filepath);
                        foreach (XmlNode CurrentNode in Doc["Services"])
                        {
                            Services.Add(Classes.Service.GetServiceFromXml(CurrentNode));
                        }
                        MainForm.Instance.UpdateHigestServiceID();
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
                public void SaveSettingsToFile(string ConfigFilePath)
                {
                    using(StreamWriter Writer = new StreamWriter(ConfigFilePath))
                    {
                        
                        System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                        ns.Add("", "");

                        System.Xml.Serialization.XmlSerializer Serializer = new System.Xml.Serialization.XmlSerializer(typeof(EditorSettings));
                        Serializer.Serialize(Writer, Settings, ns) ;
                    }
                }

                public void SaveCompaniesToFile(string Filepath)
                {
                    Filepath = Path.Combine(Filepath, "Companies.xml");

                    XmlDocument Doc = new XmlDocument();

                    XmlDeclaration xmlHeader = Doc.CreateXmlDeclaration("1.0", "utf-8", null);

                    Doc.LoadXml("<Companies></Companies>");
                    Doc.InsertBefore(xmlHeader, Doc.DocumentElement); 


                    foreach (var CurrentCompany in Companies)
                    {
                        Doc["Companies"].AppendChild(CurrentCompany.GetXmlElement(Doc));
                    }

                    Doc.Save(Filepath);
                }
                public void SaveDocumentsToFile(string Filepath)
                {
                    Filepath = Path.Combine(Filepath, "Documents.xml");

                    XmlDocument Doc = new XmlDocument();

                    XmlDeclaration xmlHeader = Doc.CreateXmlDeclaration("1.0", "utf-8", null);

                    Doc.LoadXml("<Documents></Documents>");
                    Doc.InsertBefore(xmlHeader, Doc.DocumentElement); 


                    foreach (var CurrentDocument in Documents)
                    {
                        Doc["Documents"].AppendChild(CurrentDocument.GetXmlElement(Doc));
                    }

                    Doc.Save(Filepath);
                }
                public void SaveServicesToFile(string Filepath)
                {
                    Filepath = Path.Combine(Filepath, "Services.xml");

                    XmlDocument Doc = new XmlDocument();

                    XmlDeclaration xmlHeader = Doc.CreateXmlDeclaration("1.0", "utf-8", null);

                    Doc.LoadXml("<Services></Services>");
                    Doc.InsertBefore(xmlHeader, Doc.DocumentElement);


                    foreach (var CurrentService in Services)
                    {
                        Doc["Services"].AppendChild(CurrentService.GetXmlElement(Doc));
                    }

                    Doc.Save(Filepath);
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
            childFormNumber++;
            if (childFormNumber > 1)
                childForm.Text += " (" + childFormNumber.ToString() + ")";

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
                string FileName = saveFileDialog.FileName;
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
            foreach (var CurrentChild in MdiChildren)
            {
                CurrentChild.Close();
            }
            if (EndApplication() == true) Close();
        }

        private void newCompanyToolStripButton_Click(object sender, EventArgs e)
        {
            addCompany();
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
