using System;
using System.Drawing;
using System.Collections.Generic;
using Faktury.Print_Framework;

namespace Faktury.Classes
{
    [Serializable]
    public class Document : IPrintable
    {
        public int CompanyId { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime SellDate { get; set; }

        public string Paynament { get; set; }
        public string PaynamentTime { get; set; }

        public string Name { get; set; }
        public bool DefaultName { get; set; }

        public int Number { get; set; }
        public int Year { get; set; }

        public MoneyData MoneyData { get; set; } = new MoneyData();

        public bool Paid { get; set; }

        public void Print(PrintElement element)
        {
            Company company = Windows.MainForm.Instance.Companies.Find(n => n.Id == CompanyId);
            Company ownerCompany = Windows.MainForm.Instance.Settings.OwnerCompany;
            if (company == null || ownerCompany == null)
            {
                System.Windows.Forms.MessageBox.Show("Nie znaleziono firmy! Błąd jest prawdopodobnie spowodowany usunięciem kontrahenta", "Błąd!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            //mniejszy font
            Font footerFont = new Font("Times New Roman", 15);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //nagłówek
            RectangleF headerTextPosition = new RectangleF(15, 20, 400, 30);
            element.AddText(ownerCompany.Name, StringAlignment.Center, headerTextPosition);

            headerTextPosition.Y += 25;
            element.AddText(ownerCompany.Owner, StringAlignment.Center, headerTextPosition);

            headerTextPosition.Y += 25;
            element.AddText(ownerCompany.Address, StringAlignment.Center, headerTextPosition);

            if (!string.IsNullOrEmpty(ownerCompany.Street))
            {
                headerTextPosition.Y += 25;
                element.AddText(ownerCompany.Street, StringAlignment.Center, headerTextPosition);
            }

            headerTextPosition.X += 75;
            headerTextPosition.Y += 26;

            

            headerTextPosition = new RectangleF(425, 30, 400, 30);

            element.AddText("NIP: " + ownerCompany.Nip, footerFont, headerTextPosition);
            if (ownerCompany.Bank == true)
            {
                headerTextPosition.Y += 20;
                element.AddText("Bank: " + ownerCompany.BankSection, footerFont, headerTextPosition);
                headerTextPosition.Y += 20;
                element.AddText(ownerCompany.BankAccount, footerFont, headerTextPosition);
            }

            headerTextPosition.Y += 17;

            if (!string.IsNullOrEmpty(ownerCompany.PhoneNumber))
            {
                headerTextPosition.Y += 17;                
                element.AddText("Telefon: " + ownerCompany.PhoneNumber, footerFont, headerTextPosition);

            }
            if (!string.IsNullOrEmpty(ownerCompany.MobileNumber))
            {
                headerTextPosition.Y += 17;
                element.AddText("Komórka: " + ownerCompany.MobileNumber, footerFont, headerTextPosition);
            }

            headerTextPosition.Y += 21;
            int middlePartPosition = 150;


            //linia
            element.AddHorizontalLine(middlePartPosition);
            //środek


            //numer
            element.AddText("Faktura VAT nr: " + Number.ToString() + "/" + Year.ToString(), new PointF(50, middlePartPosition + 10));

            //daty
            element.AddText(string.Format("Data wystawienia: {0}.{1}.{2}", IssueDate.Day.ToString("00"), IssueDate.Month.ToString("00"), IssueDate.Year), new PointF(420, middlePartPosition + 30));
            element.AddText(string.Format("Data sprzedaży: {0} {1}", NumberToWordConventer.ConvertNumberToMonth(SellDate.Month.ToString()), SellDate.Year.ToString()), new PointF(420, middlePartPosition + 60));

            //płatność
            string[] paynamentTextes = new String[2] { "Forma płatności: " + Paynament, "Termin płatności: " + PaynamentTime };
            float[] paynamentLengths = new float[2] { System.Windows.Forms.TextRenderer.MeasureText(paynamentTextes[0], PrintEngine.Instane.DefaultFont).Width, Graphics.FromHwnd(new IntPtr()).MeasureString(paynamentTextes[1], PrintEngine.Instane.DefaultFont).Width };
            PointF[] paynamentPositions = new PointF[2] { new PointF(475, middlePartPosition + 185), new PointF(475, middlePartPosition + 215) };

            float xPosition = 475;
            if (paynamentLengths[0] > paynamentLengths[1])
            {
                if (paynamentLengths[0] > 300)
                {
                    xPosition -= paynamentLengths[0] - 300;
                }
            }
            else
            {
                if (paynamentLengths[1] > 300)
                {
                    xPosition -= paynamentLengths[1] - 300;
                }
            }
            paynamentPositions[0].X = paynamentPositions[1].X = xPosition;

            element.AddText(paynamentTextes[0], paynamentPositions[0]);
            element.AddText(paynamentTextes[1], paynamentPositions[1]);

            //nabywca
            element.AddText("Nabywca:", new PointF(50, middlePartPosition + 80));

            element.AddText(company.Name, footerFont, new PointF(65, middlePartPosition + 120));
            element.AddText(company.Owner, footerFont, new PointF(65, middlePartPosition + 140));
            element.AddText(company.Address, footerFont, new PointF(65, middlePartPosition + 160));
            element.AddText(company.Street, footerFont, new PointF(65, middlePartPosition + 180));
            element.AddText("NIP:" + company.Nip, footerFont, new PointF(65, middlePartPosition + 210));

            //rameczka
            element.AddRectangle(new RectangleF(50, middlePartPosition + 250, 725, 475));

            //puste miejsce
            element.AddRectangle(new RectangleF(50, middlePartPosition + 550, 275, 125));

            /////////////////////////
            //linie poziome
            ////////////////////////

            //nagłówek tabeli

            //linia na nagłowek
            int[] fieldSizes = { 35, 150, 40, 50, 120, 90, 55, 85, 100 };
            string[] fieldNames = { "LP", "Nazwa", "jm", "Ilość", "Cena jed.bez\npod. VAT", "Wartość bez VAT", "VAT\n%", "Kwota Vat", "Wartość z pod. VAT" };

            Font extraSmall = new Font("Times New Roman", 12);

            //napisy do nagłówka
            PrintItem(element, new PointF(50, middlePartPosition + 250), fieldSizes, fieldNames, footerFont);
            //itemy

            PointF position1 = new PointF(50, middlePartPosition + 300);
            for (int i = 0; i < MoneyData.Records.Count; i++)
            {
                List<string> input = new List<string>();
                int lp = i + 1;
                input.Add(lp.ToString());
                input.Add(MoneyData.Records[i].Name.ToString());
                input.Add(MoneyData.Records[i].Unit.ToString());
                input.Add(MoneyData.Records[i].Count.ToString());
                input.Add(MoneyData.Records[i].Cost.ToString("0.00"));
                input.Add(MoneyData.Records[i].Netto.ToString("0.00"));
                input.Add(MoneyData.Records[i].VatPrecent.ToString());
                input.Add(MoneyData.Records[i].Vat.ToString("0.00"));
                input.Add(MoneyData.Records[i].Brutto.ToString("0.00"));

                PrintItem(element, position1, fieldSizes, input.ToArray(), extraSmall);

                position1.Y += 60;
            }


            //linie na razem, w tym
            RectangleF linePos = new RectangleF(325, middlePartPosition + 550, 775, middlePartPosition + 550);
            element.AddLine(linePos);
            linePos.Y += 50; linePos.Height += 50;
            element.AddLine(linePos);
            linePos.Y += 50; linePos.Height += 50;
            element.AddLine(linePos);
            linePos.Y += 25; linePos.Height += 25;
            element.AddLine(linePos);

            //mała tabelka
            int offset = 0;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 120, 50);
            element.AddText("Razem:", sf, footerFont, null, linePos);
            linePos.Y += 50;
            element.AddText("w tym:", sf, footerFont, null, linePos);

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 90, 50);
            element.AddText(MoneyData.Netto.ToString("0.00"), sf, extraSmall, null, linePos);
            linePos.Y += 50;
            element.AddText(MoneyData.Netto.ToString("0.00"), sf, extraSmall, null, linePos);

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 55, 50);
            element.AddText(23.ToString(), sf, extraSmall, null, linePos);//TODO
            linePos.Y += 50;
            element.AddText(23.ToString(), sf, extraSmall, null, linePos);//TODO

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 85, 50);
            element.AddText(MoneyData.TotalVat.ToString("0.00"), sf, extraSmall, null, linePos);
            linePos.Y += 50;
            element.AddText(MoneyData.TotalVat.ToString("0.00"), sf, extraSmall, null, linePos);

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 100, 50);
            element.AddText(MoneyData.Brutto.ToString("0.00"), sf, extraSmall, null, linePos);
            linePos.Y += 50;
            element.AddText(MoneyData.Brutto.ToString("0.00"), sf, extraSmall, null, linePos);


            ////////////////////////
            //linie pionowe
            ////////////////////////
            //linie do tabelki
            RectangleF position = new RectangleF(50, middlePartPosition + 250, 50, middlePartPosition + 550);
            for (int i = 0; i < fieldSizes.Length; i++)
            {
                element.AddLine(position);
                position.X += fieldSizes[i];
                position.Width += fieldSizes[i];
                if (i == 3) position.Height += 125;
            }


            //słownie

            element.AddText("Słownie:", sf, footerFont, null, new RectangleF(50, middlePartPosition + 675, 100, 50));
            format.Alignment = StringAlignment.Near;

            int size = System.Windows.Forms.TextRenderer.MeasureText(MoneyData.InWords, extraSmall).Width;
            if (size >= 570)
            {
                Font veryVerySmall = new Font("Times New Roman", 11);
                element.AddText(MoneyData.InWords, format, veryVerySmall, null, new RectangleF(155, middlePartPosition + 675, 725, 50));
            }
            else
            {
                element.AddText(MoneyData.InWords, format, extraSmall, null, new RectangleF(155, middlePartPosition + 675, 725, 50));
            }

            int lineOffset = 150;

            element.AddLine(new RectangleF(lineOffset, middlePartPosition + 675, lineOffset, middlePartPosition + 725));


            //stopka
            int footerPartPosition = 900;
            element.AddText("Faktura jest wezwaniem do zapłaty\nPotwierdzenie odbioru faktury jest jednocześnie potwierdzeniem wykonania zlecenia.", footerFont, new PointF(50, footerPartPosition));
            element.AddText("W/G oświadczenia\npodpis osoby uprawnionej do odbioru faktury", StringAlignment.Center, footerFont, new RectangleF(0, footerPartPosition + 70, 350, footerPartPosition + 70));
            element.AddText("Podpis osoby uprawnionej\ndo wystawienia faktury", StringAlignment.Center, footerFont, new RectangleF(300, footerPartPosition + 70, 700, footerPartPosition + 70));
            int length = 300;
            int x = 25;
            element.AddLine(new RectangleF(x, footerPartPosition + 190, x + length, footerPartPosition + 190));
            x = 500;
            element.AddLine(new RectangleF(x, footerPartPosition + 190, x + length, footerPartPosition + 190));
        }
        private void PrintItem(PrintElement element, PointF pos, int[] fieldSizes, string[] fieldNames, Font font)
        {
            //wypisz nazwy
            RectangleF position = new RectangleF(pos.X, pos.Y, 0, 50);
            for (int i = 0; i < fieldSizes.Length; i++)
            {
                position.Width = fieldSizes[i];
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                element.AddText(fieldNames[i], sf, font, PrintEngine.Instane.DefaultBrush, position);
                position.X += fieldSizes[i];
            }

            //podkreśl
            element.AddLine(new RectangleF(50, pos.Y + 50, 775, pos.Y + 50));

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
        

        public static Document CreateNewDocument()
        {

            Document newDocument = new Document
            {
                Name = "bez_nazwy",
                DefaultName = true,
                Paynament = "Przelew",
                PaynamentTime = "14 dni",
                Year = DateTime.Today.Year,
                IssueDate = DateTime.Today,
                SellDate = DateTime.Today
            };


            return newDocument;
        }

    }
}
