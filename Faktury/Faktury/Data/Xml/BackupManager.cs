using System;
using System.IO;
using Faktury.Windows;

namespace Faktury.Data.Xml
{
    public class BackupManager
    {
        public bool ExitingApplication = false;

        public void SaveLocalBackup()
        {
            if(MainForm.Instance.Settings.LocalBackup)
            {

                if (MainForm.Instance.Settings.LocalBackupOnlyOnExit && ExitingApplication || !MainForm.Instance.Settings.LocalBackupOnlyOnExit)
                {
                    if (!Directory.Exists(MainForm.Instance.BackupPath)) Directory.CreateDirectory(MainForm.Instance.BackupPath);
                    string mainDirectoryPath = Path.Combine(MainForm.Instance.BackupPath, DateTime.Now.ToShortDateString().Replace('\\', '-').Replace('/', '-'));
                    mainDirectoryPath = Path.Combine(mainDirectoryPath, DateTime.Now.ToLongTimeString().Replace(':', '-'));
                    Directory.CreateDirectory(mainDirectoryPath);

                    MainForm.Instance.SaveDocumentsToFile(mainDirectoryPath);
                    MainForm.Instance.SaveCompaniesToFile(mainDirectoryPath);
                    MainForm.Instance.SaveServicesToFile(mainDirectoryPath);
                }
            }
        }

        public void SaveDeviceBackup()
        {
            if (MainForm.Instance.Settings.DeviceBackup)
            {
                //nie wybrano nośnika
                if (MainForm.Instance.Settings.DeviceRandomNumber == -1)
                {
                    System.Windows.Forms.MessageBox.Show("Nie wybrano nośnika! Wybierz nośnik w menu opcje -> ustawienia kopii zapasowych.");
                    return;
                }

                //Check if need backup on device
                if (MainForm.Instance.Settings.DeviceBackupLastTime.Add(new TimeSpan(MainForm.Instance.Settings.DeviceBackupPeriod, 0, 0, 0)).Subtract(DateTime.Today).Days <= 0)
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
                                backupFolderPath = Path.Combine(drive.RootDirectory.FullName, Path.Combine("Faktury", Path.Combine("Backup", MainForm.Instance.Settings.DeviceRandomNumber.ToString())));
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
                        else
                        {
                            if (System.Windows.Forms.MessageBox.Show("Widzisz to okno, ponieważ na dzisiaj zostało zaplanowane stworzenie kopii zapasowej danych na zewnętrznym nośniku. Nie znaleziono nośnika. Prawdopodobnie jest on nie podłączony, lub dane na nim uległy uszkodzeniu. Podłącz nośnik i wybierz Tak. Jeśli chcesz przerwać wybierz Nie.", "Kopia zapasowa na nośniku", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk, System.Windows.Forms.MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                            {
                                MainForm.Instance.Settings.DeviceBackupLastTime = DateTime.Today;
                                return;
                            }
                        }
                    }
                    while (true);

                    //Jeśli ustawiono nośnik kontynuuj
                    backupFolderPath = Path.Combine(backupFolderPath, Path.Combine(DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString().Replace(':', '-')));
                    Directory.CreateDirectory(backupFolderPath);

                    MainForm.Instance.Settings.DeviceBackupLastTime = DateTime.Today;
                                
                    MainForm.Instance.SaveDocumentsToFile(backupFolderPath);
                    MainForm.Instance.SaveCompaniesToFile(backupFolderPath);
                    MainForm.Instance.SaveServicesToFile(backupFolderPath);
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
            string restorePath = Path.Combine(MainForm.Instance.BackupPath, "Restore point");
            try
            {
                // Create restore point
                if (!Directory.Exists(MainForm.Instance.BackupPath)) Directory.CreateDirectory(MainForm.Instance.BackupPath);
                if (!Directory.Exists(restorePath)) Directory.CreateDirectory(restorePath);
                    
                MainForm.Instance.SaveDocumentsToFile(restorePath);
                MainForm.Instance.SaveCompaniesToFile(restorePath);
                MainForm.Instance.SaveServicesToFile(restorePath);

                // Delete data
                MainForm.Instance.CleanCompanies();
                MainForm.Instance.CleanDocuments();
                MainForm.Instance.CleanServices();


                // Load backup
                if (Directory.Exists(path))
                {
                    MainForm.Instance.LoadCompaniesFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                    MainForm.Instance.LoadDocumentsFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                    MainForm.Instance.LoadServicesFromFile(MainForm.Instance.OpenDataFolder.SelectedPath);
                    MainForm.Instance.ReloadCompanyComboboxesInChildWindows();
                }
            }
            catch
            {
                // Restore if failed
                MainForm.Instance.LoadCompaniesFromFile(restorePath);
                MainForm.Instance.LoadDocumentsFromFile(restorePath);
                MainForm.Instance.LoadServicesFromFile(restorePath);

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
