using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Faktury.Classes
{
    public class Service
    {
        public int ID = - 1;

        public string Name = "";
        public string Tag = "";
        public string Jm = "";
        public float Price = 0;
        public int Vat = 0;

        public DateTime CreationDate;
        public DateTime ModificationDate;

        public static Service GetServiceFromXml(XmlNode Element)
        {
            Service NewService = new Service();

            NewService.Name = Element["Name"].InnerText;
            NewService.Tag = Element["Tag"].InnerText;
            NewService.Jm = Element["jm"].InnerText;
            NewService.Price = Convert.ToSingle(Element["Price"].InnerText);
            NewService.Vat = Convert.ToInt32(Element["Vat"].InnerText);
            NewService.ID = Convert.ToInt32(Element["ID"].InnerText);

            if (Element["CreationDate"] != null)
            {
                NewService.CreationDate = new DateTime(int.Parse(Element["CreationDate"]["Year"].InnerText), int.Parse(Element["CreationDate"]["Month"].InnerText), int.Parse(Element["CreationDate"]["Day"].InnerText));
            }
            else NewService.CreationDate = DateTime.Now;

            if (Element["ModificationDate"] != null)
            {
                NewService.ModificationDate = new DateTime(int.Parse(Element["ModificationDate"]["Year"].InnerText), int.Parse(Element["ModificationDate"]["Month"].InnerText), int.Parse(Element["ModificationDate"]["Day"].InnerText));
            }
            else NewService.ModificationDate = DateTime.Now;

            return NewService;

        }
        public XmlElement GetXmlElement(XmlDocument Doc)
        {
            XmlElement Service = Doc.CreateElement("Service");

            XmlElement Name = Doc.CreateElement("Name");
            XmlElement Tag = Doc.CreateElement("Tag");
            XmlElement jm = Doc.CreateElement("jm");
            XmlElement Price = Doc.CreateElement("Price");
            XmlElement Vat = Doc.CreateElement("Vat");
            XmlElement IDElem = Doc.CreateElement("ID");

            Name.InnerText = this.Name;
            Tag.InnerText = this.Tag;
            jm.InnerText = this.Jm;
            Price.InnerText = this.Price.ToString();
            Vat.InnerText = this.Vat.ToString();
            IDElem.InnerText = this.ID.ToString();

            Service.AppendChild(Name);
            Service.AppendChild(Tag);
            Service.AppendChild(jm);
            Service.AppendChild(Price);
            Service.AppendChild(Vat);
            Service.AppendChild(IDElem);

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
            Service.AppendChild(CreationDateElement);

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
            Service.AppendChild(ModificationDateElement);
            
            return Service;
        }
    }
}
