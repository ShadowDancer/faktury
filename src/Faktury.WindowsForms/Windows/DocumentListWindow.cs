﻿using System;
using System.Globalization;
using System.Windows.Forms;
using Faktury.Domain.Data.Repository;
using Faktury.Domain.Domain;
using Faktury.Print_Framework;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class DocumentListWindow : DockContent
    {
        private readonly SettingsRepository _settingsRepository;
        private readonly ModelStore _modelStore;
        private readonly PrintEngine _printEngine;

        public DocumentListWindow(ModelStore modelStore, PrintEngine printEngine, SettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _modelStore = modelStore;
            _printEngine = printEngine;
            InitializeComponent();
        }

        private void DocumentListWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Instance.documentListToolStripMenuItem.Checked = false;
        }

        private ListViewItem GetListViewItemFromDocument(Document document)
        {
            ListViewItem newItem = new ListViewItem(document.Number.ToString());
            newItem.SubItems.Add(document.Year.ToString());
            newItem.SubItems.Add(document.Customer.ShortName ?? "");
            newItem.SubItems.Add(document.IssueDate.ToString("d", CultureInfo.CurrentCulture));
            newItem.SubItems.Add(document.DocumentSummary.TotalNet.ToString("C", CultureInfo.CurrentCulture));
            newItem.SubItems.Add(document.ReverseVat ? "TAK" : "NIE");

            return newItem;
        }

        public void UpdateCompanyCombobox()
        {
            MainForm.Instance.ReloadCompanyCombobox(CBCompanyTag);
        }

        private void Reload()
        {
            LVDocuments.Items.Clear();
            foreach (var currentDocument in _modelStore.DocumentRepository.Documents)
            {
                    //throw exception if something is wrong
                    if(cBYearFilter.Checked)
                        if (currentDocument.Year != nUDYear.Value) continue;

                    if (cBDateFilter.Checked)
                    {
                        DateTime checkDate = currentDocument.IssueDate;

                        if (RBYoungerThan.Checked && checkDate.CompareTo(DTPDateFilter.Value) >= 0) continue;
                        if (RBFromDay.Checked && checkDate.CompareTo(DTPDateFilter.Value) != 0) continue;
                        if (RBOlderThan.Checked && checkDate.CompareTo(DTPDateFilter.Value) <= 0) continue;
                    }

                    if (CxBCompanyTagFilter.Checked)
                    {
                        if (currentDocument.Customer.Id != ((ComboBoxItem)CBCompanyTag.SelectedItem).Id) continue;
                    }
                    
                    //if no exceptions add item
                    ListViewItem newItem = GetListViewItemFromDocument(currentDocument);
                    if(newItem != null)
                        LVDocuments.Items.Add(newItem);
            }
        }

        private void AutoReload()
        {
            if (cBAutoRefreshList.Checked) Reload();
        }

        private void DocumentList_Activated(object sender, EventArgs e)
        {
            Reload();
        }

        private void OpenDocument_Load(object sender, EventArgs e)
        {
            MainForm.Instance.ReloadCompanyCombobox(CBCompanyTag);

            _lvwColumnSorter = new ListViewColumnSorter {SortByPrev = true, SortColumn = 1, Order = SortOrder.Descending};
            LVDocuments.ListViewItemSorter = _lvwColumnSorter;
            LVDocuments.Sort();

            #region InitButtons
            //main filters checkboxes
            var editorSettings = _settingsRepository.GetSettings();
            CxBCompanyTagFilter.Checked = editorSettings.DocumentFilterCompany;
            CxBCompanyTagFilter_CheckedChanged(null, null);
            cBYearFilter.Checked = editorSettings.DocumentFilterYear;
            cBYearFilter_CheckedChanged(null, null);

            nUDYear.Value = editorSettings.DocumentFilterYearValue;
            foreach(ComboBoxItem item in CBCompanyTag.Items)
            {
                if (item.Id == editorSettings.DocumentFilterCompanyValue)
                {
                    CBCompanyTag.SelectedItem = item;
                    break;
                }
            }
            
            //date checkbox
            groupBoxDateFilter.Enabled = cBDateFilter.Checked;
            DTPDateFilter.Value = editorSettings.DocumentFilterDateTime;

            //date radiobutons
            RBFromDay.Checked = editorSettings.DocumentFilterDateNow;
            RBFromDay_CheckedChanged(null, null);
            RBOlderThan.Checked = editorSettings.DocumentFilterDateOlder;
            RBOlderThan_CheckedChanged(null, null);
            RBYoungerThan.Checked = editorSettings.DocumentFilterDateYounger;
            RBYoungerThan_CheckedChanged(null, null);


            //context menu buttons

            //filtry
            pokażFiltryToolStripMenuItem.Checked = editorSettings.DocumentShowFilters;
            pokażFiltryToolStripMenuItem_Click(null, null);
            //autouzupełnianie
            cBAutoRefreshList.Checked = editorSettings.DocumentAutoRefresh;
            cBAutoRefreshList_Click(null, null);

            #endregion

            Reload();
        }

        #region Filters

        #region Contol
        private void RBYoungerThan_CheckedChanged(object sender, EventArgs e)
        {
            _settingsRepository.GetSettings().DocumentFilterDateYounger = RBYoungerThan.Checked;
            AutoReload();
        }

        private void RBFromDay_CheckedChanged(object sender, EventArgs e)
        {
            _settingsRepository.GetSettings().DocumentFilterDateNow = RBFromDay.Checked;
            AutoReload();
        }

        private void RBOlderThan_CheckedChanged(object sender, EventArgs e)
        {
            _settingsRepository.GetSettings().DocumentFilterDateOlder = RBOlderThan.Checked;
            AutoReload();
        }

        private void cBYearFilter_CheckedChanged(object sender, EventArgs e)
        {
            nUDYear.Enabled = cBYearFilter.Checked;
            _settingsRepository.GetSettings().DocumentFilterYear = cBYearFilter.Checked;
            AutoReload();
        }

        private void cBDate_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDateFilter.Enabled = cBDateFilter.Checked;
            _settingsRepository.GetSettings().DocumentFilterDate = cBDateFilter.Checked;
            AutoReload();
        }
        #endregion
        private void CxBCompanyTagFilter_CheckedChanged(object sender, EventArgs e)
        {
            CBCompanyTag.Enabled = CxBCompanyTagFilter.Checked;
            _settingsRepository.GetSettings().DocumentFilterCompany = CxBCompanyTagFilter.Checked;
            AutoReload();
        }
        
        #endregion

        #region AutoRefresh

        private void nUDYear_ValueChanged(object sender, EventArgs e)
        {
            var editorSettings = _settingsRepository.GetSettings();
            editorSettings.DocumentFilterYearValue = (int)nUDYear.Value;
            AutoReload();
        }
        
        private void CBCompanyTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            var editorSettings = _settingsRepository.GetSettings();
            editorSettings.DocumentFilterCompanyValue = ((ComboBoxItem)CBCompanyTag.SelectedItem).Id;
            AutoReload();
        }
        
        private void DTPDateFilter_ValueChanged(object sender, EventArgs e)
        {
            var editorSettings = _settingsRepository.GetSettings();
            editorSettings.DocumentFilterDateTime = DTPDateFilter.Value;
            AutoReload();
        }
        
        #endregion

        #region ContextMenu

        private void BRemoveSelected_Click(object sender, EventArgs e)
        {
            if (LVDocuments.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Na pewno chcesz usunąć wybrany(e) dokument(y)?", "Usuwanie dokumentów...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    foreach (ListViewItem currentItem in LVDocuments.SelectedItems)
                    {
                        try
                        {
                            _modelStore.DocumentRepository.Documents.Remove(_modelStore.DocumentRepository.FindDocument(Convert.ToInt32(currentItem.Text), Convert.ToInt32(currentItem.SubItems[1].Text)));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Błąd podczas usuwania plików:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    Reload();
                }
            }
            else MessageBox.Show("Wybierz z listy dokumenty do usunięcia!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BOpenSelected_Click(object sender, EventArgs e)
        {
            if (LVDocuments.SelectedItems.Count > 0)
            {
                    foreach (ListViewItem currentItem in LVDocuments.SelectedItems)
                    {
                        try
                        {
                            MainForm.Instance.OpenDocument(_modelStore.DocumentRepository.FindDocument(Convert.ToInt32(currentItem.Text), Convert.ToInt32(currentItem.SubItems[1].Text)));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Błąd otwierania plików:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
            }
            else MessageBox.Show("Wybierz z listy dokumenty do edycji!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BZamknij_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.Instance.NewDocument(null, null);
        }

        private void BRefreshList_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void pokażFiltryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = pokażFiltryToolStripMenuItem.Checked;
            _settingsRepository.GetSettings().DocumentShowFilters = pokażFiltryToolStripMenuItem.Checked;
        }

        private void cBAutoRefreshList_Click(object sender, EventArgs e)
        {
            _settingsRepository.GetSettings().DocumentAutoRefresh = cBAutoRefreshList.Checked;
            AutoReload();
        }

        #endregion


        public void ShowPreview()
        {
            if (LVDocuments.SelectedItems.Count > 0)
            {
                    foreach (ListViewItem currentItem in LVDocuments.SelectedItems)
                    {
                        try
                        {
                            var document = _modelStore.DocumentRepository.FindDocument(Convert.ToInt32(currentItem.Text), Convert.ToInt32(currentItem.SubItems[1].Text));
                            new DocumentPrinter(document).ShowPreview(_printEngine);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Błąd otwierania plików:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
            }
            else MessageBox.Show("Wybierz z listy dokumenty do podglądu!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Print()
        {
            if (LVDocuments.SelectedItems.Count > 0)
            {
                foreach (ListViewItem currentItem in LVDocuments.SelectedItems)
                {
                    try
                    {
                        var document = _modelStore.DocumentRepository.FindDocument(Convert.ToInt32(currentItem.Text), Convert.ToInt32(currentItem.SubItems[1].Text));
                        new DocumentPrinter(document).Print(_printEngine);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Błąd otwierania plików:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else MessageBox.Show("Wybierz z listy dokumenty do druku!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private ListViewColumnSorter _lvwColumnSorter;

        private void LVDocuments_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            

           ListView myListView = (ListView)sender;

           // Determine if clicked column is already the column that is being sorted.
           if ( e.Column == _lvwColumnSorter.SortColumn )
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
    }
}
