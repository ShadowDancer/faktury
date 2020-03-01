using System;
using System.Globalization;
using System.Xml;
using Faktury.Domain.Classes;

// ReSharper disable PossibleNullReferenceException

namespace Faktury.Domain.Data.Xml
{
    public class ServiceToXmlSerializer
    {
        public static Service GetServiceFromXml(XmlNode xmlElement)
        {
            Service newService = new Service
            {
                Name = xmlElement["Name"].InnerText,
                Tag = xmlElement["Tag"].InnerText,
                Unit = xmlElement["jm"].InnerText,
                PriceNet = Convert.ToDecimal(xmlElement["Price"].InnerText),
                Vat = Convert.ToInt32(xmlElement["Vat"].InnerText),
                Id = Convert.ToInt32(xmlElement["ID"].InnerText),
                PKWiU = xmlElement["pkwiu"]?.InnerText
            };


            if (xmlElement["CreationDate"] != null)
            {
                newService.CreationDate = new DateTime(int.Parse(xmlElement["CreationDate"]["Year"].InnerText), int.Parse(xmlElement["CreationDate"]["Month"].InnerText), int.Parse(xmlElement["CreationDate"]["Day"].InnerText));
            }
            else newService.CreationDate = DateTime.Now;

            if (xmlElement["ModificationDate"] != null)
            {
                newService.ModificationDate = new DateTime(int.Parse(xmlElement["ModificationDate"]["Year"].InnerText), int.Parse(xmlElement["ModificationDate"]["Month"].InnerText), int.Parse(xmlElement["ModificationDate"]["Day"].InnerText));
            }
            else newService.ModificationDate = DateTime.Now;

            return newService;

        }

        public static XmlElement GetXmlElement(Service service, XmlDocument xmlDocument)
        {
            XmlElement serviceElement = xmlDocument.CreateElement("Service");

            XmlElement name = xmlDocument.CreateElement("Name");
            XmlElement tag = xmlDocument.CreateElement("Tag");
            XmlElement jm = xmlDocument.CreateElement("jm");
            XmlElement price = xmlDocument.CreateElement("Price");
            XmlElement vat = xmlDocument.CreateElement("Vat");
            XmlElement idElem = xmlDocument.CreateElement("ID");

            if (!string.IsNullOrWhiteSpace(service.PKWiU))
            {
                // ReSharper disable once IdentifierTypo
                XmlElement pkwiuElem = xmlDocument.CreateElement("pkwiu");
                pkwiuElem.InnerText = service.PKWiU;
                serviceElement.AppendChild(pkwiuElem);
            }


            name.InnerText = service.Name;
            tag.InnerText = service.Tag;
            jm.InnerText = service.Unit;
            price.InnerText = service.PriceNet.ToString(CultureInfo.CurrentCulture);
            vat.InnerText = service.Vat.ToString();
            idElem.InnerText = service.Id.ToString();

            serviceElement.AppendChild(name);
            serviceElement.AppendChild(tag);
            serviceElement.AppendChild(jm);
            serviceElement.AppendChild(price);
            serviceElement.AppendChild(vat);
            serviceElement.AppendChild(idElem);


            XmlElement creationDateElement = xmlDocument.CreateElement("CreationDate");
            {
                XmlElement day = xmlDocument.CreateElement("Day");
                XmlElement month = xmlDocument.CreateElement("Month");
                XmlElement year = xmlDocument.CreateElement("Year");

                day.InnerText = Convert.ToString(service.CreationDate.Day);
                month.InnerText = Convert.ToString(service.CreationDate.Month);
                year.InnerText = Convert.ToString(service.CreationDate.Year);

                creationDateElement.AppendChild(day);
                creationDateElement.AppendChild(month);
                creationDateElement.AppendChild(year);
            }
            serviceElement.AppendChild(creationDateElement);

            XmlElement modificationDateElement = xmlDocument.CreateElement("ModificationDate");
            {
                XmlElement day = xmlDocument.CreateElement("Day");
                XmlElement month = xmlDocument.CreateElement("Month");
                XmlElement year = xmlDocument.CreateElement("Year");

                day.InnerText = Convert.ToString(service.ModificationDate.Day);
                month.InnerText = Convert.ToString(service.ModificationDate.Month);
                year.InnerText = Convert.ToString(service.ModificationDate.Year);

                modificationDateElement.AppendChild(day);
                modificationDateElement.AppendChild(month);
                modificationDateElement.AppendChild(year);
            }
            serviceElement.AppendChild(modificationDateElement);

            return serviceElement;
        }
    }
}