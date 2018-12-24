using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Faktury.Data.Xml;

namespace Faktury.Classes
{
    public class ModelStoreLoader
    {
        private readonly SettingsAccessor _settingsAccessor;
        private readonly ModelStore _modelStore;
        private readonly string _configPath;
        private readonly string _dataPath;

        public ModelStoreLoader(SettingsAccessor settingsAccessor, ModelStore modelStore, string applicationDirectory)
        {
            _settingsAccessor = settingsAccessor;
            _modelStore = modelStore;
            _configPath = Path.Combine(applicationDirectory, "Config.xml");
            _dataPath = Path.Combine(applicationDirectory, "Data");
        }



        public void LoadEverythingFromDirectory()
        {
            LoadSettingsFromFile(_configPath);

            LoadCompaniesFromFile(_dataPath);
            LoadDocumentsFromFile(_dataPath);
            LoadServicesFromFile(_dataPath);

        }

        public bool ConfigExists()
        {
            return File.Exists(_configPath);
        }

        public void LoadSettingsFromFile(string configFilePath)
        {
            if (File.Exists(configFilePath))
            {
                using (StreamReader reader = new StreamReader(configFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(EditorSettings));
                    _settingsAccessor.SetSettings((EditorSettings)serializer.Deserialize(reader));
                }
            }
            else
            {
                _settingsAccessor.SetSettings(new EditorSettings());
            }
        }


        public void SaveEverythingToDirectoryWithBackup(BackupManager backupManager)
        {
            try
            {
                if (!Directory.Exists(_dataPath)) Directory.CreateDirectory(_dataPath);

                SaveCompaniesToFile(_dataPath);
                SaveDocumentsToFile(_dataPath);
                SaveServicesToFile(_dataPath);
                backupManager.SaveLocalBackup();
                backupManager.SaveDeviceBackup();
                SaveSettingsToFile(_configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas zapisywania plików:\n" + ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveSettingsToFile(string configFilePath)
        {
            using(StreamWriter writer = new StreamWriter(configFilePath))
            {
                        
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                XmlSerializer serializer = new XmlSerializer(typeof(EditorSettings));
                serializer.Serialize(writer, _settingsAccessor.GetSettings(), ns) ;
            }
        }


        public void LoadDataFromFile(string directory)
        {
            LoadCompaniesFromFile(Path.Combine(directory, "Companies.xml"));
            LoadDocumentsFromFile(Path.Combine(directory, "Documents.xml"));
            LoadServicesFromFile(Path.Combine(directory, "Services.xml"));
        }

        private void LoadCompaniesFromFile(string filepath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);

                // ReSharper disable once PossibleNullReferenceException
                foreach (XmlNode currentNode in doc["Companies"])
                {
                    if (currentNode.Name == "IssueCompany") continue;

                    Company newCompany = CompanyToXmlSerializer.GetCompanyFromXml(currentNode);
                    if (_modelStore.Companies.Find(n => n.Id == newCompany.Id) == null)
                    {
                        _modelStore.Companies.Add(newCompany);
                    }
                    else MessageBox.Show("Kolekcja zawiera już element o ID " + newCompany.Id + "!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _modelStore.UpdateHigestCompanyId();
            }
            catch
            {
                MessageBox.Show("Nie można wczytać pliku z kontrahentami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDocumentsFromFile(string filepath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);
                // ReSharper disable once PossibleNullReferenceException
                foreach (XmlNode currentNode in doc["Documents"])
                {
                    Document newDocument = new DocumentXmlSerializer(_modelStore).GetDocumentFromXml(currentNode);
                    _modelStore.Documents.Add(newDocument);
                }

                _modelStore.UpdateHighestDocumentId();
            }
            catch
            {
                MessageBox.Show("Nie można wczytać pliku z dokumentami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadServicesFromFile(string filepath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filepath);
                // ReSharper disable once PossibleNullReferenceException
                foreach (XmlNode currentNode in doc["Services"])
                {
                    _modelStore.Services.Add(ServiceToXmlSerializer.GetServiceFromXml(currentNode));
                }

                _modelStore.UpdateHigestServiceId();
            }
            catch
            {
                MessageBox.Show("Nie można wczytać pliku z usługami!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveDataToDirectory(string directory)
        {
            SaveCompaniesToFile(Path.Combine(directory, "Companies.xml"));
            SaveDocumentsToFile(Path.Combine(directory, "Documents.xml"));
            SaveServicesToFile(Path.Combine(directory, "Services.xml"));
        }

        private void SaveCompaniesToFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

            doc.LoadXml("<Companies></Companies>");
            doc.InsertBefore(xmlHeader, doc.DocumentElement); 


            foreach (var currentCompany in _modelStore.Companies)
            {
                // ReSharper disable once PossibleNullReferenceException
                doc["Companies"].AppendChild(CompanyToXmlSerializer.GetXmlElement(currentCompany, doc));
            }

            doc.Save(filePath);
        }

        private void SaveDocumentsToFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

            doc.LoadXml("<Documents></Documents>");
            doc.InsertBefore(xmlHeader, doc.DocumentElement); 


            foreach (var currentDocument in _modelStore.Documents)
            {
                // ReSharper disable once PossibleNullReferenceException
                doc["Documents"].AppendChild(new DocumentXmlSerializer(_modelStore).GetXmlElement(currentDocument, doc));
            }

            doc.Save(filePath);
        }

        private void SaveServicesToFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

            doc.LoadXml("<Services></Services>");
            doc.InsertBefore(xmlHeader, doc.DocumentElement);


            foreach (var currentService in _modelStore.Services)
            {
                // ReSharper disable once PossibleNullReferenceException
                doc["Services"].AppendChild(ServiceToXmlSerializer.GetXmlElement(currentService, doc));
            }

            doc.Save(filePath);
        }
    }
}