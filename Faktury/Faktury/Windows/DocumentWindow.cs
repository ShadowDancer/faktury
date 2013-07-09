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
    public partial class DocumentWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public DocumentWindow()
        {
            InitializeComponent();
            CBPaynament.Items.Clear();
            CBPaynament.Items.Add("Przelew");
            CBPaynament.Items.Add("Gotówka");
            CBPaynament.SelectedIndex = 0;
        }

        public bool Changed
        {
            get
            {
                if(Document.CompanyID != ((ComboBoxItem)CBCompanyTag.SelectedItem).ID) return true;

                if(Document.IssueDate != DTPIssueDate.Value) return true;
                if(Document.SellDate != DTPSellDate.Value) return true;
                if(Document.Paynament != CBPaynament.Text) return true;
                if(Document.PaynamentTime != TBPaynamentTime.Text) return true;

                if(Document.Name != TBName.Text) return true;
                if(Document.DefaultName != cBDefaultName.Checked) return true;

                if(Document.Number != (int)nUDNumber.Value) return true;
                if(Document.Year != (int)nUDYear.Value) return true;

                if (Document.Paid != CxBPaid.Checked) return true;

                if (documentProperties.Changed) return true;

                return false;
            }
        }

        public bool ForceClose = false;

        private int OldNumberValue = 0;
        private int OldYearValue = 0;

        public Classes.Document Document { get; set; }

        private bool OrderClose()
        {
            if (!ForceClose && (Changed))
            {
                switch (MessageBox.Show("Zapisać zmiany?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:

                        MainForm.Instance.SaveDocument(this);
                        return true;
                    case DialogResult.No:

                        return true;
                    case DialogResult.Cancel:

                        return false;
                    default:
                        return false;
                }

            }
            else return true;
        }

        public void SaveDataFromControls(Classes.Document Document)
        {
            Document.CompanyID = ((ComboBoxItem)CBCompanyTag.SelectedItem).ID;

            Document.IssueDate = DTPIssueDate.Value;
            Document.SellDate = DTPSellDate.Value;
            Document.Paynament = CBPaynament.Text;
            Document.PaynamentTime = TBPaynamentTime.Text;

            Document.Name = TBName.Text;
            Document.DefaultName = cBDefaultName.Checked;

            Document.Number = (int)nUDNumber.Value;
            Document.Year = (int)nUDYear.Value;

            Document.Paid = CxBPaid.Checked;

            documentProperties.Save(Document.MoneyData);
        }

        private void DocumentWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !OrderClose();
        }

        public void SetDefaultName()
        {
            if (cBDefaultName.Checked == true)
            {
                TBName.Text = nUDNumber.Value.ToString() + ": " + CBCompanyTag.Text + " " + DTPIssueDate.Value.ToLongDateString();
            }
        }

        public void ReloadCompanyCombobox()
        {
            MainForm.Instance.ReloadCompanyCombobox(CBCompanyTag);
        }

        public void UpdateID()
        {
            MainForm.Instance.UpdateHigestDocumentID();

            if (MainForm.Instance.HigestDocumentID.ContainsKey((int)nUDYear.Value))
            {
                nUDNumber.Value = OldNumberValue = MainForm.Instance.HigestDocumentID[((int)nUDYear.Value)] + 1;
            }
            else
            {
                OldNumberValue = 1;
                nUDNumber.Value = 1;
            }
        }

        private void cBDefaultName_CheckedChanged(object sender, EventArgs e)
        {
            TBName.Enabled = !cBDefaultName.Checked;
            SetDefaultName();
        }

        private void CBCompanyTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultName();
        }

        private void nUDNumber_ValueChanged(object sender, EventArgs e)
        {
            if (OldNumberValue > nUDNumber.Value)
            {
                //decrease number
                int CurrentNumber = (int)nUDNumber.Value;

                while (CurrentNumber > 0)
                {
                    if (MainForm.Instance.Documents.Find(n => (n != Document) && (n.Year == (int)nUDYear.Value && n.Number == CurrentNumber)) == null)
                    {
                        nUDNumber.Value = CurrentNumber;
                        break;
                    }
                    else
                    {
                        CurrentNumber--;
                    }
                }
                if (CurrentNumber == 0)
                {
                    nUDNumber.Value = OldNumberValue;
                    MessageBox.Show("Nie znaleziono wolnego numeru. Aby zmieniejszyć numer faktury musi być puste miejsce! Aby rozwiązać problem zwiększ numer dokumentu z niższym numerem.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            if (OldNumberValue < nUDNumber.Value)
            {
                //increase number
                int CurrentNumber = (int)nUDNumber.Value;

                while (true)
                {
                    if (MainForm.Instance.Documents.Find(n => (n != Document) && (n.Year == (int)nUDYear.Value && n.Number == CurrentNumber)) == null)
                    {
                        nUDNumber.Value = CurrentNumber;
                        break;
                    }
                    else
                    {
                        CurrentNumber++;
                    }
                }
            }

            OldNumberValue = (int)nUDNumber.Value;
            SetDefaultName();
        }

        private void nUDYear_ValueChanged(object sender, EventArgs e)
        {
            if(OldYearValue != nUDYear.Value)
            UpdateID();
        }

        private void DocumentWindow_Load(object sender, EventArgs e)
        {
            //Setup nUD control
            ((TextBox)nUDNumber.Controls[1]).ReadOnly = true;
            ((TextBox)nUDYear.Controls[1]).ReadOnly = true;

            ReloadCompanyCombobox();

            //create new document if neccessary
            if (Document == null)
            {
                Document = Faktury.Classes.Document.CreateNewDocument();
                nUDYear.Value = DateTime.Today.Year;
                UpdateID();
                Document.DefaultName = MainForm.Instance.Settings.DocumentSetDefaultNames;
                CxBSimilarDates.Checked = MainForm.Instance.Settings.DocumentCreationDateSameAsSellDate;
                nUDYear.Enabled = true;
            }
            else
            {
                if (Document.SellDate.Day == Document.IssueDate.Day && Document.IssueDate.Month == Document.SellDate.Month && Document.IssueDate.Year == Document.SellDate.Year)
                    CxBSimilarDates.Checked = true;
                else CxBSimilarDates.Checked = false;

                OldNumberValue = Document.Number;
                nUDNumber.Value = Document.Number;

                OldYearValue = Document.Year;
                nUDYear.Value = Document.Year;
            }

            foreach (ComboBoxItem Item in CBCompanyTag.Items)
            {
                if (Item.ID == Document.CompanyID)
                {
                    CBCompanyTag.SelectedItem = Item;
                    break;
                }
            }

            DTPIssueDate.Value = Document.IssueDate;
            DTPSellDate.Value = Document.SellDate;

            CBPaynament.Text = Document.Paynament;
            TBPaynamentTime.Text = Document.PaynamentTime;

            TBName.Text = Document.Name;
            cBDefaultName.Checked = Document.DefaultName;
            TBName.Enabled = !Document.DefaultName;

            CxBPaid.Checked = Document.Paid;

            documentProperties.Initialize(Document.MoneyData);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (MainForm.Instance.SaveDocument(this))
            {
                ForceClose = true;
                Close();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            ForceClose = true;
            Close();
        }

        public void Print()
        {
            Classes.Document TargetDocument = new Classes.Document();
            TargetDocument.MoneyData = Document.MoneyData;
            SaveDataFromControls(TargetDocument);

            //check for errors
            if (MainForm.Instance.Companies.Find(n => n.ID == TargetDocument.CompanyID) == null)
            {
                MessageBox.Show("Kontrahent nieznany!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TargetDocument.Print();
        }

        public void ShowPreview()
        {
            Classes.Document TargetDocument = new Classes.Document();
            TargetDocument.MoneyData = Document.MoneyData;
            SaveDataFromControls(TargetDocument);

            //check for errors
            if (MainForm.Instance.Companies.Find(n => n.ID == TargetDocument.CompanyID) == null)
            {
                MessageBox.Show("Kontrahent nieznany!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TargetDocument.ShowPreview();
        }

        private void CxBSimilarDates_CheckedChanged(object sender, EventArgs e)
        {
            if (CxBSimilarDates.Checked) DTPSellDate.Value = DTPIssueDate.Value;
            DTPSellDate.Enabled = !CxBSimilarDates.Checked;
        }

        private void DTPIssueDate_ValueChanged(object sender, EventArgs e)
        {
            if (CxBSimilarDates.Checked)
            {
                DTPSellDate.Value = DTPIssueDate.Value;
            }
        }
    }
       
    public class ComboBoxItem
    {
        public ComboBoxItem(string Text, int Data)
        {
            this.Text = Text;
            this.Data = Data;
        }
        public ComboBoxItem(string Text, Object Data)
        {
            this.Text = Text;
            this.Data = Data;
        }

        public string Text = "";
        public Object Data = -1;
        public int ID { get { return (int)Data; } }

        public override string ToString()
        {
            return Text;
        }
    }
}
