using System;
using System.Xml;
using Faktury.Classes;
using Faktury.Print_Framework;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Data.Xml
{
    public class DocumentXmlSerializer
    {
        public static XmlElement GetXmlElement(Document document, XmlDocument xmlDoc)
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
            payment.InnerText = document.Paynament;
            documentElement.AppendChild(payment);

            XmlElement paymentTime = xmlDoc.CreateElement("PaynamentTime");
            paymentTime.InnerText = document.PaynamentTime;
            documentElement.AppendChild(paymentTime);

            XmlElement name = xmlDoc.CreateElement("Name");
            name.InnerText = document.Name;
            documentElement.AppendChild(name);

            XmlElement defaultName = xmlDoc.CreateElement("DefaultName");
            defaultName.InnerText = Convert.ToString(document.DefaultName);
            documentElement.AppendChild(defaultName);

            XmlElement number = xmlDoc.CreateElement("Number");
            number.InnerText = Convert.ToString(document.Number);
            documentElement.AppendChild(number);

            XmlElement yearElem = xmlDoc.CreateElement("Year");
            yearElem.InnerText = Convert.ToString(document.Year);
            documentElement.AppendChild(yearElem);

            XmlElement paidElem = xmlDoc.CreateElement("Paid");
            paidElem.InnerText = Convert.ToString(document.Paid);
            documentElement.AppendChild(paidElem);

            documentElement.AppendChild(MoneyDataXmlSerializer.GetXmlElement(document.MoneyData, xmlDoc));

            return documentElement;
        }

        public static Document GetDocumentFromXml(XmlNode element)
        {
            Document newDocument = new Document();

            if(element["CompanyID"] != null) newDocument.CompanyId = int.Parse(element["CompanyID"].InnerText);
            if (element["CompanyTag"] != null)
            {
                string text = element["CompanyTag"].InnerText;
                Company comp = Windows.MainForm.Instance.Companies.Find(n => n.Tag == text);
                newDocument.CompanyId = comp.Id;
            }


            newDocument.IssueDate = new DateTime(int.Parse(element["IssueDate"]["Year"].InnerText), int.Parse(element["IssueDate"]["Month"].InnerText), int.Parse(element["IssueDate"]["Day"].InnerText));
            newDocument.SellDate = new DateTime(int.Parse(element["SellDate"]["Year"].InnerText), NumberToWordConventer.ConvertMonthToNumber(element["SellDate"]["Month"].InnerText), int.Parse(element["SellDate"]["Day"].InnerText));

            newDocument.Paynament = element["Paynament"].InnerText;
            newDocument.PaynamentTime = element["PaynamentTime"].InnerText;
            newDocument.Name = element["Name"].InnerText;
            newDocument.DefaultName = Convert.ToBoolean(element["DefaultName"].InnerText);
            newDocument.Number = Convert.ToInt32 (element["Number"].InnerText);
            newDocument.Year = Convert.ToInt32(element["Year"].InnerText);

            if(element["Paid"] != null)newDocument.Paid = Convert.ToBoolean(element["Paid"].InnerText);//to remove

            newDocument.MoneyData = MoneyDataXmlSerializer.GetMoneyDataFromXml(element["MoneyData"]);

            return newDocument;
        }
    }
}