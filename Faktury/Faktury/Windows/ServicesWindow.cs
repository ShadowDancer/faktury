using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Faktury.Windows
{
    public partial class ServiceWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ServiceWindow()
        {
            InitializeComponent();
        }

        public Classes.Service Service = null;

        private void ServiceWindow_Load(object sender, EventArgs e)
        {
            //create Service if null
            if (Service == null)
            {
                Service = new Faktury.Classes.Service();
                Service.CreationDate = DateTime.Now;
                Service.ModificationDate = Service.CreationDate;

                Service.Name = "Nienazwana usługa";
                Service.Tag = Service.Name;
                Service.ID = MainForm.Instance.GetNewServiceID;
            }

            //setup comboboxes
            foreach (string Value in MainForm.Instance.Settings.Properties_Vat)
            {
                CBVat.Items.Add(Value);
                if (CBVat.Items.Count > 0) CBVat.SelectedIndex = 0;
                else CBVat.Text = "0";
            }

            foreach (string Value in MainForm.Instance.Settings.Properties_Unit)
            {
                CBJm.Items.Add(Value);
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

                Classes.Service Check = MainForm.Instance.Services.Find(n => n.ID == Service.ID);
                if (Check == null)
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
