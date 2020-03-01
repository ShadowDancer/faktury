namespace Faktury.Domain.Classes
{
    public class DocumentSummary
    {
        public decimal TotalNet { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalGross { get; set; }

        public string InWords { get; set; } = "";
    }
}