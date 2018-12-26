using System;
using System.Xml;
using Faktury.Classes;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Data.Xml
{
    public class DocumentXmlSerializer
    {
        private readonly ModelStore _modelStore;

        public DocumentXmlSerializer(ModelStore modelStore)
        {
            _modelStore = modelStore;
        }

        public XmlElement GetXmlElement(Document document, XmlDocument xmlDoc)
        {
            XmlElement documentElement = xmlDoc.CreateElement("Document");

            XmlElement companyTag = xmlDoc.CreateElement("CompanyID");
            companyTag.InnerText = document.CompanyId.ToString();
            documentElement.AppendChild(companyTag);


            #region IssueDate

            XmlElement issueDateElement = xmlDoc.CreateElement("IssueDate");
            {
                XmlElement day = xmlDoc.CreateElement("Day");
                XmlElement month = xmlDoc.CreateElement("Month");
                XmlElement year = xmlDoc.CreateElement("Year");

                day.InnerText = Convert.ToString(document.IssueDate.Day);
                month.InnerText = Convert.ToString(document.IssueDate.Month);
                year.InnerText = Convert.ToString(document.IssueDate.Year);

                issueDateElement.AppendChild(day);
                issueDateElement.AppendChild(month);
                issueDateElement.AppendChild(year);
            }
            documentElement.AppendChild(issueDateElement);

            #endregion
            #region SellDate

            XmlElement sellDateElement = xmlDoc.CreateElement("SellDate");
            {
                XmlElement day = xmlDoc.CreateElement("Day");
                XmlElement month = xmlDoc.CreateElement("Month");
                XmlElement year = xmlDoc.CreateElement("Year");

                day.InnerText = Convert.ToString(document.SellDate.Day);
                month.InnerText = Convert.ToString(document.SellDate.Month);
                year.InnerText = Convert.ToString(document.SellDate.Year);

                sellDateElement.AppendChild(day);
                sellDateElement.AppendChild(month);
                sellDateElement.AppendChild(year);
            }
            documentElement.AppendChild(sellDateElement);

            #endregion

            XmlElement payment = xmlDoc.CreateElement("Paynament");
            payment.InnerText = document.PaymentType;
            documentElement.AppendChild(payment);

            XmlElement paymentTime = xmlDoc.CreateElement("PaynamentTime");
            paymentTime.InnerText = document.PaymentTime;
            documentElement.AppendChild(paymentTime);

            XmlElement number = xmlDoc.CreateElement("Number");
            number.InnerText = Convert.ToString(document.Number);
            documentElement.AppendChild(number);

            XmlElement yearElem = xmlDoc.CreateElement("Year");
            yearElem.InnerText = Convert.ToString(document.Year);
            documentElement.AppendChild(yearElem);
            
            documentElement.AppendChild(DocumentSummaryXmlSerializer.GetXmlElement(document, xmlDoc));

            return documentElement;
        }

        public Document GetDocumentFromXml(XmlNode element)
        {
            Document newDocument = new Document();

            if(element["CompanyID"] != null) newDocument.CompanyId = int.Parse(element["CompanyID"].InnerText);
            if (element["CompanyTag"] != null)
            {
                string text = element["CompanyTag"].InnerText;
                Company comp = _modelStore.Companies.Find(n => n.ShortName == text);
                newDocument.CompanyId = comp.Id;
            }


            newDocument.IssueDate = new DateTime(int.Parse(element["IssueDate"]["Year"].InnerText), int.Parse(element["IssueDate"]["Month"].InnerText), int.Parse(element["IssueDate"]["Day"].InnerText));
            newDocument.SellDate = new DateTime(int.Parse(element["SellDate"]["Year"].InnerText), NumberToWordConventer.ConvertMonthToNumber(element["SellDate"]["Month"].InnerText), int.Parse(element["SellDate"]["Day"].InnerText));

            newDocument.PaymentType = element["Paynament"].InnerText;
            newDocument.PaymentTime = element["PaynamentTime"].InnerText;
            newDocument.Number = Convert.ToInt32 (element["Number"].InnerText);
            newDocument.Year = Convert.ToInt32(element["Year"].InnerText);

            newDocument.DocumentSummary = DocumentSummaryXmlSerializer.GetMoneyDataFromXml(newDocument, element["MoneyData"]);

            return newDocument;
        }
    }
}