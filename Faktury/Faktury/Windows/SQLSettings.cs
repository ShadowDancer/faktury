using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Faktury.Windows
{
    public partial class SQLSettings : Form
    {


        public SQLManager SQLManager = MainForm.Instance.SQLManager;

        public SQLSettings()
        {
            InitializeComponent();
        }

        public void RefershBaseState()
        {
            string Text = "";

            if (SQLManager.Connection == null) Text = "Brak";
            else switch (SQLManager.Connection.State)
            {
                case ConnectionState.Broken:
                    Text = "Przerwane";
                    break;
                case ConnectionState.Closed:
                    Text = "Zamknięte";
                    break;
                case ConnectionState.Connecting:
                    Text = "Łączenie";
                    break;
                case ConnectionState.Executing:
                    Text = "Pracuje";
                    break;
                case ConnectionState.Open:
                    Text = "Połączono";
                    break;
                default:
                    Text = "Nieznany";
                    break;
            }

            LConectionState.Text = Text;
        }

        private void BRefreshStatus_Click(object sender, EventArgs e)
        {
            RefershBaseState();
        }

        private void BConnect_Click(object sender, EventArgs e)
        {
            if (!(SQLManager.Connection == null) && (SQLManager.Connection.State == ConnectionState.Open || SQLManager.Connection.State == ConnectionState.Connecting || SQLManager.Connection.State == ConnectionState.Executing))
            {
                MessageBox.Show("Baza danych jest już połączona!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            SQLManager.Connect(TBAdress.Text, TBUser.Text, TBPassword.Text, TBName.Text);
            RefershBaseState();
            if (SQLManager.Connection.State == ConnectionState.Open)
            {
                SQLManager.CreateTables();
            }
        }

        private void BDisconnect_Click(object sender, EventArgs e)
        {
            if (SQLManager.Connection.State == ConnectionState.Open || SQLManager.Connection.State == ConnectionState.Connecting || SQLManager.Connection.State == ConnectionState.Executing)
            {
                SQLManager.Disconnect();
            }
            else
            {
                MessageBox.Show("Nie połączono z bazą danych!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            RefershBaseState();
        }

        private void SQLSettings_Load(object sender, EventArgs e)
        {
            RefershBaseState();
            TBUser.Text = MainForm.Instance.Settings.SQLUser;
            TBPassword.Text = MainForm.Instance.Settings.SQLPass;
            TBAdress.Text = MainForm.Instance.Settings.SQLAdress;
            TBName.Text = MainForm.Instance.Settings.SQLName;
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            MainForm.Instance.Settings.SQLUser = TBUser.Text;
            MainForm.Instance.Settings.SQLPass = TBPassword.Text;
            MainForm.Instance.Settings.SQLAdress = TBAdress.Text;
            MainForm.Instance.Settings.SQLName = TBName.Text;
            Close();
        }
    }
}
