using System.Globalization;
using System.Xml;
using Faktury.Classes;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Data.Xml
{
    public class MoneyDataXmlSerializer
    {
        public static XmlElement GetXmlElement(Document document, XmlDocument xmlDoc)
        {
            var moneyDataElement = xmlDoc.CreateElement("MoneyData");
            var documentSummary = document.DocumentSummary;
            var netto = xmlDoc.CreateElement("Netto");
            netto.InnerText = documentSummary.Netto.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(netto);

            var vat = xmlDoc.CreateElement("TotalVAT");
            vat.InnerText = documentSummary.TotalVat.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(vat);

            var brutto = xmlDoc.CreateElement("Brutto");
            brutto.InnerText = documentSummary.Brutto.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(brutto);

            var inWords = xmlDoc.CreateElement("InWords");
            inWords.InnerText = documentSummary.InWords;
            moneyDataElement.AppendChild(inWords);

            var records = xmlDoc.CreateElement("Records");
            moneyDataElement.AppendChild(records);

            foreach (var currentRecord in document.Items)
                records.AppendChild(MoneyDataRecordXmlSerializer.GetXmlElement(currentRecord, xmlDoc));

            return moneyDataElement;
        }

        public static DocumentSummary GetMoneyDataFromXml(Document document, XmlNode xmlElement)
        {
            var newMoneyData = new DocumentSummary
            {
                Netto = float.Parse(xmlElement["Netto"].InnerText),
                TotalVat = float.Parse(xmlElement["TotalVAT"].InnerText),
                Brutto = float.Parse(xmlElement["Brutto"].InnerText),
                InWords = xmlElement["InWords"].InnerText
            };


            foreach (XmlElement currentElement in xmlElement["Records"])
                document.Items.Add(MoneyDataRecordXmlSerializer.GetRecordFromXml(currentElement));

            if (xmlElement["Vat"] != null)
            {
                int vat = int.Parse(xmlElement["Vat"].InnerText);
                foreach (var item in document.Items)
                {
                    item.Vat = vat;
                }
                    
            }
                

            return newMoneyData;
        }
    }
}