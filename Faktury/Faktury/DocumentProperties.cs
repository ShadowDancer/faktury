using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Faktury.Windows
{
    public partial class DocumentProperties : UserControl
    {
        public DocumentProperties()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            float TotalNetto = 0;
            float TotalBrutto = 0;
            float TotalVAT = 0;

            for (int i = 0; i < LVEServices.Items.Count; i++)
            {
                try
                {
                    //ID
                    LVEServices.Items[i].Text = (i + 1).ToString();

                    //Wartość Netto
                    float Netto = (float)Math.Round((Convert.ToSingle(LVEServices.Items[i].SubItems[2].Text) * Convert.ToSingle(LVEServices.Items[i].SubItems[3].Text)), 2);
                    TotalNetto += Netto;
                    LVEServices.Items[i].SubItems[5].Text = Netto.ToString("0.00");

                    //vat
                    float VAT = (float)Math.Round(Netto * (float.Parse(LVEServices.Items[i].SubItems[8].Text) / 100), 2);
                    TotalVAT += VAT;
                    LVEServices.Items[i].SubItems[6].Text = VAT.ToString("0.00");

                    //Wartość Brutto
                    float Brutto = (float)Math.Round(Netto + VAT, 2);
                    TotalBrutto += Brutto;
                    LVEServices.Items[i].SubItems[7].Text = Brutto.ToString("0.00");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Wiersz " + (i + 1).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            TotalNetto = (float)Math.Round(TotalNetto, 2);
            TotalVAT = (float)Math.Round(TotalVAT, 2);
            TotalBrutto = (float)Math.Round(TotalBrutto, 2);

            TBTotalNetto.Text = TotalNetto.ToString("0.00");
            TBTotalVAT.Text = TotalVAT.ToString("0.00");
            TBTotalBrutto.Text = TotalBrutto.ToString("0.00");

            TBSlownie.Text = Classes.NumberToWordConventer.ConvertValues(TotalBrutto);

        }

        public bool Changed = false;

        //Wczytuje kontrolki i rekordy
        public void Initialize(Classes.MoneyData MoneyData)
        {
            //load combo box
            foreach (Classes.Service CurrentService in MainForm.Instance.Services)
            {
                CBService.Items.Add(new ComboBoxItem(CurrentService.Tag, CurrentService.ID));
            }
            if (CBService.Items.Count > 0) CBService.SelectedIndex = 0;

            foreach (var CurrentRecord in MoneyData.Records)
            {
                ListViewItem NewItem = new ListViewItem();
                NewItem.SubItems.Add(CurrentRecord.Name);
                NewItem.SubItems.Add(CurrentRecord.Cost.ToString());
                NewItem.SubItems.Add(CurrentRecord.Count.ToString());
                NewItem.SubItems.Add(CurrentRecord.Unit);
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add(CurrentRecord.VATPrecent.ToString());
                
                LVEServices.Items.Add(NewItem);
            }
            Reload();
        }

        //Zapisuje dane
        public void Save(Classes.MoneyData MoneyData)
        {
            MoneyData.Brutto =  Convert.ToSingle(TBTotalBrutto.Text);
            MoneyData.Netto =  Convert.ToSingle(TBTotalNetto.Text);
            MoneyData.TotalVAT =  Convert.ToSingle(TBTotalVAT.Text);

            MoneyData.InWords = TBSlownie.Text;

            MoneyData.Records.Clear();
            foreach (ListViewItem CurrentItem in LVEServices.Items)
            {
                Classes.MoneyDataRecord NewRecord = new Faktury.Classes.MoneyDataRecord();

                NewRecord.Name = CurrentItem.SubItems[1].Text;
                NewRecord.Cost = Convert.ToSingle(CurrentItem.SubItems[2].Text);
                NewRecord.Count = Convert.ToSingle(CurrentItem.SubItems[3].Text);
                NewRecord.Unit = CurrentItem.SubItems[4].Text;
                NewRecord.Netto = Convert.ToSingle(CurrentItem.SubItems[5].Text);
                NewRecord.VAT = Convert.ToSingle(CurrentItem.SubItems[6].Text);
                NewRecord.Brutto = Convert.ToSingle(CurrentItem.SubItems[7].Text);
                NewRecord.VATPrecent = Convert.ToSingle(CurrentItem.SubItems[8].Text);

                MoneyData.Records.Add(NewRecord);
            }
        }

        #region Extended List View
        private void LVEServices_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
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
            Classes.Service Check = null;
            try
            {
                Check = MainForm.Instance.Services.First(n => n.ID == ((ComboBoxItem)CBService.SelectedItem).ID);

            }
            catch
            {
                MessageBox.Show("Wybierz szablon usługi z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Changed = true;
            ListViewItem NewItem = new ListViewItem("");
            {
                NewItem.SubItems.Add(Check.Name);
                NewItem.SubItems.Add(Check.Price.ToString());
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add(Check.Jm);
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add("0");
                NewItem.SubItems.Add(Check.Vat.ToString());
            }

            LVEServices.Items.Add(NewItem);
            Reload();
        }

        private void RecordEdit_Click(object sender, EventArgs e)
        {
            Classes.Service Check = null;
            try
            {
                Check = MainForm.Instance.Services.Find(n => n.ID == ((ComboBoxItem)CBService.SelectedItem).ID);
                if (Check == null) throw new Exception();
            }
            catch
            {
                MessageBox.Show("Wybierz szablon usługi z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Changed = true;
            if (LVEServices.SelectedItems.Count > 0)
            {
                LVEServices.SelectedItems[0].SubItems[1].Text = Check.Name;
                LVEServices.SelectedItems[0].SubItems[2].Text = Check.Price.ToString();
                LVEServices.SelectedItems[0].SubItems[4].Text = Check.Jm;
                LVEServices.SelectedItems[0].SubItems[8].Text = Check.Vat.ToString();

                Reload();
            }
            else
            {
                MessageBox.Show("Wybierz usługę do edycji z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void RecordDelete_Click(object sender, EventArgs e)
        {
            if (LVEServices.SelectedItems.Count > 0)
            {
                foreach (ListViewItem CurrentItem in LVEServices.SelectedItems)
                {
                    LVEServices.Items.Remove(CurrentItem);
                }
                MainForm.Instance.UpdateHigestDocumentID();
                Reload();
                Changed = true;
            }
            else
            {
                MessageBox.Show("Wybierz usługę do usunięcia z listy!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void RecordDeleteAll_Click(object sender, EventArgs e)
        {

            Changed = true;
            LVEServices.Items.Clear();
            MainForm.Instance.UpdateHigestDocumentID();
        }

        #endregion
    }
}
