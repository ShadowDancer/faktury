using System;
using System.Globalization;

namespace Faktury.Domain.Domain
{
    public class VatRate
    {
        public VatRate(string symbol, decimal vatPercent)
        {
            Symbol = symbol;
            VatPercent = vatPercent;
        }

        public VatRate(decimal vatPercent)
        {
            VatPercent = vatPercent;
            Symbol = vatPercent.ToString(CultureInfo.CurrentCulture);
        }

        public string Symbol { get; }

        public decimal VatPercent { get; }

        public static VatRate FromString(string s)
        {
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