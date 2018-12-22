using System;
using System.Windows.Forms;

namespace Faktury.Windows
{
    public partial class ServicesListWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ServicesListWindow()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            LVServices.Items.Clear();
            foreach (Classes.Service service in MainForm.Instance.Services)
            {
                LVServices.Items.Add(new ListViewItem(new string[] {service.Id.ToString(), service.Name.ToString(), service.Jm.ToString(), service.Price.ToString()} ));
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
                    Classes.Service serviceToEdit = null;
                    serviceToEdit = MainForm.Instance.Services.Find(n => n.Id == int.Parse(item.SubItems[0].Text));

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
                    Classes.Service serviceToEdit = null;
                    serviceToEdit = MainForm.Instance.Services.Find(n => n.Id == int.Parse(item.SubItems[0].Text));

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
