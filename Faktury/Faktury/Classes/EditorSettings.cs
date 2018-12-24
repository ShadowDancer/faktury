using System;
using System.Collections.Generic;

namespace Faktury.Classes
{
    public class EditorSettings
    {
        public Company OwnerCompany = null;

        //backup
        public bool LocalBackup = true;
        public bool LocalBackupOnlyOnExit = true;

        //device
        public bool DeviceBackup = true;
        public string DeviceBackupLabel = "";
        public int DeviceRandomNumber = -1;
        public int DeviceBackupPeriod = 0;
        public DateTime DeviceBackupLastTime = DateTime.Now;

        //materials
        public List<string> PropertiesVat = new List<string>();
        public List<string> PropertiesUnit = new List<string>();

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
