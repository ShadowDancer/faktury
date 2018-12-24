using System;
using System.Windows.Forms;
using Faktury.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class ServiceWindow : DockContent
    {
        private readonly SettingsAccessor _settingsAccessor;
        private readonly ModelStore _modelStore;

        public ServiceWindow(ModelStore modelStore, SettingsAccessor settingsAccessor)
        {
            _settingsAccessor = settingsAccessor;
            _modelStore = modelStore;
            InitializeComponent();
        }

        public Service Service;

        private void ServiceWindow_Load(object sender, EventArgs e)
        {
            //create Service if null
            if (Service == null)
            {
                var date = DateTime.Now;
                var name = "Nienazwana usługa";
                Service = new Service
                {
                    CreationDate = date, ModificationDate = date, Name = name,
                    Tag = name,
                    Id = _modelStore.NewServiceId()
                };
            }

            //setup comboboxes
            var editorSettings = _settingsAccessor.GetSettings();
            foreach (string value in editorSettings.Properties_Vat)
            {
                CBVat.Items.Add(value);
                if (CBVat.Items.Count > 0) CBVat.SelectedIndex = 0;
                else CBVat.Text = "0";
            }

            foreach (string value in editorSettings.Properties_Unit)
            {
                CBJm.Items.Add(value);
            }

            //load data
            TBName.Text = Service.Name;
            TBTag.Text = Service.Tag;
            nUDPrice.Value = (decimal)Service.Price;
            CBVat.Text = Service.Vat.ToString();
            CBJm.Text = Service.Jm;
        }

        private void SaveData()
        {
            Service.Vat = int.Parse(CBVat.Text);
            Service.Jm = CBJm.Text;
            Service.Name = TBName.Text;
            Service.Price = (float)nUDPrice.Value;
            Service.Tag = TBTag.Text;

            Service.ModificationDate = DateTime.Now;
        }

        private void BOk_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch
            {
                MessageBox.Show("Błąd!");
                return;
            }

            if (_modelStore.Services.Find(n => n.Id == Service.Id) == null)
            {
                _modelStore.Services.Add(Service);
            }

            if (MainForm.Instance.ServicesListWindow != null && !MainForm.Instance.ServicesListWindow.IsDisposed)
            {
                MainForm.Instance.ServicesListWindow.Reload();
            }
            Close();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
