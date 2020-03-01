using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Faktury.Domain.Classes;
using Faktury.Domain.Data;
using Faktury.Domain.Services;
using Faktury.Windows;

namespace Faktury.Data.Xml
{
    public class BackupManager : IFileBackup
    {
        public bool ExitingApplication = false;
        private readonly SettingsAccessor _settingsAccessor;
        private readonly ModelStoreLoader _modelStoreLoader;
        public readonly string BackupPath;

        public BackupManager(SettingsAccessor settingsAccessor, ModelStoreLoader modelStoreLoader, string backupPath)
        {
            _settingsAccessor = settingsAccessor;
            _modelStoreLoader = modelStoreLoader;
            BackupPath = backupPath;
        }

        public Task SaveBackup()
        {
            if(_settingsAccessor.GetSettings().LocalBackup)
            {
                var editorSettings = _settingsAccessor.GetSettings();
                if (editorSettings.LocalBackupOnlyOnExit && ExitingApplication || !editorSettings.LocalBackupOnlyOnExit)
                {
                    if (!Directory.Exists(BackupPath))
                    {
                        Directory.CreateDirectory(BackupPath);
                    }
                    string mainDirectoryPath = Path.Combine(BackupPath, DateTime.Now.ToString("d", CultureInfo.InvariantCulture).Replace('\\', '-').Replace('/', '-'));
                    mainDirectoryPath = Path.Combine(mainDirectoryPath, DateTime.Now.ToString("T", CultureInfo.InvariantCulture).Replace(':', '-'));
                    Directory.CreateDirectory(mainDirectoryPath);

                    _modelStoreLoader.SaveDataToDirectory(mainDirectoryPath);
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Load local backup, return true if success
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task<bool> LoadBackupFromFile(string path)
        {
            bool result = true;
            string restorePath = Path.Combine(BackupPath, "Restore point");
            try
            {
                // Create restore point
                if (!Directory.Exists(BackupPath)) Directory.CreateDirectory(BackupPath);
                if (!Directory.Exists(restorePath)) Directory.CreateDirectory(restorePath);

                _modelStoreLoader.SaveDataToDirectory(restorePath);

                // Delete data
                MainForm.Instance.RemoveAllData();

                // Load backup
                if (Directory.Exists(path))
                {
                    _modelStoreLoader.LoadDataFromDirectory(MainForm.Instance.OpenDataFolder.SelectedPath);
                    MainForm.Instance.ReloadCompanyComboboxesInChildWindows();
                }
            }
            catch
            {
                // Restore if failed
                _modelStoreLoader.LoadDataFromDirectory(MainForm.Instance.OpenDataFolder.SelectedPath);
                MainForm.Instance.ReloadCompanyComboboxesInChildWindows();

                result = false;
            }
            finally
            {
                new DirectoryInfo(restorePath).Delete(true);
            }
            return Task.FromResult(result);
        }
    }
}
