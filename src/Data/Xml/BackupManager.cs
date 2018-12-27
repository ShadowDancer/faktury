using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Faktury.Classes;
using Faktury.Windows;

namespace Faktury.Data.Xml
{
    public class BackupManager
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

        public void SaveLocalBackup()
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
        }

        public void SaveDeviceBackup()
        {
            var editorSettings = _settingsAccessor.GetSettings();
            if (editorSettings.DeviceBackup)
            {
                //nie wybrano nośnika
                if (editorSettings.DeviceRandomNumber == -1)
                {
                    MessageBox.Show("Nie wybrano nośnika! Wybierz nośnik w menu opcje -> ustawienia kopii zapasowych.");
                    return;
                }

                //Check if need backup on device
                if (editorSettings.DeviceBackupLastTime.Add(new TimeSpan(editorSettings.DeviceBackupPeriod, 0, 0, 0)).Subtract(DateTime.Today).Days <= 0)
                {
                    //Do backup
                    string backupFolderPath = "";
                    DriveInfo targetDrive = null;

                    do
                    {
                        foreach (DriveInfo drive in DriveInfo.GetDrives())
                        {
                            if (drive.DriveType == DriveType.Removable || drive.DriveType == DriveType.Network)
                            {
                                backupFolderPath = Path.Combine(drive.RootDirectory.FullName, Path.Combine("Faktury", Path.Combine("Backup", editorSettings.DeviceRandomNumber.ToString())));
                                if (Directory.Exists(backupFolderPath))
                                {
                                    targetDrive = drive;
                                    break;
                                }
                            }
                        }

                        if (targetDrive != null)
                        {
                            break;
                        }

                        if (MessageBox.Show("Widzisz to okno, ponieważ na dzisiaj zostało zaplanowane stworzenie kopii zapasowej danych na zewnętrznym nośniku. Nie znaleziono nośnika. Prawdopodobnie jest on nie podłączony, lub dane na nim uległy uszkodzeniu. Podłącz nośnik i wybierz Tak. Jeśli chcesz przerwać wybierz Nie.", "Kopia zapasowa na nośniku", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            editorSettings.DeviceBackupLastTime = DateTime.Today;
                            return;
                        }
                    }
                    while (true);

                    //Jeśli ustawiono nośnik kontynuuj
                    backupFolderPath = Path.Combine(backupFolderPath, Path.Combine(DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString().Replace(':', '-')));
                    Directory.CreateDirectory(backupFolderPath);

                    editorSettings.DeviceBackupLastTime = DateTime.Today;

                    _modelStoreLoader.SaveDataToDirectory(backupFolderPath);
                }
            }
        }

        /// <summary>
        /// Load local backup, return true if success
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadLocalBackup(string path)
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
            return result;
        }
    }
}
