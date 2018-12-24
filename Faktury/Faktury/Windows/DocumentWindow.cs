using System;
using System.Globalization;
using System.Windows.Forms;
using Faktury.Classes;
using Faktury.Print_Framework;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class DocumentWindow : DockContent
    {
        private readonly ModelStore _modelStore;
        private readonly PrintEngine _printEngine;
        private readonly SettingsAccessor _settingsAccessor;

        public DocumentWindow(ModelStore modelStore, PrintEngine printEngine, SettingsAccessor settingsAccessor)
        {
            _modelStore = modelStore;
            _printEngine = printEngine;
            _settingsAccessor = settingsAccessor;
            InitializeComponent();
            CBPaynament.Items.Clear();
            CBPaynament.Items.Add("Przelew");
            CBPaynament.Items.Add("Gotówka");
            CBPaynament.SelectedIndex = 0;
            documentProperties.ModelStore = modelStore;
        }

        public bool Changed
        {
            get
            {
                if(Document.CompanyId != ((ComboBoxItem)CBCompanyTag.SelectedItem).Id) return true;

                if(Document.IssueDate != DTPIssueDate.Value) return true;
                if(Document.SellDate != DTPSellDate.Value) return true;
                if(Document.Payment != CBPaynament.Text) return true;
                if(Document.PaymentTime != TBPaynamentTime.Text) return true;

                if(Document.Name != TBName.Text) return true;
                if(Document.DefaultName != cBDefaultName.Checked) return true;

                if(Document.Number != (int)nUDNumber.Value) return true;
                if(Document.Year != (int)nUDYear.Value) return true;

                if (Document.Paid != CxBPaid.Checked) return true;

                if (documentProperties.Changed) return true;

                return false;
            }
        }

        public bool ForceClose;

        private int _oldNumberValue;
        private int _oldYearValue;

        public Document Document { get; set; }

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

            return true;
        }

        public void SaveDataFromControls(Document document)
        {
            document.CompanyId = ((ComboBoxItem)CBCompanyTag.SelectedItem).Id;

            document.IssueDate = DTPIssueDate.Value;
            document.SellDate = DTPSellDate.Value;
            document.Payment = CBPaynament.Text;
            document.PaymentTime = TBPaynamentTime.Text;

            document.Name = TBName.Text;
            document.DefaultName = cBDefaultName.Checked;

            document.Number = (int)nUDNumber.Value;
            document.Year = (int)nUDYear.Value;

            document.Paid = CxBPaid.Checked;

            documentProperties.Save(document.MoneyData);
        }

        private void DocumentWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !OrderClose();
        }

        public void SetDefaultName()
        {
            if (cBDefaultName.Checked)
            {
                TBName.Text = nUDNumber.Value.ToString(CultureInfo.CurrentCulture) + ": " + CBCompanyTag.Text + " " + DTPIssueDate.Value.ToLongDateString();
            }
        }

        public void ReloadCompanyCombobox()
        {
            MainForm.Instance.ReloadCompanyCombobox(CBCompanyTag);
        }

        private void UpdateId()
        {
            _modelStore.UpdateHighestDocumentId();
            nUDNumber.Value = _oldNumberValue = _modelStore.NewDocumentId((int)nUDYear.Value);
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
            if (_oldNumberValue > nUDNumber.Value)
            {
                //decrease number
                var currentNumber = (int)nUDNumber.Value;

                while (currentNumber > 0)
                {
                    if (_modelStore.Documents.Find(n => (n != Document) && (n.Year == (int)nUDYear.Value && n.Number == currentNumber)) == null)
                    {
                        nUDNumber.Value = currentNumber;
                        break;
                    }

                    currentNumber--;
                }
                if (currentNumber == 0)
                {
                    nUDNumber.Value = _oldNumberValue;
                    MessageBox.Show("Nie znaleziono wolnego numeru. Aby zmieniejszyć numer faktury musi być puste miejsce! Aby rozwiązać problem zwiększ numer dokumentu z niższym numerem.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            if (_oldNumberValue < nUDNumber.Value)
            {
                //increase number
                var currentNumber = (int)nUDNumber.Value;

                while (true)
                {
                    if (_modelStore.Documents.Find(n => (n != Document) && (n.Year == (int)nUDYear.Value && n.Number == currentNumber)) == null)
                    {
                        nUDNumber.Value = currentNumber;
                        break;
                    }

                    currentNumber++;
                }
            }

            _oldNumberValue = (int)nUDNumber.Value;
            SetDefaultName();
        }

        private void nUDYear_ValueChanged(object sender, EventArgs e)
        {
            if(_oldYearValue != nUDYear.Value)
            UpdateId();
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
                var editorSettings = _settingsAccessor.GetSettings();
                Document = Document.CreateNewDocument();
                nUDYear.Value = DateTime.Today.Year;
                UpdateId();
                Document.DefaultName = editorSettings.DocumentSetDefaultNames;
                CxBSimilarDates.Checked = editorSettings.DocumentCreationDateSameAsSellDate;
                nUDYear.Enabled = true;
            }
            else
            {
                if (Document.SellDate.Day == Document.IssueDate.Day && Document.IssueDate.Month == Document.SellDate.Month && Document.IssueDate.Year == Document.SellDate.Year)
                    CxBSimilarDates.Checked = true;
                else CxBSimilarDates.Checked = false;

                _oldNumberValue = Document.Number;
                nUDNumber.Value = Document.Number;

                _oldYearValue = Document.Year;
                nUDYear.Value = Document.Year;
            }

            foreach (ComboBoxItem item in CBCompanyTag.Items)
            {
                if (item.Id == Document.CompanyId)
                {
                    CBCompanyTag.SelectedItem = item;
                    break;
                }
            }

            DTPIssueDate.Value = Document.IssueDate;
            DTPSellDate.Value = Document.SellDate;

            CBPaynament.Text = Document.Payment;
            TBPaynamentTime.Text = Document.PaymentTime;

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
            var targetDocument = new Document
            {
                MoneyData = Document.MoneyData
            };
            SaveDataFromControls(targetDocument);

            //check for errors
            if (_modelStore.Companies.Find(n => n.Id == targetDocument.CompanyId) == null)
            {
                MessageBox.Show("Kontrahent nieznany!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            new DocumentPrinter(_modelStore, _settingsAccessor, targetDocument).Print(_printEngine);
        }

        public void ShowPreview()
        {
            var targetDocument = new Document
            {
                MoneyData = Document.MoneyData
            };
            SaveDataFromControls(targetDocument);

            //check for errors
            if (_modelStore.Companies.Find(n => n.Id == targetDocument.CompanyId) == null)
            {
                MessageBox.Show("Kontrahent nieznany!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            new DocumentPrinter(_modelStore, _settingsAccessor, targetDocument).ShowPreview(_printEngine);
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
        public ComboBoxItem(string text, int data)
        {
            Text = text;
            Data = data;
        }
        public ComboBoxItem(string text, Object data)
        {
            Text = text;
            Data = data;
        }

        public string Text;
        public object Data;
        public int Id => (int)Data;

        public override string ToString()
        {
            return Text;
        }
    }
}
