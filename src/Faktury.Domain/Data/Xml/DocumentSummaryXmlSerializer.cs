using System.Globalization;
using System.Xml;
using Faktury.Domain.Domain;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Domain.Data.Xml
{
    public class DocumentSummaryXmlSerializer
    {
        public static XmlElement GetXmlElement(Document document, XmlDocument xmlDoc)
        {
            var moneyDataElement = xmlDoc.CreateElement("MoneyData");
            var documentSummary = document.DocumentSummary;
            var netto = xmlDoc.CreateElement("Netto");
            netto.InnerText = documentSummary.TotalNet.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(netto);

            var vat = xmlDoc.CreateElement("TotalVAT");
            vat.InnerText = documentSummary.TotalVat.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(vat);

            var brutto = xmlDoc.CreateElement("Brutto");
            brutto.InnerText = documentSummary.TotalGross.ToString(CultureInfo.InvariantCulture);
            moneyDataElement.AppendChild(brutto);

            var inWords = xmlDoc.CreateElement("InWords");
            inWords.InnerText = documentSummary.TotalInWords;
            moneyDataElement.AppendChild(inWords);

            var records = xmlDoc.CreateElement("Records");
            moneyDataElement.AppendChild(records);

            foreach (var currentRecord in document.Items)
                records.AppendChild(DocumentItemXmlSerializer.GetXmlElement(currentRecord, xmlDoc));

            return moneyDataElement;
        }

        public static DocumentSummary GetMoneyDataFromXml(Document document, XmlNode xmlElement)
        {
            var newMoneyData = new DocumentSummary
            {
                TotalNet = decimal.Parse(xmlElement["Netto"].InnerText, CultureInfo.InvariantCulture),
                TotalVat = decimal.Parse(xmlElement["TotalVAT"].InnerText, CultureInfo.InvariantCulture),
                TotalGross = decimal.Parse(xmlElement["Brutto"].InnerText, CultureInfo.InvariantCulture),
                TotalInWords = xmlElement["InWords"].InnerText
            };


            foreach (XmlElement currentElement in xmlElement["Records"])
                document.Items.Add(DocumentItemXmlSerializer.GetRecordFromXml(currentElement));

            if (xmlElement["Vat"] != null)
            {
                int vat = int.Parse(xmlElement["Vat"].InnerText);
                foreach (var item in document.Items)
                {
                    item.SumVat = vat;
                }
                    
            }
                

            return newMoneyData;
        }
    }
}