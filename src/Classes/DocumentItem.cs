namespace Faktury.Classes
{
    public class DocumentItem
    {
        /// <summary>
        ///     szt / m / m^2 etc.
        /// </summary>
        public string Unit { get; set; }

        public string Name { get; set; }

        public decimal PriceNet { get; set; }

        public decimal Quantity { get; set; }

        public decimal SumNet { get; set; }

        public decimal SumGross { get; set; }

        public decimal SumVat { get; set; }

        public VatRate VatRate { get; set; }
    }
}