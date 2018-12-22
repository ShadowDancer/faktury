using System.Globalization;
using System.Xml;
using Faktury.Classes;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Data.Xml
{
    public class MoneyDataXmlSerializer
    {
        public static XmlElement GetXmlElement(MoneyData moneyData, XmlDocument xmlDoc)
        {
            var moneyDataElement = xmlDoc.CreateElement("MoneyData");

            var netto = xmlDoc.CreateElement("Netto");
            netto.InnerText = moneyData.Netto.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(netto);

            var vat = xmlDoc.CreateElement("TotalVAT");
            vat.InnerText = moneyData.TotalVat.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(vat);

            var brutto = xmlDoc.CreateElement("Brutto");
            brutto.InnerText = moneyData.Brutto.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(brutto);

            var inWords = xmlDoc.CreateElement("InWords");
            inWords.InnerText = moneyData.InWords;
            moneyDataElement.AppendChild(inWords);

            var records = xmlDoc.CreateElement("Records");
            moneyDataElement.AppendChild(records);

            foreach (var currentRecord in moneyData.Records)
                records.AppendChild(MoneyDataRecordXmlSerializer.GetXmlElement(currentRecord, xmlDoc));

            return moneyDataElement;
        }

        public static MoneyData GetMoneyDataFromXml(XmlNode xmlElement)
        {
            var newMoneyData = new MoneyData
            {
                Netto = float.Parse(xmlElement["Netto"].InnerText),
                TotalVat = float.Parse(xmlElement["TotalVAT"].InnerText),
                Brutto = float.Parse(xmlElement["Brutto"].InnerText),
                InWords = xmlElement["InWords"].InnerText
            };


            foreach (XmlElement currentElement in xmlElement["Records"])
                newMoneyData.Records.Add(MoneyDataRecordXmlSerializer.GetRecordFromXml(currentElement));

            if (xmlElement["Vat"] != null)
                foreach (var record in newMoneyData.Records)
                    record.Vat = int.Parse(xmlElement["Vat"].InnerText);

            return newMoneyData;
        }
    }
}