using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Faktury.Windows
{
    public partial class StatsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public class CompanyStatisticData
        {
            private int _documentsTotal;
            private int _documentsPaid;
            private int _documentsNotPaid;

            private float _moneyTotal;
            private float _moneyPaid;
            private float _moneyNotPaid;

            private readonly string _companyTag = "";

            public CompanyStatisticData()
            {
                _companyTag = "Razem:";
            }
            public CompanyStatisticData(Classes.Company source)
            {
                _companyTag = source.Tag;

                foreach (Classes.Document document in MainForm.Instance.Documents)
                {
                    if (document.CompanyId != source.Id) continue;

                    _documentsTotal++;
                    _moneyTotal += document.MoneyData.Brutto;
                    if (document.Paid)
                    {
                        _documentsPaid++;
                        _moneyPaid += document.MoneyData.Brutto;
                    }
                    else
                    {
                        _documentsNotPaid++;
                        _moneyNotPaid += document.MoneyData.Brutto;
                    }
                }
            }

            public ListViewItem GetListViewItem()
            {
                ListViewItem newItem = new ListViewItem();

                newItem.SubItems[0].Text = _companyTag;

                newItem.SubItems.Add(_documentsTotal.ToString());
                newItem.SubItems.Add(_documentsPaid.ToString());
                newItem.SubItems.Add(_documentsNotPaid.ToString());

                newItem.SubItems.Add(_moneyPaid.ToString());
                newItem.SubItems.Add(_moneyNotPaid.ToString());
                newItem.SubItems.Add(_moneyTotal.ToString());

                return newItem;
            }

            public void IncreaseBy(CompanyStatisticData data)
            {
                _documentsTotal += data._documentsTotal;
                _documentsPaid += data._documentsPaid;
                _documentsNotPaid += data._documentsNotPaid;

                _moneyTotal += data._moneyTotal;
                _moneyPaid += data._moneyPaid;
                _moneyNotPaid += data._moneyNotPaid;
            }

        }

        private readonly Dictionary<Classes.Company, CompanyStatisticData> _companies = new Dictionary<Classes.Company, CompanyStatisticData>();
        public StatsWindow(List<Classes.Company> source)
        {
            InitializeComponent();

            CompanyStatisticData final = new CompanyStatisticData();
            foreach(Classes.Company company in source)
            {
                CompanyStatisticData data = new CompanyStatisticData(company);
                final.IncreaseBy(data);

                _companies.Add(company, data);
                LVStats.Items.Add(data.GetListViewItem());
            }
            LVStats.Items.Add(final.GetListViewItem());

        }

        private void LVStats_DoubleClick(object sender, EventArgs e)
        {
            if (LVStats.SelectedItems.Count > 0)
            {
                if (!MainForm.Instance.dokumentyToolStripMenuItem.Checked)
                {
                    MainForm.Instance.dokumentyToolStripMenuItem.Checked = true;
                    MainForm.Instance.dokumentyToolStripMenuItem_Click(null, null);
                }

                MainForm.Instance.DocumentListWindow.Activate();
                MainForm.Instance.DocumentListWindow.ShowUnpaidDocuments(LVStats.Items[0].SubItems[0].Text);

            }
        }
    }
}
