using System;
using System.Collections.Generic;

namespace Faktury.Domain.Domain
{
    [Serializable]
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
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public List<string> Properties_Vat = new List<string>();
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
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

        // ReSharper disable once IdentifierTypo
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
