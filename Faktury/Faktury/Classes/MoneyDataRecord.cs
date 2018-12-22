namespace Faktury.Classes
{
    public class MoneyDataRecord
    {
        public string Unit { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public float Count { get; set; }

        public float Netto { get; set; }
        public float Vat { get; set; }
        public float Brutto { get; set; }
        public float VatPrecent { get; set; }
    }
}