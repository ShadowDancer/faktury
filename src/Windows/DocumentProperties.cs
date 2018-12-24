using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Faktury.Classes;
using Faktury.Print_Framework;
using ListViewEx;

namespace Faktury.Windows
{
    public partial class DocumentProperties : UserControl
    {
        /// <summary>
        /// Injected as runtime, because controls cannot use constructor injection
        /// </summary>
        public ModelStore ModelStore { get; set; }

        public DocumentProperties()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            float totalNetto = 0;
            float totalBrutto = 0;
            float totalVat = 0;

            for (int i = 0; i < LVEServices.Items.Count; i++)
            {
                try
                {
                    //ID
                    LVEServices.Items[i].Text = (i + 1).ToString();

                    //Wartość Netto
                    float netto = (float)Math.Round((Convert.ToSingle(LVEServices.Items[i].SubItems[2].Text) * Convert.ToSingle(LVEServices.Items[i].SubItems[3].Text)), 2);
                    totalNetto += netto;
                    LVEServices.Items[i].SubItems[5].Text = netto.ToString("0.00");

                    //vat
                    float vat = (float)Math.Round(netto * (float.Parse(LVEServices.Items[i].SubItems[8].Text) / 100), 2);
                    totalVat += vat;
                    LVEServices.Items[i].SubItems[6].Text = vat.ToString("0.00");

                    //Wartość Brutto
                    float brutto = (float)Math.Round(netto + vat, 2);
                    totalBrutto += brutto;
                    LVEServices.Items[i].SubItems[7].Text = brutto.ToString("0.00");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Wiersz " + (i + 1), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            totalNetto = (float)Math.Round(totalNetto, 2);
            totalVat = (float)Math.Round(totalVat, 2);
            totalBrutto = (float)Math.Round(totalBrutto, 2);

            TBTotalNetto.Text = totalNetto.ToString("0.00");
            TBTotalVAT.Text = totalVat.ToString("0.00");
            TBTotalBrutto.Text = totalBrutto.ToString("0.00");

            TBSlownie.Text = NumberToWordConventer.ConvertValues(totalBrutto);

        }

        public bool Changed;

        //Wczytuje kontrolki i rekordy
        public void Initialize(Document document)
        {
            //load combo box
            foreach (Service currentService in ModelStore.Services)
            {
                CBService.Items.Add(new ComboBoxItem(currentService.Tag, currentService.Id));
            }
            if (CBService.Items.Count > 0) CBService.SelectedIndex = 0;

            foreach (var currentRecord in document.Items)
            {
                ListViewItem newItem = new ListViewItem();
                newItem.SubItems.Add(currentRecord.Name);
                newItem.SubItems.Add(currentRecord.Cost.ToString(CultureInfo.CurrentCulture));
                newItem.SubItems.Add(currentRecord.Count.ToString(CultureInfo.CurrentCulture));
                newItem.SubItems.Add(currentRecord.Unit);
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add(currentRecord.VatPercent.ToString(CultureInfo.CurrentCulture));
                
                LVEServices.Items.Add(newItem);
            }
            Reload();
        }

        //Zapisuje dane
        public void Save(Document document)
        {
            DocumentSummary documentSummary = document.DocumentSummary;
            documentSummary.Brutto =  Convert.ToSingle(TBTotalBrutto.Text);
            documentSummary.Netto =  Convert.ToSingle(TBTotalNetto.Text);
            documentSummary.TotalVat =  Convert.ToSingle(TBTotalVAT.Text);

            documentSummary.InWords = TBSlownie.Text;

            document.Items.Clear();
            foreach (ListViewItem currentItem in LVEServices.Items)
            {
                DocumentItem newRecord = new DocumentItem
                {
                    Name = currentItem.SubItems[1].Text,
                    Cost = Convert.ToSingle(currentItem.SubItems[2].Text),
                    Count = Convert.ToSingle(currentItem.SubItems[3].Text),
                    Unit = currentItem.SubItems[4].Text,
                    Netto = Convert.ToSingle(currentItem.SubItems[5].Text),
                    Vat = Convert.ToSingle(currentItem.SubItems[6].Text),
                    Brutto = Convert.ToSingle(currentItem.SubItems[7].Text),
                    VatPercent = Convert.ToSingle(currentItem.SubItems[8].Text)
                };

                document.Items.Add(newRecord);
            }
        }

        #region Extended List View
        private void LVEServices_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (e.SubItem > 0 && e.SubItem < 5)
                LVEServices.StartEditing(TBListViewExTB, e.Item, e.SubItem);

            if (e.SubItem == 8)
                LVEServices.StartEditing(TBListViewExTB, e.Item, e.SubItem);
        }

        private void TBListViewExTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                LVEServices.EndEditing(true);
            }
        }

        private void TBListViewExTB_VisibleChanged(object sender, EventArgs e)
        {
            if (TBListViewExTB.Visible == false) Reload();
            Changed = true;
        }
        #endregion

        #region ContextMenu


        private void RecordAdd_Click(object sender, EventArgs e)
        {
            Service check;
            try
            {
                check = ModelStore.Services.First(n => n.Id == ((ComboBoxItem)CBService.SelectedItem).Id);

            }
            catch
            {
                MessageBox.Show("Wybierz szablon usługi z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Changed = true;
            ListViewItem newItem = new ListViewItem("");
            {
                newItem.SubItems.Add(check.Name);
                newItem.SubItems.Add(check.Price.ToString(CultureInfo.CurrentCulture));
                newItem.SubItems.Add("0");
                newItem.SubItems.Add(check.Jm);
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add(check.Vat.ToString());
            }

            LVEServices.Items.Add(newItem);
            Reload();
        }

        private void RecordEdit_Click(object sender, EventArgs e)
        {
            Service check;
            try
            {
                check = ModelStore.Services.Find(n => n.Id == ((ComboBoxItem)CBService.SelectedItem).Id);
                if (check == null) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Wybierz szablon usługi z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Changed = true;
            if (LVEServices.SelectedItems.Count > 0)
            {
                LVEServices.SelectedItems[0].SubItems[1].Text = check.Name;
                LVEServices.SelectedItems[0].SubItems[2].Text = check.Price.ToString(CultureInfo.CurrentCulture);
                LVEServices.SelectedItems[0].SubItems[4].Text = check.Jm;
                LVEServices.SelectedItems[0].SubItems[8].Text = check.Vat.ToString();

                Reload();
            }
            else
            {
                MessageBox.Show("Wybierz usługę do edycji z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RecordDelete_Click(object sender, EventArgs e)
        {
            if (LVEServices.SelectedItems.Count > 0)
            {
                foreach (ListViewItem currentItem in LVEServices.SelectedItems)
                {
                    LVEServices.Items.Remove(currentItem);
                }
                ModelStore.UpdateHighestDocumentId();
                Reload();
                Changed = true;
            }
            else
            {
                MessageBox.Show("Wybierz usługę do usunięcia z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecordDeleteAll_Click(object sender, EventArgs e)
        {
            Changed = true;
            LVEServices.Items.Clear();
            ModelStore.UpdateHighestDocumentId();
        }

        #endregion
    }
}
