using System;
using System.Windows.Forms;
using Faktury.Domain.Data.Repository;
using Faktury.Domain.Domain;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class ServiceWindow : DockContent
    {
        private readonly SettingsRepository _settingsRepository;
        private readonly ModelStore _modelStore;

        public ServiceWindow(ModelStore modelStore, SettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
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
                    Id = _modelStore.ServiceRepository.NewServiceId()
                };
            }

            //setup comboboxes
            var editorSettings = _settingsRepository.GetSettings();
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
            nUDPrice.Value = Service.PriceNet;
            CBVat.Text = Service.Vat.ToString();
            CBJm.Text = Service.Unit;
            TBpkwiu.Text = Service.PKWiU;
        }

        private void SaveData()
        {
            Service.Vat = int.Parse(CBVat.Text);
            Service.Unit = CBJm.Text;
            Service.Name = TBName.Text;
            Service.PriceNet = nUDPrice.Value;
            Service.Tag = TBTag.Text;
            Service.PKWiU = TBpkwiu.Text;
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

            if (_modelStore.ServiceRepository.FindService(Service.Id) == null)
            {
                _modelStore.ServiceRepository.AddService(Service);
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
