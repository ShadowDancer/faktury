using System;
using System.IO;
using System.Windows.Forms;
using Faktury.Classes;

namespace Faktury.Windows
{
    public partial class BackupSettings : Form
    {
        private readonly SettingsAccessor _settingsAccessor;

        public BackupSettings(SettingsAccessor settingsAccessor)
        {
            _settingsAccessor = settingsAccessor;
            InitializeComponent();
        }

        private void BackupSettings_Load(object sender, EventArgs e)
        {
            //load local backup
            var editorSettings = _settingsAccessor.GetSettings();
            GBLocalCopy.Enabled = CxBLocalBackup.Checked = editorSettings.LocalBackup;
            CxBLocalBackupOnlyWhenExit.Checked = editorSettings.LocalBackupOnlyOnExit;

            //device
            GBDevice.Enabled = CxBDeviceBackup.Checked = editorSettings.DeviceBackup;
            NuDPeriod.Value = editorSettings.DeviceBackupPeriod;
            ComputeTimeToNextDeviceBackupUpdate();

            TBDeviceName.Text = editorSettings.DeviceBackupLabel;
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ComputeTimeToNextDeviceBackupUpdate()
        {
            var editorSettings = _settingsAccessor.GetSettings();
            TimeSpan span = editorSettings.DeviceBackupLastTime.Add(new TimeSpan(editorSettings.DeviceBackupPeriod, 0, 0, 0)).Subtract(DateTime.Today);
            if (span.Days < 0) span = new TimeSpan();
            LElapsedTime.Text = $"Pozostało {span.Days} dni.";
        }

        private void CxBLocalBackup_CheckedChanged(object sender, EventArgs e)
        {
            _settingsAccessor.GetSettings().LocalBackup = GBLocalCopy.Enabled = CxBLocalBackup.Checked;
        }

        private void CxBLocalBackupOnlyWhenExit_CheckedChanged(object sender, EventArgs e)
        {
            _settingsAccessor.GetSettings().LocalBackupOnlyOnExit = CxBLocalBackupOnlyWhenExit.Checked;
        }

        private void CxBDeviceBackup_CheckedChanged(object sender, EventArgs e)
        {
            _settingsAccessor.GetSettings().DeviceBackup = GBDevice.Enabled = CxBDeviceBackup.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _settingsAccessor.GetSettings().DeviceBackupPeriod = (int)NuDPeriod.Value;
            ComputeTimeToNextDeviceBackupUpdate();
        }

        private void BReset_Click(object sender, EventArgs e)
        {
            _settingsAccessor.GetSettings().DeviceBackupLastTime = DateTime.Today;
            ComputeTimeToNextDeviceBackupUpdate();
        }

        private void BChange_Click(object sender, EventArgs e)
        {
            if (CBSelectDevice.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz nośnik z menu rozwijanego. Jeśli nie ma tam żadnych opcji oznacza to, że dysk USB jest nie podłączony, lub jest uszkodzony.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Czy napewno?", "Zmiana nośnika kopii zapasowych", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DriveInfo drive = (DriveInfo)((ComboBoxItem)CBSelectDevice.SelectedItem).Data;
                if (!drive.IsReady || !drive.RootDirectory.Exists)
                {
                    MessageBox.Show("Nośnik nie jest gotowy (został wypięty z gniazda usb lub uszkodzony)!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        var editorSettings = _settingsAccessor.GetSettings();
                        editorSettings.DeviceBackupLabel = CBSelectDevice.Text;
                        editorSettings.DeviceRandomNumber = new Random().Next();
                        string backupFolderPath = Path.Combine(drive.RootDirectory.FullName, Path.Combine("Faktury", Path.Combine("Backup", editorSettings.DeviceRandomNumber.ToString())));
                        Directory.CreateDirectory(backupFolderPath);
                        TBDeviceName.Text = CBSelectDevice.Text;
                        MessageBox.Show(
                            $"Nośnik {CBSelectDevice.Text} jest przygotowany do zapisu kopii zapasowych. Gdy upłynie wyznaczony czas zostniesz poproszony o włożenie nośnika do portu USB.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Błąd podczas przygotowywania nośnika!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CBSelectDevice_DropDown(object sender, EventArgs e)
        {
            CBSelectDevice.Items.Clear();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable || drive.DriveType == DriveType.Network)
                {
                    CBSelectDevice.Items.Add(new ComboBoxItem(drive.Name + " " + drive.VolumeLabel, drive));
                }
            }
        }

    }
}
