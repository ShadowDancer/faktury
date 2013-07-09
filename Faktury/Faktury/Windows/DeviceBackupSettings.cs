using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Faktury.Windows
{
    public partial class BackupSettings : Form
    {
        public BackupSettings()
        {
            InitializeComponent();
        }

        private void BackupSettings_Load(object sender, EventArgs e)
        {
            //load local backup
            GBLocalCopy.Enabled = CxBLocalBackup.Checked = MainForm.Instance.Settings.LocalBackup;
            CxBLocalBackupOnlyWhenExit.Checked = MainForm.Instance.Settings.LocalBackupOnlyOnExit;

            //device
            GBDevice.Enabled = CxBDeviceBackup.Checked = MainForm.Instance.Settings.DeviceBackup;
            NuDPeriod.Value = MainForm.Instance.Settings.DeviceBackupPeriod;
            TimeToNextDeviceBackupUpdate();

            TBDeviceName.Text = MainForm.Instance.Settings.DeviceBackupLabel;
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void TimeToNextDeviceBackupUpdate()
        {
            TimeSpan span = MainForm.Instance.Settings.DeviceBackupLastTime.Add(new TimeSpan(MainForm.Instance.Settings.DeviceBackupPeriod, 0, 0, 0)).Subtract(DateTime.Today);
            if (span.Days < 0) span = new TimeSpan();
            LElapsedTime.Text = String.Format("Pozostało {0} dni.", span.Days);
        }

        private void CxBLocalBackup_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Instance.Settings.LocalBackup = GBLocalCopy.Enabled = CxBLocalBackup.Checked;
        }

        private void CxBLocalBackupOnlyWhenExit_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Instance.Settings.LocalBackupOnlyOnExit = CxBLocalBackupOnlyWhenExit.Checked;
        }

        private void CxBDeviceBackup_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.Instance.Settings.DeviceBackup = GBDevice.Enabled = CxBDeviceBackup.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            MainForm.Instance.Settings.DeviceBackupPeriod = (int)NuDPeriod.Value;
            TimeToNextDeviceBackupUpdate();
        }

        private void BReset_Click(object sender, EventArgs e)
        {
            MainForm.Instance.Settings.DeviceBackupLastTime = DateTime.Today;
            TimeToNextDeviceBackupUpdate();
        }

        private void BChange_Click(object sender, EventArgs e)
        {
            if (CBSelectDevice.SelectedIndex == -1)
            {
                MessageBox.Show("Wybierz nośnik z menu rozwijanego. Jeśli nie ma tam żadnych opcji oznacza to, że dysk USB jest nie podłączony, lub jest uszkodzony.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Czy napewno?", "Zmiana nośnika kopii zapasowych", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                DriveInfo Drive = (DriveInfo)((ComboBoxItem)CBSelectDevice.SelectedItem).Data;
                if (!Drive.IsReady || !Drive.RootDirectory.Exists)
                {
                    MessageBox.Show("Nośnik nie jest gotowy (został wypięty z gniazda usb lub uszkodzony)!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        MainForm.Instance.Settings.DeviceBackupLabel = CBSelectDevice.Text;
                        MainForm.Instance.Settings.DeviceRandomNumber = new Random().Next();
                        string BackupFolderPath = Path.Combine(Drive.RootDirectory.FullName, Path.Combine("Faktury", Path.Combine("Backup", MainForm.Instance.Settings.DeviceRandomNumber.ToString())));
                        Directory.CreateDirectory(BackupFolderPath);
                        TBDeviceName.Text = CBSelectDevice.Text;
                        MessageBox.Show(string.Format("Nośnik {0} jest przygotowany do zapisu kopii zapasowych. Gdy upłynie wyznaczony czas zostniesz poproszony o włożenie nośnika do portu USB.", CBSelectDevice.Text), "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            foreach (DriveInfo Drive in DriveInfo.GetDrives())
            {
                if (Drive.DriveType == DriveType.Removable || Drive.DriveType == DriveType.Network)
                {
                    CBSelectDevice.Items.Add(new ComboBoxItem(Drive.Name + " " + Drive.VolumeLabel, Drive));
                }
            }
        }

    }
}
