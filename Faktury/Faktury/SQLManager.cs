using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Faktury
{
    public class SQLManager
    {
        // ComapnyID, Paynament, PaynamentTime, Name, DefaultyName, Number, Year, Paid, IssueDate, SellDate
        // int,       text,      text,          text, tinyint,      int,    int,  tinyint,  date,  date
        const string DocumentsTableName = "Documents";
        // ID, Name, Owner, Adress, Street, NIP, Tag, BankAccount, BankSection, PhoneNumber, CreationDate, ModificationDate
        // int, text, text, text,   text,   text, text, text,      text,        text,        date,         date,
        const string CompaniesTableName = "Companies";
        // ID, Name, Tag, Jm, Price, Vat, CreationDate, ModificationDate
        // int, text, text, text, real, int, date, date
        const string GoodsTableName = "Goods";

        public SqlConnection Connection = null;

        public bool Connect(string Address, string UserName, string Password, string Name)
        {
            string ConnectionString = "Server=_Server_;Database=_Database_;User ID=_UserName_;Password=_Password_;";
            ConnectionString = ConnectionString.Replace("_UserName_", UserName).Replace("_Password_", Password).Replace("_Server_", Address).Replace("_Database_", Name);
            
            try
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Błąd podczas próby połączenia z bazą danych", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public void CreateTables()
        {
            SqlCommand Query = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", Connection);
            SqlDataReader QueryReader = Query.ExecuteReader();
            DataTable Result = new DataTable();
            
            Result.Load(QueryReader);
            QueryReader.Close();

            bool DocumentsTable = false;
            bool CompaniesTable = false;
            bool GoodsTable = false;

            foreach (DataRow CurrentRow in Result.Rows)
            {
                foreach (DataColumn CurrentColumn in Result.Columns)
                {
                    if (!DocumentsTable)
                        if ((string)CurrentRow[CurrentColumn.ColumnName] == DocumentsTableName)
                            DocumentsTable = true;

                    if (!CompaniesTable)
                        if ((string)CurrentRow[CurrentColumn.ColumnName] == CompaniesTableName)
                            CompaniesTable = true;

                    if (!GoodsTable)
                        if ((string)CurrentRow[CurrentColumn.ColumnName] == GoodsTableName)
                            GoodsTable = true;
                }
            }

            if (!DocumentsTable)
            {
                
                Query.CommandText = "CREATE TABLE " + DocumentsTableName + "(" +
                    "CompanyID int," +
                    "Paynament text," +
                    "PaynamentTime text," +
                    "Name text," +
                    "DefaultName tinyint," +
                    "Number int," +
                    "Year int," +
                    "Paid tinyint," +
                    "IssueDate date," +
                    "SellDate date" +
                    ")";


                QueryReader = Query.ExecuteReader();
                Result.Clear();
                Result.Load(QueryReader);
                QueryReader.Close();

            }

            if (!CompaniesTable)
            {
                Query.CommandText = "CREATE TABLE " + CompaniesTableName + "(" +
                "ID int," +
                "Name text," +
                "Owner text," +
                "Adress text," +
                "Street text," +
                "Nip text," +
                "Tag text," +
                "BankAccount text," +
                "BankSection text," +
                "PhoneNumber text," +
                "MobileNumber text," +
                "CreationDate date," +
                "ModificationDate date," +
                ")";


                QueryReader = Query.ExecuteReader();
                Result.Clear();
                Result.Load(QueryReader);
                QueryReader.Close();
            }

            if (!GoodsTable)
            {
                Query.CommandText = "CREATE TABLE " + GoodsTableName + "(" +
                "ID int," +
                "Name text," +
                "Tag text," +
                "Jm text," +
                "Price real," +
                "Vat int," +
                "CreationDate date," +
                "ModificationDate date," +
                ")";


                QueryReader = Query.ExecuteReader();
                Result.Clear();
                Result.Load(QueryReader);
                QueryReader.Close();
            }

            SqlCommand Insert = InsertDocuments(Windows.MainForm.Instance.Documents.ToArray());
            Insert.Connection = Connection;
            Insert.ExecuteReader().Close();
            Insert = InsertCompanies(Windows.MainForm.Instance.Companies.ToArray());
            Insert.Connection = Connection;
            Insert.ExecuteReader().Close();
            Insert = InsertGoods(Windows.MainForm.Instance.Services.ToArray());
            Insert.Connection = Connection;
            Insert.ExecuteReader().Close();
                    
            
        }

        public string CreateDocumentInsertString(Classes.Document Doc)
        {
            return string.Format("({0}, N'{1}', N'{2}', N'{3}', {4}, {5}, {6}, {7}, '{8}', '{9}')", new object[] {
               Doc.CompanyID, Doc.Paynament, Doc.PaynamentTime, Doc.Name, (Doc.DefaultName) ? 1 : 0, Doc.Number, Doc.Year, (Doc.Paid) ? 1 : 0, Doc.IssueDate.ToString("yyyy-MM-dd"), Doc.SellDate.ToString("yyyy-MM-dd")
            });
        }
        public string CreateCompanyInsertString(Classes.Company Com)
        {
            return string.Format("({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}', '{11}', '{12}')", new object[] {
                Com.ID, Com.Name, Com.Owner, Com.Adress, Com.Street, Com.Nip, Com.Tag, Com.BankAccount, Com.BankSection, Com.PhoneNumber, Com.MobileNumber, Com.CreationDate.ToString("yyyy-MM-dd"), Com.ModificationDate.ToString("yyyy-MM-dd")
            });
        }

        public string CreateGoodInsertString(Classes.Service Good)
        {
            return string.Format("({0}, N'{1}', N'{2}', N'{3}', {4}, {5}, '{6}', '{7}')", new object[] {
                Good.ID, Good.Name, Good.Tag, Good.Jm, Good.Price, Good.Vat, Good.CreationDate.ToString("yyyy-MM-dd"), Good.ModificationDate.ToString("yyyy-MM-dd")
            });
        }

        public SqlCommand InsertDocuments(Classes.Document[] Documents)
        {
            SqlCommand Query = new SqlCommand();

            Query.CommandText = "INSERT INTO " + DocumentsTableName + "\nVALUES ";
            foreach (var CurrentDocument in Documents)
            {
                Query.CommandText += CreateDocumentInsertString(CurrentDocument) + ", "; 
            }

            Query.CommandText = Query.CommandText.Substring(0, Query.CommandText.Length - 2) + ';';

            return Query;
        }

        public SqlCommand InsertCompanies(Classes.Company[] Companies)
        {
            SqlCommand Query = new SqlCommand();

            Query.CommandText = "INSERT INTO " + CompaniesTableName + "\nVALUES ";
            foreach (var CurrentDocument in Companies)
            {
                Query.CommandText += CreateCompanyInsertString(CurrentDocument) + ", ";
            }

            Query.CommandText = Query.CommandText.Substring(0, Query.CommandText.Length - 2) + ';';

            return Query;
        }

        public SqlCommand InsertGoods(Classes.Service[] Goods)
        {
            SqlCommand Query = new SqlCommand();

            Query.CommandText = "INSERT INTO " + GoodsTableName + "\nVALUES ";
            foreach (var CurrentDocument in Goods)
            {
                Query.CommandText += CreateGoodInsertString(CurrentDocument) + ", ";
            }

            Query.CommandText = Query.CommandText.Substring(0, Query.CommandText.Length - 2) + ';';

            return Query;
        }

        public void Disconnect()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Błąd podczas próby rozłączniea bazy danych", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

    }
}
