using System;
using System.Windows.Forms;

namespace Faktury.Windows
{
    public partial class ServiceWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ServiceWindow()
        {
            InitializeComponent();
        }

        public Classes.Service Service;

        private void ServiceWindow_Load(object sender, EventArgs e)
        {
            //create Service if null
            if (Service == null)
            {
                Service = new Classes.Service();
                Service.CreationDate = DateTime.Now;
                Service.ModificationDate = Service.CreationDate;

                Service.Name = "Nienazwana usługa";
                Service.Tag = Service.Name;
                Service.Id = MainForm.Instance.GetNewServiceId;
            }

            //setup comboboxes
            foreach (string value in MainForm.Instance.Settings.PropertiesVat)
            {
                CBVat.Items.Add(value);
                if (CBVat.Items.Count > 0) CBVat.SelectedIndex = 0;
                else CBVat.Text = "0";
            }

            foreach (string value in MainForm.Instance.Settings.PropertiesUnit)
            {
                CBJm.Items.Add(value);
            }

            //load data
            TBName.Text = Service.Name;
            TBTag.Text = Service.Tag;
            nUDPrice.Value = (decimal)Service.Price;
            CBVat.Text = Service.Vat.ToString();
            CBJm.Text = Service.Jm.ToString();
        }

        public void SaveData()
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

                Classes.Service check = MainForm.Instance.Services.Find(n => n.Id == Service.Id);
                if (check == null)
                    MainForm.Instance.Services.Add(Service);


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
