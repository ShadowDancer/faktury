using System;
using System.Globalization;
using System.Windows.Forms;
using Faktury.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class ServicesListWindow : DockContent
    {
        private readonly ModelStore _modelStore;

        public ServicesListWindow(ModelStore modelStore)
        {
            _modelStore = modelStore;
            InitializeComponent();
        }

        public void Reload()
        {
            LVServices.Items.Clear();
            foreach (Service service in _modelStore.Services)
            {
                LVServices.Items.Add(new ListViewItem(new[] {service.Id.ToString(), service.Name, service.Jm, service.Price.ToString(CultureInfo.CurrentCulture)} ));
            }
        }

        private void MaterialListWindow_Activated(object sender, EventArgs e)
        {
            Reload();
        }

        private void MaterialListWindow_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void MaterialListWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Instance.usługiToolStripMenuItem.Checked = false;
        }

        #region ContextMenu

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.Instance.AddService();
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVServices.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in LVServices.SelectedItems)
                {
                    var serviceToEdit = _modelStore.Services.Find(n => n.Id == int.Parse(item.SubItems[0].Text));

                    if (serviceToEdit != null)
                        MainForm.Instance.EditService(serviceToEdit);
                    else
                        MessageBox.Show("Nie znaleziono usługi " + item.SubItems[1].Text + "!");
                }
            }
            else MessageBox.Show("Wybierz z listy usługi do edycji!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVServices.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in LVServices.SelectedItems)
                {
                    var serviceToEdit = _modelStore.Services.Find(n => n.Id == int.Parse(item.SubItems[0].Text));

                    if (serviceToEdit != null)
                        MainForm.Instance.DeleteService(serviceToEdit);
                    else
                        MessageBox.Show("Nie znaleziono usługi " + item.SubItems[1].Text + "!");
                }
            }
            else MessageBox.Show("Wybierz z listy usługi do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void odświeżToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
