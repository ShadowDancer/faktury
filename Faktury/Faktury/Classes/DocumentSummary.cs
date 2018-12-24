namespace Faktury.Classes
{
    public class DocumentSummary
    {
        public float Netto { get; set; }
        public float TotalVat { get; set; }
        public float Brutto { get; set; }

        public string InWords { get; set; } = "";
    }
}
