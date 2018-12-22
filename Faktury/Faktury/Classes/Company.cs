using System;
using System.Xml;

namespace Faktury.Classes
{
    public class Company
    {
        public string Name { get; set; } = "";
        public string Owner { get; set; } = "";
        public string Adress { get; set; } = "";
        public string Street { get; set; } = "";
        public string Nip { get; set; } = "";
        public string Tag { get; set; } = "";
        public bool Bank { get; set; } = false;
        public string BankAccount { get; set; } = "";
        public string BankSection { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string MobileNumber { get; set; } = "";

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; } = DateTime.Now;

        public int ID;

        public XmlElement GetXmlElement(XmlDocument Doc)
        {
            XmlElement Company = Doc.CreateElement("Company");

            XmlElement Name = Doc.CreateElement("Name");
            XmlElement Owner = Doc.CreateElement("Owner");
            XmlElement Adress = Doc.CreateElement("Adress");
            XmlElement Adress2 = Doc.CreateElement("Adress2");
            XmlElement Nip = Doc.CreateElement("Nip");
            XmlElement Tag = Doc.CreateElement("Tag");
            XmlElement Phone = Doc.CreateElement("Phone");
            XmlElement Bank = Doc.CreateElement("Bank");
            XmlElement BankAccount = Doc.CreateElement("BankAccount");
            XmlElement BankSection = Doc.CreateElement("BankSection");
            XmlElement PhoneNumber = Doc.CreateElement("PhoneNumber");
            XmlElement MobileNumber = Doc.CreateElement("MobileNumber");
            XmlElement IDElem = Doc.CreateElement("ID");

             Name.InnerText = this.Name;
             Owner.InnerText = this.Owner;
             Adress.InnerText = this.Adress;
             Adress2.InnerText = this.Street;
             Nip.InnerText = this.Nip;
             Tag.InnerText = this.Tag;
             Bank.InnerText = Convert.ToString(this.Bank);
             BankAccount.InnerText = this.BankAccount;
             BankSection.InnerText = this.BankSection;
             PhoneNumber.InnerText = this.PhoneNumber;
             MobileNumber.InnerText = this.MobileNumber;
             IDElem.InnerText = this.ID.ToString();

             Company.AppendChild(Name);
             Company.AppendChild(Owner);
             Company.AppendChild(Adress);
             Company.AppendChild(Adress2);
             Company.AppendChild(Nip);
             Company.AppendChild(Tag);
             Company.AppendChild(Phone);
             Company.AppendChild(Bank);
             Company.AppendChild(BankAccount);
             Company.AppendChild(BankSection);
             Company.AppendChild(PhoneNumber);
             Company.AppendChild(MobileNumber);
             Company.AppendChild(IDElem);

             #region Dates
             XmlElement CreationDateElement = Doc.CreateElement("CreationDate");
             {
                 XmlElement Day = Doc.CreateElement("Day");
                 XmlElement Month = Doc.CreateElement("Month");
                 XmlElement Year = Doc.CreateElement("Year");

                 Day.InnerText = Convert.ToString(CreationDate.Day);
                 Month.InnerText = Convert.ToString(CreationDate.Month);
                 Year.InnerText = Convert.ToString(CreationDate.Year);

                 CreationDateElement.AppendChild(Day);
                 CreationDateElement.AppendChild(Month);
                 CreationDateElement.AppendChild(Year);
             }
             Company.AppendChild(CreationDateElement);

             XmlElement ModificationDateElement = Doc.CreateElement("ModificationDate");
             {
                 XmlElement Day = Doc.CreateElement("Day");
                 XmlElement Month = Doc.CreateElement("Month");
                 XmlElement Year = Doc.CreateElement("Year");

                 Day.InnerText = Convert.ToString(ModificationDate.Day);
                 Month.InnerText = Convert.ToString(ModificationDate.Month);
                 Year.InnerText = Convert.ToString(ModificationDate.Year);

                 ModificationDateElement.AppendChild(Day);
                 ModificationDateElement.AppendChild(Month);
                 ModificationDateElement.AppendChild(Year);
             }
             Company.AppendChild(ModificationDateElement);
             #endregion

            return Company;
        }
        public static Company GetCompanyFromXml(XmlNode Element)
        {
            Company NewCompany = new Company();

            NewCompany.Name = Element["Name"].InnerText;
            NewCompany.Adress = Element["Adress"].InnerText;
            NewCompany.Street = Element["Adress2"].InnerText;
            NewCompany.Bank = Convert.ToBoolean(Element["Bank"].InnerText);
            NewCompany.BankAccount = Element["BankAccount"].InnerText;
            NewCompany.BankSection = Element["BankSection"].InnerText;
            NewCompany.MobileNumber = Element["MobileNumber"].InnerText;
            NewCompany.Nip = Element["Nip"].InnerText;
            NewCompany.Owner = Element["Owner"].InnerText;
            NewCompany.PhoneNumber = Element["PhoneNumber"].InnerText;
            NewCompany.Tag = Element["Tag"].InnerText;
            NewCompany.ID = int.Parse(Element["ID"].InnerText);            

            if (Element["CreationDate"] != null)
            {

                NewCompany.CreationDate = new DateTime(int.Parse(Element["CreationDate"]["Year"].InnerText), int.Parse(Element["CreationDate"]["Month"].InnerText), int.Parse(Element["CreationDate"]["Day"].InnerText));
            }
            else NewCompany.CreationDate = DateTime.Now;

            if (Element["ModificationDate"] != null)
            {

                NewCompany.ModificationDate = new DateTime(int.Parse(Element["ModificationDate"]["Year"].InnerText), int.Parse(Element["ModificationDate"]["Month"].InnerText), int.Parse(Element["ModificationDate"]["Day"].InnerText)); ;
            }
            else NewCompany.ModificationDate = DateTime.Now;

            return NewCompany;
        }
    }
}