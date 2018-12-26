using System;
using System.Globalization;

namespace Faktury.Classes
{
    public class VatRate
    {
        public VatRate(string symbol, decimal vatPercent, bool isInverseVat)
        {
            IsInverseVat = isInverseVat;
            Symbol = symbol;
            VatPercent = vatPercent;
        }

        public VatRate(decimal vatPercent)
        {
            VatPercent = vatPercent;
            Symbol = vatPercent.ToString(CultureInfo.CurrentCulture);
            IsInverseVat = false;
        }

        public bool IsInverseVat { get; }

        public string Symbol { get; }

        public decimal VatPercent { get; }

        public static VatRate InverseVat()
        {
            return new VatRate("-", 0, true);
        }

        public static VatRate FromString(string s)
        {
            if (s == "-")
            {
                return InverseVat();
            }

            if (decimal.TryParse(s, out var d))
            {
                return new VatRate(d);
            }

            throw new InvalidOperationException("Nieznana stawka VAT: " + s);
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}