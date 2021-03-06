﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Faktury.Domain.Business;
using Faktury.Domain.Data.Repository;
using Faktury.Domain.Domain;
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


        private bool reverseVAT { get; set; }

        /// <summary>
        /// Notifies control that reverse VAT property has changed
        /// </summary>
        public void ReverseVATChanged(bool newValue)
        {
            reverseVAT = newValue;
            Reload();
        }

        private void Reload()
        {
            decimal totalNetto = 0;
            decimal totalBrutto = 0;
            decimal totalVat = 0;

            for (int i = 0; i < LVEServices.Items.Count; i++)
            {
                try
                {
                    //ID
                    LVEServices.Items[i].Text = (i + 1).ToString();

                    //Wartość Netto
                    decimal netto = Math.Round(Convert.ToDecimal(LVEServices.Items[i].SubItems[3].Text) * Convert.ToDecimal(LVEServices.Items[i].SubItems[4].Text), 2, MidpointRounding.AwayFromZero);
                    totalNetto += netto;
                    LVEServices.Items[i].SubItems[6].Text = netto.ToString("0.00");
                    
                    //vat
                    var rate = VatRate.FromString(LVEServices.Items[i].SubItems[9].Text);
                    if (reverseVAT)
                    {
                        rate = new VatRate(0);
                    }

                    decimal vat = Math.Round(netto * (rate.VatPercent / 100.0m), 2, MidpointRounding.AwayFromZero);
                    totalVat += vat;
                    LVEServices.Items[i].SubItems[7].Text = vat.ToString("0.00");

                    //Wartość Brutto
                    decimal brutto = Math.Round(netto + vat, 2, MidpointRounding.AwayFromZero);
                    totalBrutto += brutto;
                    LVEServices.Items[i].SubItems[8].Text = brutto.ToString("0.00");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Wiersz " + (i + 1), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            totalNetto = Math.Round(totalNetto, 2);
            totalVat = Math.Round(totalVat, 2);
            totalBrutto = Math.Round(totalBrutto, 2);

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
            foreach (Service currentService in ModelStore.ServiceRepository.Services)
            {
                CBService.Items.Add(new ComboBoxItem(currentService.Tag, currentService.Id));
            }
            if (CBService.Items.Count > 0) CBService.SelectedIndex = 0;

            foreach (var currentRecord in document.Items)
            {
                ListViewItem newItem = new ListViewItem();
                newItem.SubItems.Add(currentRecord.Name);
                newItem.SubItems.Add(currentRecord.PKWiU);
                newItem.SubItems.Add(currentRecord.PriceNet.ToString(CultureInfo.CurrentCulture));
                newItem.SubItems.Add(currentRecord.Quantity.ToString(CultureInfo.CurrentCulture));
                newItem.SubItems.Add(currentRecord.Unit);
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add(currentRecord.VatRate.ToString());
                
                LVEServices.Items.Add(newItem);
            }
            Reload();
        }

        //Zapisuje dane
        public void Save(Document document)
        {
            DocumentSummary documentSummary = document.DocumentSummary;
            documentSummary.TotalGross =  Convert.ToDecimal(TBTotalBrutto.Text);
            documentSummary.TotalNet =  Convert.ToDecimal(TBTotalNetto.Text);
            documentSummary.TotalVat =  Convert.ToDecimal(TBTotalVAT.Text);

            documentSummary.TotalInWords = TBSlownie.Text;

            document.Items.Clear();
            foreach (ListViewItem currentItem in LVEServices.Items)
            {
                DocumentItem newRecord = new DocumentItem
                {
                    Name = currentItem.SubItems[1].Text,
                    PKWiU = currentItem.SubItems[2].Text,
                    PriceNet = Convert.ToDecimal(currentItem.SubItems[3].Text),
                    Quantity = Convert.ToDecimal(currentItem.SubItems[4].Text),
                    Unit = currentItem.SubItems[5].Text,
                    SumNet = Convert.ToDecimal(currentItem.SubItems[6].Text),
                    SumVat = Convert.ToDecimal(currentItem.SubItems[7].Text),
                    VatRate = VatRate.FromString(currentItem.SubItems[9].Text),
                    SumGross = Convert.ToDecimal(currentItem.SubItems[8].Text),
                };

                document.Items.Add(newRecord);
            }
        }

        #region Extended List View
        private void LVEServices_SubItemClicked(object sender, SubItemEventArgs e)
        {
            if (e.SubItem > 0 && e.SubItem < 5)
                LVEServices.StartEditing(TBListViewExTB, e.Item, e.SubItem);

            if (e.SubItem == 9)
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
            Service service;
            try
            {
                service = ModelStore.ServiceRepository.Services.First(n => n.Id == ((ComboBoxItem)CBService.SelectedItem).Id);

            }
            catch
            {
                MessageBox.Show("Wybierz szablon usługi z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Changed = true;
            ListViewItem newItem = new ListViewItem("");
            {
                newItem.SubItems.Add(service.Name);
                newItem.SubItems.Add(service.PKWiU);
                newItem.SubItems.Add(service.PriceNet.ToString(CultureInfo.CurrentCulture));
                newItem.SubItems.Add("1");
                newItem.SubItems.Add(service.Unit);
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add("0");
                newItem.SubItems.Add(service.Vat.ToString());
            }

            LVEServices.Items.Add(newItem);
            Reload();
        }

        private void RecordEdit_Click(object sender, EventArgs e)
        {
            Service service = ModelStore.ServiceRepository.FindService(((ComboBoxItem)CBService.SelectedItem).Id);
            if (service == null)
            {
                MessageBox.Show("Wybierz szablon usługi z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Changed = true;
            if (LVEServices.SelectedItems.Count > 0)
            {
                LVEServices.SelectedItems[0].SubItems[1].Text = service.Name;
                LVEServices.SelectedItems[0].SubItems[1].Text = service.PKWiU;
                LVEServices.SelectedItems[0].SubItems[3].Text = service.PriceNet.ToString(CultureInfo.CurrentCulture);
                LVEServices.SelectedItems[0].SubItems[5].Text = service.Unit;
                LVEServices.SelectedItems[0].SubItems[9].Text = service.Vat.ToString();

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
                ModelStore.DocumentRepository.UpdateHighestDocumentId();
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
            ModelStore.DocumentRepository.UpdateHighestDocumentId();
        }

        #endregion
    }
}
