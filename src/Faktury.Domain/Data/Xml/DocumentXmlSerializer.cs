using System;
using System.Xml;
using Faktury.Classes;
using Faktury.Data.Xml;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Domain.Data.Xml
{
    public class DocumentXmlSerializer
    {
        public XmlElement GetXmlElement(Document document, XmlDocument xmlDoc)
        {
            XmlElement documentElement = xmlDoc.CreateElement("Document");

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

            XmlElement reverseVATElement = xmlDoc.CreateElement("ReverseVAT");
            reverseVATElement.InnerText = Convert.ToString(document.ReverseVAT);
            documentElement.AppendChild(reverseVATElement);

            XmlElement customerElement = CompanyToXmlSerializer.GetXmlElement(document.Customer, xmlDoc, "Customer");
            documentElement.AppendChild(customerElement);

            XmlElement issuerElement = CompanyToXmlSerializer.GetXmlElement(document.Issuer, xmlDoc, "Issuer");
            documentElement.AppendChild(issuerElement);

            documentElement.AppendChild(DocumentSummaryXmlSerializer.GetXmlElement(document, xmlDoc));

            return documentElement;
        }

        public Document GetDocumentFromXml(XmlNode element, ModelStore modelStore, SettingsAccessor settingsAccessor)
        {
            Document newDocument = new Document
            {
                IssueDate = new DateTime(int.Parse(element["IssueDate"]["Year"].InnerText),
                    int.Parse(element["IssueDate"]["Month"].InnerText),
                    int.Parse(element["IssueDate"]["Day"].InnerText)),
                SellDate = new DateTime(int.Parse(element["SellDate"]["Year"].InnerText),
                    NumberToWordConventer.ConvertMonthToNumber(element["SellDate"]["Month"].InnerText),
                    int.Parse(element["SellDate"]["Day"].InnerText)),
                PaymentType = element["Paynament"].InnerText,
                PaymentTime = element["PaynamentTime"].InnerText,
                Number = Convert.ToInt32(element["Number"].InnerText),
                Year = Convert.ToInt32(element["Year"].InnerText),
                ReverseVAT = false
            };

            newDocument.DocumentSummary = DocumentSummaryXmlSerializer.GetMoneyDataFromXml(newDocument, element["MoneyData"]);

            var customerElement = element["Customer"];
            if (customerElement != null)
            {
                newDocument.Customer = CompanyToXmlSerializer.GetCompanyFromXml(customerElement);
            }
            else
            {
                var companyIdElement = element["CompanyID"];
                if (companyIdElement != null)
                {
                    var companyId = int.Parse(companyIdElement.InnerText);
                    newDocument.Customer = modelStore.CompanyRepository.FindCompany(companyId);
                }
            }

            var issuerElement = element["Issuer"];
            if (issuerElement != null)
            {
                newDocument.Issuer = CompanyToXmlSerializer.GetCompanyFromXml(issuerElement);
            }
            else
            {
                newDocument.Issuer = settingsAccessor.GetSettings().OwnerCompany;
            }

            var reverseVATString = element["ReverseVAT"]?.InnerText;
            if (!string.IsNullOrWhiteSpace(reverseVATString))
            {
                newDocument.ReverseVAT = Convert.ToBoolean(reverseVATString);
            }

            return newDocument;
        }
    }
}