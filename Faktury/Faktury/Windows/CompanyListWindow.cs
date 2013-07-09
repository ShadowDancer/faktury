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
    public partial class CompanyListWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ListViewColumnSorter lvwColumnSorter;
        private string Filter;

        public CompanyListWindow()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            LVCompanies.Items.Clear();
            foreach(Classes.Company CurrentCompany in MainForm.Instance.Companies)
            {


                ListViewItem NewItem = new ListViewItem(new string[] {CurrentCompany.ID.ToString(), CurrentCompany.Tag, CurrentCompany.Name, " " + CurrentCompany.Nip, CurrentCompany.CreationDate.ToString(), CurrentCompany.ModificationDate.ToString()});

                if (!string.IsNullOrEmpty(Filter))
                {
                    if (
                        CurrentCompany.Name.ToLower().Contains(Filter) ||
                        CurrentCompany.Owner.ToLower().Contains(Filter) ||
                        CurrentCompany.Adress.ToLower().Contains(Filter) ||
                        CurrentCompany.Street.ToLower().Contains(Filter) ||
                        CurrentCompany.Nip.ToLower().Contains(Filter) ||
                        CurrentCompany.Tag.ToLower().Contains(Filter))
                    {
                        LVCompanies.Items.Add(NewItem);
                    }
                }
                else
                {
                    LVCompanies.Items.Add(NewItem);
                }
            }
        }

        private void CompanyListWindow_Activated(object sender, EventArgs e)
        {
            Reload();
        }

        private void CompanyListWindow_Load(object sender, EventArgs e)
        {
            lvwColumnSorter = new ListViewColumnSorter();
            LVCompanies.ListViewItemSorter = lvwColumnSorter;

            Reload();
        }

        private void CompanyListWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.kontrahenciToolStripMenuItem.Checked = false;
        }

        #region ContextMenu

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void odświeżToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.Instance.addCompany();
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVCompanies.SelectedItems.Count > 0)
            {
                foreach (ListViewItem Item in LVCompanies.SelectedItems)
                {
                    Classes.Company CompanyToEdit = null;
                    CompanyToEdit = MainForm.Instance.Companies.Find(n => n.ID == int.Parse(Item.SubItems[0].Text));

                    if (CompanyToEdit != null)
                        MainForm.Instance.editCompany(CompanyToEdit);
                    else
                        MessageBox.Show("Nie znaleziono kontrahenta " + Item.SubItems[1].Text + "!");
                }
            }
            else MessageBox.Show("Wybierz z listy kontrahentów do edycji!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVCompanies.SelectedItems.Count > 0)
            {
                foreach (ListViewItem Item in LVCompanies.SelectedItems)
                {
                    Classes.Company CompanyToEdit = null;
                    CompanyToEdit = MainForm.Instance.Companies.Find(n => n.ID == int.Parse(Item.SubItems[0].Text));

                    if (CompanyToEdit != null)
                        MainForm.Instance.deleteCompany(CompanyToEdit);
                    else
                        MessageBox.Show("Nie znaleziono kontrahenta " + Item.SubItems[1].Text + "!");

                    Reload();
                }
            }
            else MessageBox.Show("Wybierz z listy kontrahentów do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        private void statystykiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVCompanies.SelectedItems.Count > 0)
            {
                List<Classes.Company> Companies = new List<Faktury.Classes.Company>();
                foreach (ListViewItem CurrentItem in LVCompanies.SelectedItems)
                {
                    try
                    {
                        Classes.Company Company = MainForm.Instance.Companies.Find(n => n.ID == Convert.ToInt32(CurrentItem.SubItems[0].Text));
                        Companies.Add(Company);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Błąd otwierania plików:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                StatsWindow NewWindow = new StatsWindow(Companies);
                NewWindow.Show(MainForm.Instance.MainDockPanel);
            }
            else MessageBox.Show("Wybierz z listy kontrachentów do uwzględnienia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LVCompanies_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            Filter = tbFilter.Text.ToLower();
            Reload();
        }

    }
}
