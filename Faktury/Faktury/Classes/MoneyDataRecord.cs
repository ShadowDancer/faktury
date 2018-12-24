namespace Faktury.Classes
{
    public class MoneyDataRecord
    {
        /// <summary>
        /// szt / m / m^2 etc.
        /// </summary>
        public string Unit { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public float Count { get; set; }

        public float Netto { get; set; }
        
        public float Vat { get; set; }

        public float Brutto { get; set; }

        public float VatPercent { get; set; }
    }
}