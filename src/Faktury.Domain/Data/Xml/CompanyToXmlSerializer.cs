using System;
using System.Xml;
using Faktury.Domain.Classes;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Domain.Data.Xml
{
    public class CompanyToXmlSerializer
    {
        public static XmlElement GetXmlElement(Company company, XmlDocument xmlDoc, string xmlElementName = "Company")
        {
            XmlElement companyElement = xmlDoc.CreateElement(xmlElementName);

            XmlElement name = xmlDoc.CreateElement("Name");
            XmlElement owner = xmlDoc.CreateElement("Owner");
            XmlElement address = xmlDoc.CreateElement("Adress");
            XmlElement address2 = xmlDoc.CreateElement("Adress2");
            XmlElement nip = xmlDoc.CreateElement("Nip");
            XmlElement tag = xmlDoc.CreateElement("Tag");
            XmlElement phone = xmlDoc.CreateElement("Phone");
            XmlElement bank = xmlDoc.CreateElement("Bank");
            XmlElement bankAccount = xmlDoc.CreateElement("BankAccount");
            XmlElement bankSection = xmlDoc.CreateElement("BankSection");
            XmlElement phoneNumber = xmlDoc.CreateElement("PhoneNumber");
            XmlElement mobileNumber = xmlDoc.CreateElement("MobileNumber");
            XmlElement idElem = xmlDoc.CreateElement("ID");

            name.InnerText = company.Name;
            owner.InnerText = company.Owner;
            address.InnerText = company.Address;
            address2.InnerText = company.Street;
            nip.InnerText = company.Nip;
            tag.InnerText = company.ShortName;
            bank.InnerText = Convert.ToString(company.Bank);
            bankAccount.InnerText = company.BankAccount;
            bankSection.InnerText = company.BankSection;
            phoneNumber.InnerText = company.PhoneNumber;
            mobileNumber.InnerText = company.MobileNumber;
            idElem.InnerText = company.Id.ToString();

            companyElement.AppendChild(name);
            companyElement.AppendChild(owner);
            companyElement.AppendChild(address);
            companyElement.AppendChild(address2);
            companyElement.AppendChild(nip);
            companyElement.AppendChild(tag);
            companyElement.AppendChild(phone);
            companyElement.AppendChild(bank);
            companyElement.AppendChild(bankAccount);
            companyElement.AppendChild(bankSection);
            companyElement.AppendChild(phoneNumber);
            companyElement.AppendChild(mobileNumber);
            companyElement.AppendChild(idElem);

            #region Dates
            XmlElement creationDateElement = xmlDoc.CreateElement("CreationDate");
            {
                XmlElement day = xmlDoc.CreateElement("Day");
                XmlElement month = xmlDoc.CreateElement("Month");
                XmlElement year = xmlDoc.CreateElement("Year");

                day.InnerText = Convert.ToString(company.CreationDate.Day);
                month.InnerText = Convert.ToString(company.CreationDate.Month);
                year.InnerText = Convert.ToString(company.CreationDate.Year);

                creationDateElement.AppendChild(day);
                creationDateElement.AppendChild(month);
                creationDateElement.AppendChild(year);
            }
            companyElement.AppendChild(creationDateElement);

            XmlElement modificationDateElement = xmlDoc.CreateElement("ModificationDate");
            {
                XmlElement day = xmlDoc.CreateElement("Day");
                XmlElement month = xmlDoc.CreateElement("Month");
                XmlElement year = xmlDoc.CreateElement("Year");

                day.InnerText = Convert.ToString(company.ModificationDate.Day);
                month.InnerText = Convert.ToString(company.ModificationDate.Month);
                year.InnerText = Convert.ToString(company.ModificationDate.Year);

                modificationDateElement.AppendChild(day);
                modificationDateElement.AppendChild(month);
                modificationDateElement.AppendChild(year);
            }
            companyElement.AppendChild(modificationDateElement);
            #endregion

            return companyElement;
        }

        public static Company GetCompanyFromXml(XmlNode xmlElement)
        {
            Company company = new Company
            {
                Name = xmlElement["Name"].InnerText,
                Address = xmlElement["Adress"].InnerText,
                Street = xmlElement["Adress2"].InnerText,
                Bank = Convert.ToBoolean(xmlElement["Bank"].InnerText),
                BankAccount = xmlElement["BankAccount"].InnerText,
                BankSection = xmlElement["BankSection"].InnerText,
                MobileNumber = xmlElement["MobileNumber"].InnerText,
                Nip = xmlElement["Nip"].InnerText,
                Owner = xmlElement["Owner"].InnerText,
                PhoneNumber = xmlElement["PhoneNumber"].InnerText,
                ShortName = xmlElement["Tag"].InnerText,
                Id = int.Parse(xmlElement["ID"].InnerText)
            };



            if (xmlElement["CreationDate"] != null)
            {

                company.CreationDate = new DateTime(int.Parse(xmlElement["CreationDate"]["Year"].InnerText), int.Parse(xmlElement["CreationDate"]["Month"].InnerText), int.Parse(xmlElement["CreationDate"]["Day"].InnerText));
            }
            else company.CreationDate = DateTime.Now;

            if (xmlElement["ModificationDate"] != null)
            {

                company.ModificationDate = new DateTime(int.Parse(xmlElement["ModificationDate"]["Year"].InnerText), int.Parse(xmlElement["ModificationDate"]["Month"].InnerText), int.Parse(xmlElement["ModificationDate"]["Day"].InnerText));
            }
            else company.ModificationDate = DateTime.Now;

            return company;
        }
    }
}