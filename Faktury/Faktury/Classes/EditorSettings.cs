using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Faktury
{

    public class CommonDocuments
    {
        [DllImport("shell32.dll")]
        static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken,
           uint dwFlags, [Out] StringBuilder pszPath);

        public static string GetCommonDocumentsFolder()
        {
            int SIDL_COMMON_DOCUMENTS = 0x002e;
            StringBuilder sb = new StringBuilder();
            SHGetFolderPath(IntPtr.Zero, SIDL_COMMON_DOCUMENTS, IntPtr.Zero, 0x0000, sb);
            return sb.ToString();
        }
    }

    public class EditorSettings
    {
        public Classes.Company OwnerCompany = null;

        //SQL database
        public string SQLAdress = "";
        public string SQLUser = "";
        public string SQLPass = "";
        public string SQLName = "";

        //backup
        public bool LocalBackup = true;
        public bool LocalBackupOnlyOnExit = true;

        //device
        public bool DeviceBackup = true;
        public string DeviceBackupLabel = "";
        public int DeviceRandomNumber = -1;
        public int DeviceBackupTotalSpace = 0;
        public int DeviceBackupPeriod = 0;
        public DateTime DeviceBackupLastTime = DateTime.Now;

        //materials
        public List<string> Properties_Vat = new List<string>();
        public List<string> Properties_Unit = new List<string>();

        //documents
        public bool DocumentAutoRefresh = true;
        public bool DocumentShowFilters = false;
        //TODO
        public bool DocumentSetDefaultNames = true;
        public bool DocumentCreationDateSameAsSellDate = true;

        //filters
        public bool DocumentFilterYear = false;

        public bool DocumentFilterDate = false;
        public DateTime DocumentFilterDateTime = DateTime.Today;
        public bool DocumentFilterDateYounger = false;
        public bool DocumentFilterDateNow = false;
        public bool DocumentFilterDateOlder = false;

        public bool DocumentFilterPaynament = false;
        public bool DocumentFilterPaidOnly = false;

        public bool DocumentFilterCompany = false;
        public bool DocumentFilterName = false;

        //values
        public int DocumentFilterYearValue = DateTime.Now.Year;
        public string DocumentFilterNameValue = "";
        public int DocumentFilterCompanyValue = 0;

        public int DocumentFilterDateDayValue = DateTime.Now.Day;
        public int DocumentFilterDateMonthValue = DateTime.Now.Month;
        public int DocumentFilterDateYearValue = DateTime.Now.Year;

        
    }
}
