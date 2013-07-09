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
    public partial class StatsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public class CompanyStatisticData
        {
            int DocumentsTotal = 0;
            int DocumentsPaid = 0;
            int DocumentsNotPaid = 0;

            float MoneyTotal = 0;
            float MoneyPaid = 0;
            float MoneyNotPaid = 0;

            string CompanyTag = "";

            public CompanyStatisticData()
            {
                CompanyTag = "Razem:";
            }
            public CompanyStatisticData(Classes.Company Source)
            {
                CompanyTag = Source.Tag;

                foreach (Classes.Document Document in MainForm.Instance.Documents)
                {
                    if (Document.CompanyID != Source.ID) continue;

                    DocumentsTotal++;
                    MoneyTotal += Document.MoneyData.Brutto;
                    if (Document.Paid)
                    {
                        DocumentsPaid++;
                        MoneyPaid += Document.MoneyData.Brutto;
                    }
                    else
                    {
                        DocumentsNotPaid++;
                        MoneyNotPaid += Document.MoneyData.Brutto;
                    }
                }
            }

            public ListViewItem GetListViewItem()
            {
                ListViewItem NewItem = new ListViewItem();

                NewItem.SubItems[0].Text = CompanyTag;

                NewItem.SubItems.Add(DocumentsTotal.ToString());
                NewItem.SubItems.Add(DocumentsPaid.ToString());
                NewItem.SubItems.Add(DocumentsNotPaid.ToString());

                NewItem.SubItems.Add(MoneyPaid.ToString());
                NewItem.SubItems.Add(MoneyNotPaid.ToString());
                NewItem.SubItems.Add(MoneyTotal.ToString());

                return NewItem;
            }

            public void IncreaseBy(CompanyStatisticData Data)
            {
                DocumentsTotal += Data.DocumentsTotal;
                DocumentsPaid += Data.DocumentsPaid;
                DocumentsNotPaid += Data.DocumentsNotPaid;

                MoneyTotal += Data.MoneyTotal;
                MoneyPaid += Data.MoneyPaid;
                MoneyNotPaid += Data.MoneyNotPaid;
            }

        }

        Dictionary<Classes.Company, CompanyStatisticData> Companies = new Dictionary<Faktury.Classes.Company, CompanyStatisticData>();
        public StatsWindow(List<Classes.Company> Source)
        {
            InitializeComponent();

            CompanyStatisticData Final = new CompanyStatisticData();
            foreach(Classes.Company Company in Source)
            {
                CompanyStatisticData Data = new CompanyStatisticData(Company);
                Final.IncreaseBy(Data);

                Companies.Add(Company, Data);
                LVStats.Items.Add(Data.GetListViewItem());
            }
            LVStats.Items.Add(Final.GetListViewItem());

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
