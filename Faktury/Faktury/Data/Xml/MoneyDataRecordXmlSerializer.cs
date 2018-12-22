using System.Globalization;
using System.Xml;
using Faktury.Classes;
// ReSharper disable PossibleNullReferenceException

namespace Faktury.Data.Xml
{
    public class MoneyDataRecordXmlSerializer
    {
        public static XmlElement GetXmlElement(MoneyDataRecord moneyData, XmlDocument xmlDoc)
        {
            XmlElement record = xmlDoc.CreateElement("Record");

            XmlElement unit = xmlDoc.CreateElement("Unit");
            unit.InnerText = moneyData.Unit;
            record.AppendChild(unit);

            XmlElement name = xmlDoc.CreateElement("Name");
            name.InnerText = moneyData.Name;
            record.AppendChild(name);

            XmlElement cost = xmlDoc.CreateElement("Cost");
            cost.InnerText = moneyData.Cost.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(cost);

            XmlElement count = xmlDoc.CreateElement("Count");
            count.InnerText = moneyData.Count.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(count);

            XmlElement netto = xmlDoc.CreateElement("Netto");
            netto.InnerText = moneyData.Netto.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(netto);

            XmlElement vat = xmlDoc.CreateElement("VAT");
            vat.InnerText = moneyData.Vat.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(vat);

            XmlElement vatPercent = xmlDoc.CreateElement("VATPrecent");
            vatPercent.InnerText = moneyData.VatPrecent.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(vatPercent);

            XmlElement brutto = xmlDoc.CreateElement("Brutto");
            brutto.InnerText = moneyData.Brutto.ToString(CultureInfo.InvariantCulture);
            record.AppendChild(brutto);

            return record;
        }

        public static MoneyDataRecord GetRecordFromXml(XmlElement xmlElement)
        {
            MoneyDataRecord newRecord = new MoneyDataRecord
            {
                Unit = xmlElement["Unit"].InnerText,
                Name = xmlElement["Name"].InnerText,
                Cost = float.Parse(xmlElement["Cost"].InnerText),
                Count = float.Parse(xmlElement["Count"].InnerText),
                Netto = float.Parse(xmlElement["Netto"].InnerText),
                Vat = float.Parse(xmlElement["VAT"].InnerText),
                Brutto = float.Parse(xmlElement["Brutto"].InnerText)
            };



            if (xmlElement["VATPrecent"] != null)
            {
                newRecord.VatPrecent = float.Parse(xmlElement["VATPrecent"].InnerText);
            }
            else newRecord.VatPrecent = 22.0f;

            return newRecord;
        }
    }
}