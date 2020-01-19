using System;
using System.Globalization;
using System.Xml;
using Faktury.Classes;

namespace Faktury.Data.Xml
{
    public class VatRateXmlSerializer
    {
        public const string VatRateElement = "VatRate";

        public XmlElement GetXmlElement(VatRate document, XmlDocument xmlDoc)
        {
            var vatRateXml = xmlDoc.CreateElement(VatRateElement);
            var symbol = xmlDoc.CreateElement("Symbol");
            symbol.InnerText = document.Symbol;
            vatRateXml.AppendChild(symbol);

            var vatPercent = xmlDoc.CreateElement("VatPercent");
            vatPercent.InnerText = document.VatPercent.ToString(CultureInfo.InvariantCulture);
            vatRateXml.AppendChild(vatPercent);

            return vatRateXml;
        }

        public static VatRate GetVatRateFromXml(XmlNode xmlElement)
        {
            var inverseVat = xmlElement["InverseVat"];
            var symbol = xmlElement["Symbol"];
            var vatPercent = xmlElement["VatPercent"];

            return new VatRate(symbol.InnerText, Convert.ToDecimal(vatPercent.InnerText));
        }
    }
}