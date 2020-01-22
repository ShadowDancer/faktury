using System.Globalization;
using System.Xml;
using Faktury.Classes;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Domain.Data.Xml
{
    public class DocumentItemXmlSerializer
    {
        public static XmlElement GetXmlElement(DocumentItem moneyData, XmlDocument xmlDoc)
        {
            var record = xmlDoc.CreateElement("Record");

            var unit = xmlDoc.CreateElement("Unit");
            unit.InnerText = moneyData.Unit;
            record.AppendChild(unit);

            var name = xmlDoc.CreateElement("Name");
            name.InnerText = moneyData.Name;
            record.AppendChild(name);

            var cost = xmlDoc.CreateElement("Cost");
            cost.InnerText = moneyData.PriceNet.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(cost);

            var count = xmlDoc.CreateElement("Count");
            count.InnerText = moneyData.Quantity.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(count);

            var netto = xmlDoc.CreateElement("Netto");
            netto.InnerText = moneyData.SumNet.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(netto);

            var vat = xmlDoc.CreateElement("VAT");
            vat.InnerText = moneyData.SumVat.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(vat);

            var vatRate = new VatRateXmlSerializer().GetXmlElement(moneyData.VatRate, xmlDoc);
            record.AppendChild(vatRate);

            var brutto = xmlDoc.CreateElement("Brutto");
            brutto.InnerText = moneyData.SumGross.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(brutto);

            if (!string.IsNullOrWhiteSpace(moneyData.PKWiU))
            {
                // ReSharper disable once IdentifierTypo
                var pkwiu = xmlDoc.CreateElement("PKWiU");
                pkwiu.InnerText = moneyData.PKWiU;
                record.AppendChild(pkwiu);
            }

            return record;
        }

        public static DocumentItem GetRecordFromXml(XmlElement xmlElement)
        {
            var newRecord = new DocumentItem
            {
                Unit = xmlElement["Unit"].InnerText,
                Name = xmlElement["Name"].InnerText,
                PriceNet = decimal.Parse(xmlElement["Cost"].InnerText, CultureInfo.InvariantCulture),
                Quantity = decimal.Parse(xmlElement["Count"].InnerText, CultureInfo.InvariantCulture),
                SumNet = decimal.Parse(xmlElement["Netto"].InnerText, CultureInfo.InvariantCulture),
                SumVat = decimal.Parse(xmlElement["VAT"].InnerText, CultureInfo.InvariantCulture),
                SumGross = decimal.Parse(xmlElement["Brutto"].InnerText, CultureInfo.InvariantCulture),
                PKWiU = xmlElement["PKWiU"]?.InnerText
            };

            var vatRateElement = xmlElement[VatRateXmlSerializer.VatRateElement];
            if (vatRateElement != null)
            {
                newRecord.VatRate = VatRateXmlSerializer.GetVatRateFromXml(vatRateElement);
            }
            else
            {
                if (xmlElement["VATPrecent"] != null)
                {
                    newRecord.VatRate = new VatRate(decimal.Parse(xmlElement["VATPrecent"].InnerText));
                }
                else
                {
                    newRecord.VatRate = new VatRate(23);
                }
            }


            return newRecord;
        }
    }
}