using System;
using System.Collections.Generic;
using System.Xml;

namespace Faktury.Classes
{
    public class MoneyDataRecord
    {
        public string Unit { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public float Count { get; set; }

        public float Netto { get; set; }
        public float VAT { get; set; }
        public float Brutto { get; set; }
        public float VATPrecent { get; set; }

        public XmlElement GetXmlElement(XmlDocument Doc)
        {
            XmlElement Record = Doc.CreateElement("Record");

                XmlElement Unit = Doc.CreateElement("Unit");
                Unit.InnerText = this.Unit;
                Record.AppendChild(Unit);

                XmlElement Name = Doc.CreateElement("Name");
                Name.InnerText = this.Name;
                Record.AppendChild(Name);

                XmlElement Cost = Doc.CreateElement("Cost");
                Cost.InnerText = this.Cost.ToString();
                Record.AppendChild(Cost);

                XmlElement Count = Doc.CreateElement("Count");
                Count.InnerText = this.Count.ToString();
                Record.AppendChild(Count);

                XmlElement Netto = Doc.CreateElement("Netto");
                Netto.InnerText = this.Netto.ToString();
                Record.AppendChild(Netto);

                XmlElement VAT = Doc.CreateElement("VAT");
                VAT.InnerText = this.VAT.ToString();
                Record.AppendChild(VAT);

                XmlElement VATPrecent = Doc.CreateElement("VATPrecent");
                VATPrecent.InnerText = this.VATPrecent.ToString();
                Record.AppendChild(VATPrecent);

                XmlElement Brutto = Doc.CreateElement("Brutto");
                Brutto.InnerText = this.Brutto.ToString();
                Record.AppendChild(Brutto);

            return Record;
        }

        public static MoneyDataRecord GetRecordFromXml(XmlElement Element)
        {
            MoneyDataRecord NewRecord = new MoneyDataRecord();

            NewRecord.Unit = Element["Unit"].InnerText;
            NewRecord.Name = Element["Name"].InnerText;
            NewRecord.Cost = float.Parse(Element["Cost"].InnerText);
            NewRecord.Count = float.Parse(Element["Count"].InnerText);

            NewRecord.Netto = float.Parse(Element["Netto"].InnerText);
            NewRecord.VAT = float.Parse(Element["VAT"].InnerText);
            NewRecord.Brutto = float.Parse(Element["Brutto"].InnerText);

            if (Element["VATPrecent"] != null) NewRecord.VATPrecent = float.Parse(Element["VATPrecent"].InnerText);
            else NewRecord.VATPrecent = 22.0f;

            return NewRecord;
        }
    }

    public class MoneyData
    {
        public MoneyData()
        {
            Records = new List<MoneyDataRecord>();
            InWords = "";
        }

        public float Netto { get; set; }
        public float TotalVAT { get; set; }
        public float Brutto { get; set; }

        public string InWords { get; set; }

        public List<MoneyDataRecord> Records { get; set; }

        public XmlElement GetXmlElement(XmlDocument Doc)
        {
            XmlElement MoneyDataElement = Doc.CreateElement("MoneyData");
            
            XmlElement Netto = Doc.CreateElement("Netto");
            Netto.InnerText = this.Netto.ToString();
            MoneyDataElement.AppendChild(Netto);

            XmlElement VAT = Doc.CreateElement("TotalVAT");
            VAT.InnerText = this.TotalVAT.ToString();
            MoneyDataElement.AppendChild(VAT);

            XmlElement Brutto = Doc.CreateElement("Brutto");
            Brutto.InnerText = this.Brutto.ToString();
            MoneyDataElement.AppendChild(Brutto);

            XmlElement InWords = Doc.CreateElement("InWords");
            InWords.InnerText = this.InWords;
            MoneyDataElement.AppendChild(InWords);

            XmlElement Records = Doc.CreateElement("Records");
            MoneyDataElement.AppendChild(Records);

            foreach (var CurrentRecord in this.Records)
            {
                Records.AppendChild(CurrentRecord.GetXmlElement(Doc));
            }

            return MoneyDataElement;
        }

        public static MoneyData GetMoneyDataFromXml(XmlNode Element)
        {
            MoneyData NewMoneyData = new MoneyData();

            NewMoneyData.Netto = float.Parse(Element["Netto"].InnerText);
            NewMoneyData.TotalVAT = float.Parse(Element["TotalVAT"].InnerText);
            NewMoneyData.Brutto = float.Parse(Element["Brutto"].InnerText);
            NewMoneyData.InWords = Element["InWords"].InnerText;

            foreach(XmlElement CurrentElement in Element["Records"])
            {
                NewMoneyData.Records.Add(MoneyDataRecord.GetRecordFromXml(CurrentElement));
            }

            if(Element["Vat"] != null)
                foreach (var Record in NewMoneyData.Records)
                {
                    Record.VAT = int.Parse(Element["Vat"].InnerText);
                }

            return NewMoneyData;
        }

    }
}
