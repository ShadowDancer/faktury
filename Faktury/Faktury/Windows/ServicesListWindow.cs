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
    public partial class ServicesListWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ServicesListWindow()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            LVServices.Items.Clear();
            foreach (Classes.Service Service in MainForm.Instance.Services)
            {
                LVServices.Items.Add(new ListViewItem(new string[] {Service.ID.ToString(), Service.Name.ToString(), Service.Jm.ToString(), Service.Price.ToString()} ));
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
            MainForm.Instance.addService();
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVServices.SelectedItems.Count > 0)
            {
                foreach (ListViewItem Item in LVServices.SelectedItems)
                {
                    Classes.Service ServiceToEdit = null;
                    ServiceToEdit = MainForm.Instance.Services.Find(n => n.ID == int.Parse(Item.SubItems[0].Text));

                    if (ServiceToEdit != null)
                        MainForm.Instance.editService(ServiceToEdit);
                    else
                        MessageBox.Show("Nie znaleziono usługi " + Item.SubItems[1].Text + "!");
                }
            }
            else MessageBox.Show("Wybierz z listy usługi do edycji!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVServices.SelectedItems.Count > 0)
            {
                foreach (ListViewItem Item in LVServices.SelectedItems)
                {
                    Classes.Service ServiceToEdit = null;
                    ServiceToEdit = MainForm.Instance.Services.Find(n => n.ID == int.Parse(Item.SubItems[0].Text));

                    if (ServiceToEdit != null)
                        MainForm.Instance.deleteService(ServiceToEdit);
                    else
                        MessageBox.Show("Nie znaleziono usługi " + Item.SubItems[1].Text + "!");
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
