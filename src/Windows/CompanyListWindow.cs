using System;
using System.Globalization;
using System.Windows.Forms;
using Faktury.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class CompanyListWindow : DockContent
    {
        private readonly ModelStore _modelStore;
        private ListViewColumnSorter _lvwColumnSorter;
        private string _filter;

        public CompanyListWindow(ModelStore modelStore)
        {
            _modelStore = modelStore;
            InitializeComponent();
        }

        private void Reload()
        {
            LVCompanies.Items.Clear();
            foreach(Company currentCompany in _modelStore.Companies)
            {
                ListViewItem newItem = new ListViewItem(new[]
                {
                    currentCompany.Id.ToString(),
                    currentCompany.ShortName,
                    currentCompany.Name,
                    " " + currentCompany.Nip,
                    currentCompany.CreationDate.ToString(CultureInfo.CurrentCulture),
                    currentCompany.ModificationDate.ToString(CultureInfo.CurrentCulture)
                });

                if (!string.IsNullOrEmpty(_filter))
                {
                    if (
                        currentCompany.Name.ToLower().Contains(_filter) ||
                        currentCompany.Owner.ToLower().Contains(_filter) ||
                        currentCompany.Address.ToLower().Contains(_filter) ||
                        currentCompany.Street.ToLower().Contains(_filter) ||
                        currentCompany.Nip.ToLower().Contains(_filter) ||
                        currentCompany.ShortName.ToLower().Contains(_filter))
                    {
                        LVCompanies.Items.Add(newItem);
                    }
                }
                else
                {
                    LVCompanies.Items.Add(newItem);
                }
            }
        }

        private void CompanyListWindow_Activated(object sender, EventArgs e)
        {
            Reload();
        }

        private void CompanyListWindow_Load(object sender, EventArgs e)
        {
            _lvwColumnSorter = new ListViewColumnSorter {SortColumn = 1};
            LVCompanies.ListViewItemSorter = _lvwColumnSorter;

            Reload();
        }

        private void CompanyListWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance.companiesToolStripMenuItem.Checked = false;
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
            MainForm.Instance.AddCompany();
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVCompanies.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in LVCompanies.SelectedItems)
                {
                    var companyToEdit = _modelStore.FindCompany(int.Parse(item.SubItems[0].Text));

                    if (companyToEdit != null)
                        MainForm.Instance.EditCompany(companyToEdit);
                    else
                        MessageBox.Show("Nie znaleziono kontrahenta " + item.SubItems[1].Text + "!");
                }
            }
            else MessageBox.Show("Wybierz z listy kontrahentów do edycji!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LVCompanies.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in LVCompanies.SelectedItems)
                {
                    var companyToEdit = _modelStore.FindCompany(int.Parse(item.SubItems[0].Text));
                    if (companyToEdit != null)
                        MainForm.Instance.DeleteCompany(companyToEdit);
                    else
                        MessageBox.Show("Nie znaleziono kontrahenta " + item.SubItems[1].Text + "!");

                    Reload();
                }
            }
            else MessageBox.Show("Wybierz z listy kontrahentów do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        private void LVCompanies_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            _filter = tbFilter.Text.ToLower();
            Reload();
        }

    }
}
