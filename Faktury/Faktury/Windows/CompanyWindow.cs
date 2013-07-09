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
    public partial class CompanyWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        public CompanyWindow()
        {
            InitializeComponent();
        }

        public Classes.Company Company { get; set; }
        public bool AddToCollection = true;

        private void CompanyWindow_Load(object sender, EventArgs e)
        {
            if (Company == null)
            {
                Company = new Faktury.Classes.Company();
                Company.Name = "Nowa";
                Company.Bank = false;
                Company.ID = MainForm.Instance.GetNewCompanyID;
                Company.CreationDate = DateTime.Now;
                Company.ModificationDate = Company.CreationDate;
            }

            TBAdress.Text = Company.Adress;
            TBAdress2.Text = Company.Street;
            TBBankAccount.Text = Company.BankAccount;
            TBBankSecion.Text = Company.BankSection;
            TBMobile.Text = Company.MobileNumber;
            TBName.Text = Company.Name;
            TBNip.Text = Company.Nip;
            TBOwner.Text = Company.Owner;
            TBPhone.Text = Company.PhoneNumber;
            TBTag.Text = Company.Tag;

            GBBank.Visible = Company.Bank;
            TBTag.Visible = LTag.Visible = !Company.Bank;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (TBTag.Text != "" || Company.Bank)
            {
                Company.Adress = TBAdress.Text;
                Company.Street = TBAdress2.Text;
                Company.BankAccount = TBBankAccount.Text;
                Company.BankSection = TBBankSecion.Text;
                Company.MobileNumber = TBMobile.Text;
                Company.Name = TBName.Text;
                Company.Nip = TBNip.Text;
                Company.Owner = TBOwner.Text;
                Company.PhoneNumber = TBPhone.Text;
                Company.Tag = TBTag.Text;

                Company.ModificationDate = DateTime.Now;

                if (AddToCollection)
                {
                    Classes.Company Check = MainForm.Instance.Companies.Find(n => n.ID == Company.ID);
                    if (Check == null)
                        MainForm.Instance.Companies.Add(Company);
                }

                MainForm.Instance.ReloadCompanyComboboxesInChildWindows();
                Close();
            }
            else MessageBox.Show("Pole \"Nazwa wyświetlana\" jest puste!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
