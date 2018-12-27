using System;
using System.Windows.Forms;
using Faktury.Classes;
using WeifenLuo.WinFormsUI.Docking;

namespace Faktury.Windows
{
    public partial class CompanyWindow : DockContent
    {
        private readonly ModelStore _modelStore;

        public CompanyWindow(ModelStore modelStore)
        {
            _modelStore = modelStore;
            InitializeComponent();
        }

        public Company Company { get; set; }
        public bool AddToCollection = true;

        private void CompanyWindow_Load(object sender, EventArgs e)
        {
            if (Company == null)
            {
                Company = new Company
                {
                    Name = "Nowa", Bank = false, Id = _modelStore.NewCompanyId(), CreationDate = DateTime.Now
                };
                Company.ModificationDate = Company.CreationDate;
            }

            TBAdress.Text = Company.Address;
            TBAdress2.Text = Company.Street;
            TBBankAccount.Text = Company.BankAccount;
            TBBankSecion.Text = Company.BankSection;
            TBMobile.Text = Company.MobileNumber;
            TBName.Text = Company.Name;
            TBNip.Text = Company.Nip;
            TBOwner.Text = Company.Owner;
            TBPhone.Text = Company.PhoneNumber;
            TBTag.Text = Company.ShortName;

            GBBank.Visible = Company.Bank;
            TBTag.Visible = LTag.Visible = !Company.Bank;
            ValidateNIP();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (TBTag.Text != "" || Company.Bank)
            {
                Company.Address = TBAdress.Text;
                Company.Street = TBAdress2.Text;
                Company.BankAccount = TBBankAccount.Text;
                Company.BankSection = TBBankSecion.Text;
                Company.MobileNumber = TBMobile.Text;
                Company.Name = TBName.Text;
                Company.Nip = TBNip.Text;
                Company.Owner = TBOwner.Text;
                Company.PhoneNumber = TBPhone.Text;
                Company.ShortName = TBTag.Text;

                Company.ModificationDate = DateTime.Now;

                if (AddToCollection)
                {
                    if (_modelStore.FindCompany(Company.Id) == null)
                        _modelStore.AddCompany(Company);
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

        private void TBNip_TextChanged(object sender, EventArgs e)
        {
            ValidateNIP();
        }

        private void ValidateNIP()
        {
            string valueToValidate = TBNip.Text;

            if (!string.IsNullOrEmpty(valueToValidate))
            {
                int digits = 0;
                foreach (var c in valueToValidate)
                {
                    if (c != '-')
                    {
                        if (!char.IsDigit(c))
                        {
                            NIPValidation.Visible = true;
                            NIPValidation.Text = "Nieprawidłowe znaki";
                            return;
                        }

                        digits++;
                    }
                }

                if (digits != 10)
                {
                    NIPValidation.Visible = true;
                    NIPValidation.Text = "Nieprawidlowa długość";
                    return;
                }

                if (!NipValidator.IsNipValid(valueToValidate))
                {
                    NIPValidation.Visible = true;
                    NIPValidation.Text = "Nieprawidłowy NIP";
                    return;
                }
            }

            NIPValidation.Visible = false;
        }
    }
}
