using System;
using System.Globalization;
using System.Xml;
using Faktury.Classes;

namespace Faktury.Data.Xml
{
    public class VatRateXmlSerializer
    {
        public const string VatRateElement = "VATRate";

        public XmlElement GetXmlElement(VatRate document, XmlDocument xmlDoc)
        {
            var vatRateXml = xmlDoc.CreateElement("VatRate");


            var inverseVat = xmlDoc.CreateElement("InverseVat");
            var symbol = xmlDoc.CreateElement("Symbol");
            var vatPercent = xmlDoc.CreateElement("VatPercent");

            vatPercent.InnerText = document.VatPercent.ToString(CultureInfo.InvariantCulture);
            symbol.InnerText = document.Symbol;
            inverseVat.InnerText = Convert.ToString(document.IsInverseVat);

            return vatRateXml;
        }

        public static VatRate GetVatRateFromXml(XmlNode xmlElement)
        {
            var inverseVat = xmlElement["InverseVat"];
            var symbol = xmlElement["Symbol"];
            var vatPercent = xmlElement["VatPercent"];

            return new VatRate(symbol.InnerText, Convert.ToDecimal(vatPercent.InnerText),
                Convert.ToBoolean(inverseVat.InnerText));
        }
    }
}