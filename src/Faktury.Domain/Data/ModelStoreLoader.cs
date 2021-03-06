﻿using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Faktury.Domain.Data.Repository;
using Faktury.Domain.Data.Xml;
using Faktury.Domain.Domain;
using Faktury.Domain.Services;

namespace Faktury.Domain.Data
{
    public class ModelStoreLoader
    {
        private readonly SettingsRepository _settingsRepository;
        private readonly ModelStore _modelStore;
        private readonly string _configPath;
        private readonly string _dataPath;

        public ModelStoreLoader(SettingsRepository settingsRepository, ModelStore modelStore, string applicationDirectory)
        {
            _settingsRepository = settingsRepository;
            _modelStore = modelStore;
            _configPath = Path.Combine(applicationDirectory, "Config.xml");
            _dataPath = Path.Combine(applicationDirectory, "Data");
        }



        public void LoadEverythingFromDirectory()
        {
            LoadSettingsFromFile(_configPath);

            LoadDataFromDirectory(_dataPath);
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
                    _settingsRepository.SetSettings((EditorSettings)serializer.Deserialize(reader));
                }
            }
            else
            {
                _settingsRepository.SetSettings(new EditorSettings());
            }
        }


        public void SaveEverythingToDirectoryWithBackup(IFileBackup backupManager)
        {
            try
            {
                if (!Directory.Exists(_dataPath)) Directory.CreateDirectory(_dataPath);

                SaveDataToDirectory(_dataPath);

                backupManager.SaveBackup();
                SaveSettingsToFile(_configPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Błąd podczas zapisywania plików!", ex);
            }
        }

        public void SaveSettingsToFile(string configFilePath)
        {
            using(StreamWriter writer = new StreamWriter(configFilePath))
            {
                        
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                XmlSerializer serializer = new XmlSerializer(typeof(EditorSettings));
                serializer.Serialize(writer, _settingsRepository.GetSettings(), ns) ;
            }
        }


        public void LoadDataFromDirectory(string directory)
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
                    if (_modelStore.CompanyRepository.FindCompany(newCompany.Id) == null)
                    {
                        _modelStore.CompanyRepository.AddCompany(newCompany);
                    }
                    else
                    {
                        throw new InvalidOperationException("Kolekcja zawiera już element o ID " + newCompany.Id + "!");
                    }
                }
            }
            catch
            {
                throw new InvalidOperationException("Nie można wczytać pliku z kontrahentami!");
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
                    Document newDocument = new DocumentXmlSerializer().GetDocumentFromXml(currentNode, _modelStore, _settingsRepository);
                    _modelStore.DocumentRepository.Documents.Add(newDocument);
                }

                _modelStore.DocumentRepository.UpdateHighestDocumentId();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Nie można wczytać pliku z dokumentami!", ex);
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
                    _modelStore.ServiceRepository.AddService(ServiceToXmlSerializer.GetServiceFromXml(currentNode));
                }
            }
            catch
            {
                throw new InvalidOperationException("Nie można wczytać pliku z usługami!");
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


            foreach (var currentCompany in _modelStore.CompanyRepository.Companies)
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


            foreach (var currentDocument in _modelStore.DocumentRepository.Documents)
            {
                // ReSharper disable once PossibleNullReferenceException
                doc["Documents"].AppendChild(new DocumentXmlSerializer().GetXmlElement(currentDocument, doc));
            }

            doc.Save(filePath);
        }

        private void SaveServicesToFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlHeader = doc.CreateXmlDeclaration("1.0", "utf-8", null);

            doc.LoadXml("<Services></Services>");
            doc.InsertBefore(xmlHeader, doc.DocumentElement);


            foreach (var currentService in _modelStore.ServiceRepository.Services)
            {
                // ReSharper disable once PossibleNullReferenceException
                doc["Services"].AppendChild(ServiceToXmlSerializer.GetXmlElement(currentService, doc));
            }

            doc.Save(filePath);
        }
    }
}