using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Faktury.Windows
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
                    string MainDirectoryPath = Path.Combine(MainForm.Instance.BackupPath, DateTime.Now.ToShortDateString().Replace('\\', '-').Replace('/', '-'));
                    MainDirectoryPath = Path.Combine(MainDirectoryPath, DateTime.Now.ToLongTimeString().Replace(':', '-'));
                    Directory.CreateDirectory(MainDirectoryPath);

                    MainForm.Instance.SaveDocumentsToFile(MainDirectoryPath);
                    MainForm.Instance.SaveCompaniesToFile(MainDirectoryPath);
                    MainForm.Instance.SaveServicesToFile(MainDirectoryPath);
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
                    string BackupFolderPath = "";
                    DriveInfo TargetDrive = null;

                    do
                    {
                        foreach (DriveInfo Drive in DriveInfo.GetDrives())
                        {
                            if (Drive.DriveType == DriveType.Removable || Drive.DriveType == DriveType.Network)
                            {
                                BackupFolderPath = Path.Combine(Drive.RootDirectory.FullName, Path.Combine("Faktury", Path.Combine("Backup", MainForm.Instance.Settings.DeviceRandomNumber.ToString())));
                                if (Directory.Exists(BackupFolderPath))
                                {
                                    TargetDrive = Drive;
                                    break;
                                }
                            }
                        }

                        if (TargetDrive != null)
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
                    BackupFolderPath = Path.Combine(BackupFolderPath, Path.Combine(DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString().Replace(':', '-')));
                    Directory.CreateDirectory(BackupFolderPath);

                    MainForm.Instance.Settings.DeviceBackupLastTime = DateTime.Today;
                                
                    MainForm.Instance.SaveDocumentsToFile(BackupFolderPath);
                    MainForm.Instance.SaveCompaniesToFile(BackupFolderPath);
                    MainForm.Instance.SaveServicesToFile(BackupFolderPath);
                }
            }
        }

        /// <summary>
        /// Load local backup, return true if success
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public bool LoadLocalBackup(string Path)
        {
            bool Result = true;
            string RestorePath = System.IO.Path.Combine(MainForm.Instance.BackupPath, "Restore point");
            try
            {
                // Create restore point
                if (!Directory.Exists(MainForm.Instance.BackupPath)) Directory.CreateDirectory(MainForm.Instance.BackupPath);
                if (!Directory.Exists(RestorePath)) Directory.CreateDirectory(RestorePath);
                    
                MainForm.Instance.SaveDocumentsToFile(RestorePath);
                MainForm.Instance.SaveCompaniesToFile(RestorePath);
                MainForm.Instance.SaveServicesToFile(RestorePath);

                // Delete data
                MainForm.Instance.CleanCompanies();
                MainForm.Instance.CleanDocuments();
                MainForm.Instance.CleanServices();


                // Load backup
                if (Directory.Exists(Path))
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
                MainForm.Instance.LoadCompaniesFromFile(RestorePath);
                MainForm.Instance.LoadDocumentsFromFile(RestorePath);
                MainForm.Instance.LoadServicesFromFile(RestorePath);

                MainForm.Instance.ReloadCompanyComboboxesInChildWindows();

                Result = false;
            }
            finally
            {
                new DirectoryInfo(RestorePath).Delete(true);
            }
            return Result;

        }
    }
}
