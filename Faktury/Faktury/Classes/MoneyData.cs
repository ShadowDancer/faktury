using System.Collections.Generic;

namespace Faktury.Classes
{
    public class MoneyData
    {
        public MoneyData()
        {
            Records = new List<MoneyDataRecord>();
            InWords = "";
        }

        public float Netto { get; set; }
        public float TotalVat { get; set; }
        public float Brutto { get; set; }

        public string InWords { get; set; }

        public List<MoneyDataRecord> Records { get; set; }
    }
}
