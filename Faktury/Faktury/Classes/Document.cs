using System;
using System.Drawing;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Faktury.Classes
{
    [Serializable]
    public class Document : Print_Framework.IPrintable
    {
        public int CompanyID { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime SellDate { get; set; }

        public string Paynament { get; set; }
        public string PaynamentTime { get; set; }

        public string Name { get; set; }
        public bool DefaultName { get; set; }

        public int Number { get; set; }
        public int Year { get; set; }

        public MoneyData MoneyData { get; set; }

        public bool Paid { get; set; }

        public XmlElement GetXmlElement(XmlDocument Doc)
        {
            XmlElement DocumentElement = Doc.CreateElement("Document");

            XmlElement CompanyTag = Doc.CreateElement("CompanyID");
            CompanyTag.InnerText = this.CompanyID.ToString();
            DocumentElement.AppendChild(CompanyTag);


            #region IssueDate

            XmlElement IssueDateElement = Doc.CreateElement("IssueDate");
            {
                XmlElement Day = Doc.CreateElement("Day");
                XmlElement Month = Doc.CreateElement("Month");
                XmlElement Year = Doc.CreateElement("Year");

                Day.InnerText = Convert.ToString(IssueDate.Day);
                Month.InnerText = Convert.ToString(IssueDate.Month);
                Year.InnerText = Convert.ToString(IssueDate.Year);

                IssueDateElement.AppendChild(Day);
                IssueDateElement.AppendChild(Month);
                IssueDateElement.AppendChild(Year);
            }
            DocumentElement.AppendChild(IssueDateElement);

            #endregion
            #region SellDate

            XmlElement SellDateElement = Doc.CreateElement("SellDate");
            {
                XmlElement Day = Doc.CreateElement("Day");
                XmlElement Month = Doc.CreateElement("Month");
                XmlElement Year = Doc.CreateElement("Year");

                Day.InnerText = Convert.ToString(SellDate.Day);
                Month.InnerText = Convert.ToString(SellDate.Month);
                Year.InnerText = Convert.ToString(SellDate.Year);

                SellDateElement.AppendChild(Day);
                SellDateElement.AppendChild(Month);
                SellDateElement.AppendChild(Year);
            }
            DocumentElement.AppendChild(SellDateElement);

            #endregion

            XmlElement Paynament = Doc.CreateElement("Paynament");
            Paynament.InnerText = this.Paynament;
            DocumentElement.AppendChild(Paynament);

            XmlElement PaynamentTime = Doc.CreateElement("PaynamentTime");
            PaynamentTime.InnerText = this.PaynamentTime;
            DocumentElement.AppendChild(PaynamentTime);

            XmlElement Name = Doc.CreateElement("Name");
            Name.InnerText = this.Name;
            DocumentElement.AppendChild(Name);

            XmlElement DefaultName = Doc.CreateElement("DefaultName");
            DefaultName.InnerText = Convert.ToString(this.DefaultName);
            DocumentElement.AppendChild(DefaultName);

            XmlElement Number = Doc.CreateElement("Number");
            Number.InnerText = Convert.ToString(this.Number);
            DocumentElement.AppendChild(Number);

            XmlElement YearElem = Doc.CreateElement("Year");
            YearElem.InnerText = Convert.ToString(this.Year);
            DocumentElement.AppendChild(YearElem);

            XmlElement PaidElem = Doc.CreateElement("Paid");
            PaidElem.InnerText = Convert.ToString(this.Paid);
            DocumentElement.AppendChild(PaidElem);

            DocumentElement.AppendChild(MoneyData.GetXmlElement(Doc));

            return DocumentElement;
        }
        
        public static Document GetDocumentFromXml(XmlNode Element)
        {
            Document NewDocument = new Document();

            if(Element["CompanyID"] != null) NewDocument.CompanyID = int.Parse(Element["CompanyID"].InnerText);
            if (Element["CompanyTag"] != null)
            {
                string Text = Element["CompanyTag"].InnerText;
                Classes.Company Comp = Windows.MainForm.Instance.Companies.Find(n => n.Tag == Text);
                NewDocument.CompanyID = Comp.ID;
            }


            NewDocument.IssueDate = new DateTime(int.Parse(Element["IssueDate"]["Year"].InnerText), int.Parse(Element["IssueDate"]["Month"].InnerText), int.Parse(Element["IssueDate"]["Day"].InnerText));
            NewDocument.SellDate = new DateTime(int.Parse(Element["SellDate"]["Year"].InnerText), NumberToWordConventer.ConvertMonthToNumber(Element["SellDate"]["Month"].InnerText), int.Parse(Element["SellDate"]["Day"].InnerText));

            NewDocument.Paynament = Element["Paynament"].InnerText;
            NewDocument.PaynamentTime = Element["PaynamentTime"].InnerText;
            NewDocument.Name = Element["Name"].InnerText;
            NewDocument.DefaultName = Convert.ToBoolean(Element["DefaultName"].InnerText);
            NewDocument.Number = Convert.ToInt32 (Element["Number"].InnerText);
            NewDocument.Year = Convert.ToInt32(Element["Year"].InnerText);

            if(Element["Paid"] != null)NewDocument.Paid = Convert.ToBoolean(Element["Paid"].InnerText);//to remove

            NewDocument.MoneyData = MoneyData.GetMoneyDataFromXml(Element["MoneyData"]);

            return NewDocument;
        }

        public void Print(Print_Framework.PrintElement element)
        {
            Company Company = Windows.MainForm.Instance.Companies.Find(n => n.ID == CompanyID);
            Company OwnerCompany = Windows.MainForm.Instance.Settings.OwnerCompany;
            if (Company == null || OwnerCompany == null)
            {
                System.Windows.Forms.MessageBox.Show("Nie znaleziono firmy! Błąd jest prawdopodobnie spowodowany usunięciem kontrahenta", "Błąd!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            //mniejszy font
            Font FooterFont = new Font("Times New Roman", 15);

            StringFormat Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;
            Format.LineAlignment = StringAlignment.Center;

            StringFormat Sf = new StringFormat();
            Sf.Alignment = StringAlignment.Center;
            Sf.LineAlignment = StringAlignment.Center;

            //nagłówek
            RectangleF HeaderTextPosition = new RectangleF(15, 20, 400, 30);
            element.AddText(OwnerCompany.Name, StringAlignment.Center, HeaderTextPosition);

            HeaderTextPosition.Y += 25;
            element.AddText(OwnerCompany.Owner, StringAlignment.Center, HeaderTextPosition);

            HeaderTextPosition.Y += 25;
            element.AddText(OwnerCompany.Adress, StringAlignment.Center, HeaderTextPosition);

            if (!string.IsNullOrEmpty(OwnerCompany.Street))
            {
                HeaderTextPosition.Y += 25;
                element.AddText(OwnerCompany.Street, StringAlignment.Center, HeaderTextPosition);
            }

            HeaderTextPosition.X += 75;
            HeaderTextPosition.Y += 26;

            

            HeaderTextPosition = new RectangleF(425, 30, 400, 30);

            element.AddText("NIP: " + OwnerCompany.Nip, FooterFont, HeaderTextPosition);
            if (OwnerCompany.Bank == true)
            {
                HeaderTextPosition.Y += 20;
                element.AddText("Bank: " + OwnerCompany.BankSection, FooterFont, HeaderTextPosition);
                HeaderTextPosition.Y += 20;
                element.AddText(OwnerCompany.BankAccount, FooterFont, HeaderTextPosition);
            }

            HeaderTextPosition.Y += 17;

            if (!string.IsNullOrEmpty(OwnerCompany.PhoneNumber))
            {
                HeaderTextPosition.Y += 17;                
                element.AddText("Telefon: " + OwnerCompany.PhoneNumber, FooterFont, HeaderTextPosition);

            }
            if (!string.IsNullOrEmpty(OwnerCompany.MobileNumber))
            {
                HeaderTextPosition.Y += 17;
                element.AddText("Komórka: " + OwnerCompany.MobileNumber, FooterFont, HeaderTextPosition);
            }

            HeaderTextPosition.Y += 21;
            int MiddlePartPosition = 150;


            //linia
            element.AddHorizontalLine(MiddlePartPosition);
            //środek


            //numer
            element.AddText("Faktura VAT nr: " + Number.ToString() + "/" + Year.ToString(), new PointF(50, MiddlePartPosition + 10));

            //daty
            element.AddText(string.Format("Data wystawienia: {0}.{1}.{2}", IssueDate.Day.ToString("00"), IssueDate.Month.ToString("00"), IssueDate.Year), new PointF(420, MiddlePartPosition + 30));
            element.AddText(string.Format("Data sprzedaży: {0} {1}", NumberToWordConventer.ConvertNumberToMonth(SellDate.Month.ToString()), SellDate.Year.ToString()), new PointF(420, MiddlePartPosition + 60));

            //płatność
            string[] PaynamentTextes = new String[2] { "Forma płatności: " + Paynament, "Termin płatności: " + PaynamentTime };
            float[] PaynamentLengths = new float[2] { System.Windows.Forms.TextRenderer.MeasureText(PaynamentTextes[0], Print_Framework.PrintEngine.Instane.DefaultFont).Width, Graphics.FromHwnd(new IntPtr()).MeasureString(PaynamentTextes[1], Print_Framework.PrintEngine.Instane.DefaultFont).Width };
            PointF[] PaynamentPositions = new PointF[2] { new PointF(475, MiddlePartPosition + 185), new PointF(475, MiddlePartPosition + 215) };

            float xPosition = 475;
            if (PaynamentLengths[0] > PaynamentLengths[1])
            {
                if (PaynamentLengths[0] > 300)
                {
                    xPosition -= PaynamentLengths[0] - 300;
                }
            }
            else
            {
                if (PaynamentLengths[1] > 300)
                {
                    xPosition -= PaynamentLengths[1] - 300;
                }
            }
            PaynamentPositions[0].X = PaynamentPositions[1].X = xPosition;

            element.AddText(PaynamentTextes[0], PaynamentPositions[0]);
            element.AddText(PaynamentTextes[1], PaynamentPositions[1]);

            //nabywca
            element.AddText("Nabywca:", new PointF(50, MiddlePartPosition + 80));

            element.AddText(Company.Name, FooterFont, new PointF(65, MiddlePartPosition + 120));
            element.AddText(Company.Owner, FooterFont, new PointF(65, MiddlePartPosition + 140));
            element.AddText(Company.Adress, FooterFont, new PointF(65, MiddlePartPosition + 160));
            element.AddText(Company.Street, FooterFont, new PointF(65, MiddlePartPosition + 180));
            element.AddText("NIP:" + Company.Nip, FooterFont, new PointF(65, MiddlePartPosition + 210));

            //rameczka
            element.AddRectangle(new RectangleF(50, MiddlePartPosition + 250, 725, 475));

            //puste miejsce
            element.AddRectangle(new RectangleF(50, MiddlePartPosition + 550, 275, 125));

            /////////////////////////
            //linie poziome
            ////////////////////////

            //nagłówek tabeli

            //linia na nagłowek
            int[] FieldSizes = { 35, 150, 40, 50, 120, 90, 55, 85, 100 };
            string[] FieldNames = { "LP", "Nazwa", "jm", "Ilość", "Cena jed.bez\npod. VAT", "Wartość bez VAT", "VAT\n%", "Kwota Vat", "Wartość z pod. VAT" };

            Font ExtraSmall = new Font("Times New Roman", 12);

            //napisy do nagłówka
            PrintItem(element, new PointF(50, MiddlePartPosition + 250), FieldSizes, FieldNames, FooterFont);
            //itemy

            PointF Position1 = new PointF(50, MiddlePartPosition + 300);
            for (int i = 0; i < MoneyData.Records.Count; i++)
            {
                List<string> Input = new List<string>();
                int Lp = i + 1;
                Input.Add(Lp.ToString());
                Input.Add(MoneyData.Records[i].Name.ToString());
                Input.Add(MoneyData.Records[i].Unit.ToString());
                Input.Add(MoneyData.Records[i].Count.ToString());
                Input.Add(MoneyData.Records[i].Cost.ToString("0.00"));
                Input.Add(MoneyData.Records[i].Netto.ToString("0.00"));
                Input.Add(MoneyData.Records[i].VATPrecent.ToString());
                Input.Add(MoneyData.Records[i].VAT.ToString("0.00"));
                Input.Add(MoneyData.Records[i].Brutto.ToString("0.00"));

                PrintItem(element, Position1, FieldSizes, Input.ToArray(), ExtraSmall);

                Position1.Y += 60;
            }


            //linie na razem, w tym
            RectangleF LinePos = new RectangleF(325, MiddlePartPosition + 550, 775, MiddlePartPosition + 550);
            element.AddLine(LinePos);
            LinePos.Y += 50; LinePos.Height += 50;
            element.AddLine(LinePos);
            LinePos.Y += 50; LinePos.Height += 50;
            element.AddLine(LinePos);
            LinePos.Y += 25; LinePos.Height += 25;
            element.AddLine(LinePos);

            //mała tabelka
            int Offset = 0;
            LinePos = new RectangleF(325 + Offset, MiddlePartPosition + 550, 120, 50);
            element.AddText("Razem:", Sf, FooterFont, null, LinePos);
            LinePos.Y += 50;
            element.AddText("w tym:", Sf, FooterFont, null, LinePos);

            Offset += (int)LinePos.Width;
            LinePos = new RectangleF(325 + Offset, MiddlePartPosition + 550, 90, 50);
            element.AddText(MoneyData.Netto.ToString("0.00"), Sf, ExtraSmall, null, LinePos);
            LinePos.Y += 50;
            element.AddText(MoneyData.Netto.ToString("0.00"), Sf, ExtraSmall, null, LinePos);

            Offset += (int)LinePos.Width;
            LinePos = new RectangleF(325 + Offset, MiddlePartPosition + 550, 55, 50);
            element.AddText(23.ToString(), Sf, ExtraSmall, null, LinePos);//TODO
            LinePos.Y += 50;
            element.AddText(23.ToString(), Sf, ExtraSmall, null, LinePos);//TODO

            Offset += (int)LinePos.Width;
            LinePos = new RectangleF(325 + Offset, MiddlePartPosition + 550, 85, 50);
            element.AddText(MoneyData.TotalVAT.ToString("0.00"), Sf, ExtraSmall, null, LinePos);
            LinePos.Y += 50;
            element.AddText(MoneyData.TotalVAT.ToString("0.00"), Sf, ExtraSmall, null, LinePos);

            Offset += (int)LinePos.Width;
            LinePos = new RectangleF(325 + Offset, MiddlePartPosition + 550, 100, 50);
            element.AddText(MoneyData.Brutto.ToString("0.00"), Sf, ExtraSmall, null, LinePos);
            LinePos.Y += 50;
            element.AddText(MoneyData.Brutto.ToString("0.00"), Sf, ExtraSmall, null, LinePos);


            ////////////////////////
            //linie pionowe
            ////////////////////////
            //linie do tabelki
            RectangleF Position = new RectangleF(50, MiddlePartPosition + 250, 50, MiddlePartPosition + 550);
            for (int i = 0; i < FieldSizes.Length; i++)
            {
                element.AddLine(Position);
                Position.X += FieldSizes[i];
                Position.Width += FieldSizes[i];
                if (i == 3) Position.Height += 125;
            }


            //słownie

            element.AddText("Słownie:", Sf, FooterFont, null, new RectangleF(50, MiddlePartPosition + 675, 100, 50));
            Format.Alignment = StringAlignment.Near;

            int Size = System.Windows.Forms.TextRenderer.MeasureText(MoneyData.InWords, ExtraSmall).Width;
            if (Size >= 570)
            {
                Font VeryVerySmall = new Font("Times New Roman", 11);
                element.AddText(MoneyData.InWords, Format, VeryVerySmall, null, new RectangleF(155, MiddlePartPosition + 675, 725, 50));
            }
            else
            {
                element.AddText(MoneyData.InWords, Format, ExtraSmall, null, new RectangleF(155, MiddlePartPosition + 675, 725, 50));
            }

            int LineOffset = 150;

            element.AddLine(new RectangleF(LineOffset, MiddlePartPosition + 675, LineOffset, MiddlePartPosition + 725));


            //stopka
            int FooterPartPosition = 900;
            element.AddText("Faktura jest wezwaniem do zapłaty\nPotwierdzenie odbioru faktury jest jednocześnie potwierdzeniem wykonania zlecenia.", FooterFont, new PointF(50, FooterPartPosition));
            element.AddText("W/G oświadczenia\npodpis osoby uprawnionej do odbioru faktury", StringAlignment.Center, FooterFont, new RectangleF(0, FooterPartPosition + 70, 350, FooterPartPosition + 70));
            element.AddText("Podpis osoby uprawnionej\ndo wystawienia faktury", StringAlignment.Center, FooterFont, new RectangleF(300, FooterPartPosition + 70, 700, FooterPartPosition + 70));
            int Length = 300;
            int x = 25;
            element.AddLine(new RectangleF(x, FooterPartPosition + 190, x + Length, FooterPartPosition + 190));
            x = 500;
            element.AddLine(new RectangleF(x, FooterPartPosition + 190, x + Length, FooterPartPosition + 190));
        }
        private void PrintItem(Print_Framework.PrintElement element, PointF Pos, int[] FieldSizes, string[] FieldNames, Font Font)
        {
            //wypisz nazwy
            RectangleF Position = new RectangleF(Pos.X, Pos.Y, 0, 50);
            for (int i = 0; i < FieldSizes.Length; i++)
            {
                Position.Width = FieldSizes[i];
                StringFormat Sf = new StringFormat();
                Sf.Alignment = StringAlignment.Center;
                Sf.LineAlignment = StringAlignment.Center;
                element.AddText(FieldNames[i], Sf, Font, Print_Framework.PrintEngine.Instane.DefaultBrush, Position);
                Position.X += FieldSizes[i];
            }

            //podkreśl
            element.AddLine(new RectangleF(50, Pos.Y + 50, 775, Pos.Y + 50));

        }

        public void Print()
        {
            Windows.MainForm.Instance.PrintEngine.AddPrintObject(this);
            Windows.MainForm.Instance.PrintEngine.Print();
            Windows.MainForm.Instance.PrintEngine.ClearPrintingObjects();
        }

        public void ShowPreview()
        {
            Windows.MainForm.Instance.PrintEngine.AddPrintObject(this);
            Windows.MainForm.Instance.PrintEngine.ShowPreview();
            Windows.MainForm.Instance.PrintEngine.ClearPrintingObjects();
        }

        public Document()
        {
            IssueDate = new DateTime();
            SellDate = new DateTime();
            MoneyData = new MoneyData();
        }

        public static Document CreateNewDocument()
        {

            Document NewDocument = new Document();
            NewDocument.Name = "bez_nazwy";
            NewDocument.DefaultName = true;
            NewDocument.Paynament = "Przelew";
            NewDocument.PaynamentTime = "14 dni";
            NewDocument.Year = DateTime.Today.Year;

            NewDocument.IssueDate = DateTime.Today;
            NewDocument.SellDate = DateTime.Today;

            return NewDocument;
        }

    }
}
