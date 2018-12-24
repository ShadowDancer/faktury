using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Faktury.Print_Framework;

namespace Faktury.Classes.Printing
{
    public class DocumentPrinter : IPrintable
    {
        private readonly ModelStore _modelStore;
        private readonly SettingsAccessor _settingsAccessor;
        private readonly Document _document;

        public DocumentPrinter(ModelStore modelStore, SettingsAccessor settingsAccessor, Document document)
        {
            _modelStore = modelStore;
            _settingsAccessor = settingsAccessor;
            _document = document;
        }

        public void Print(PrintElement element)
        {
            Company company = _modelStore.Companies.Find(n => n.Id == _document.CompanyId);
            Company ownerCompany = _settingsAccessor.GetSettings().OwnerCompany;
            if (company == null || ownerCompany == null)
            {
                MessageBox.Show("Nie znaleziono firmy! Błąd jest prawdopodobnie spowodowany usunięciem kontrahenta", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Font footerFont = new Font("Times New Roman", 15);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };

            // header
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
            if (ownerCompany.Bank)
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


            // line
            element.AddHorizontalLine(middlePartPosition);


            // number
            element.AddText("Faktura VAT nr: " + _document.Number + "/" + _document.Year, new PointF(50, middlePartPosition + 10));

            // dates
            element.AddText($"Data wystawienia: {_document.IssueDate.Day:00}.{_document.IssueDate.Month:00}.{_document.IssueDate.Year}", new PointF(420, middlePartPosition + 30));
            element.AddText(
                $"Data sprzedaży: {NumberToWordConventer.ConvertNumberToMonth(_document.SellDate.Month.ToString())} {_document.SellDate.Year.ToString()}", new PointF(420, middlePartPosition + 60));

            // payment
            string[] paymentTexts = { "Forma płatności: " + _document.Payment, "Termin płatności: " + _document.PaymentTime };
            float[] paymentLengths = { TextRenderer.MeasureText(paymentTexts[0], PrintEngine.Instane.DefaultFont).Width, Graphics.FromHwnd(new IntPtr()).MeasureString(paymentTexts[1], PrintEngine.Instane.DefaultFont).Width };
            PointF[] paymentPositions = { new PointF(475, middlePartPosition + 185), new PointF(475, middlePartPosition + 215) };

            float xPosition = 475;
            if (paymentLengths[0] > paymentLengths[1])
            {
                if (paymentLengths[0] > 300)
                {
                    xPosition -= paymentLengths[0] - 300;
                }
            }
            else
            {
                if (paymentLengths[1] > 300)
                {
                    xPosition -= paymentLengths[1] - 300;
                }
            }
            paymentPositions[0].X = paymentPositions[1].X = xPosition;

            element.AddText(paymentTexts[0], paymentPositions[0]);
            element.AddText(paymentTexts[1], paymentPositions[1]);

            // client
            element.AddText("Nabywca:", new PointF(50, middlePartPosition + 80));

            element.AddText(company.Name, footerFont, new PointF(65, middlePartPosition + 120));
            element.AddText(company.Owner, footerFont, new PointF(65, middlePartPosition + 140));
            element.AddText(company.Address, footerFont, new PointF(65, middlePartPosition + 160));
            element.AddText(company.Street, footerFont, new PointF(65, middlePartPosition + 180));
            element.AddText("NIP:" + company.Nip, footerFont, new PointF(65, middlePartPosition + 210));

            // border
            element.AddRectangle(new RectangleF(50, middlePartPosition + 250, 725, 475));

            //empty space
            element.AddRectangle(new RectangleF(50, middlePartPosition + 550, 275, 125));

            /////////////////////////
            //horizontal lines
            ////////////////////////

            //table header

            //header separator
            int[] fieldSizes = { 35, 150, 40, 50, 120, 90, 55, 85, 100 };
            string[] fieldNames = { "LP", "Nazwa", "jm", "Ilość", "Cena jed.bez\npod. VAT", "Wartość bez VAT", "VAT\n%", "Kwota Vat", "Wartość z pod. VAT" };

            Font extraSmall = new Font("Times New Roman", 12);

            //header texts
            PrintItem(element, new PointF(50, middlePartPosition + 250), fieldSizes, fieldNames, footerFont);
            
            //items
            PointF position1 = new PointF(50, middlePartPosition + 300);
            var documentItems = _document.Items;
            for (int i = 0; i < documentItems.Count; i++)
            {
                List<string> input = new List<string>();
                int lp = i + 1;
                input.Add(lp.ToString());
                input.Add(documentItems[i].Name);
                input.Add(documentItems[i].Unit);
                input.Add(documentItems[i].Count.ToString(CultureInfo.CurrentCulture));
                input.Add(documentItems[i].Cost.ToString("0.00"));
                input.Add(documentItems[i].Netto.ToString("0.00"));
                input.Add(documentItems[i].VatPercent.ToString(CultureInfo.CurrentCulture));
                input.Add(documentItems[i].Vat.ToString("0.00"));
                input.Add(documentItems[i].Brutto.ToString("0.00"));

                PrintItem(element, position1, fieldSizes, input.ToArray(), extraSmall);

                position1.Y += 60;
            }


            //summary lines
            RectangleF linePos = new RectangleF(325, middlePartPosition + 550, 775, middlePartPosition + 550);
            element.AddLine(linePos);
            linePos.Y += 50; linePos.Height += 50;
            element.AddLine(linePos);
            linePos.Y += 50; linePos.Height += 50;
            element.AddLine(linePos);
            linePos.Y += 25; linePos.Height += 25;
            element.AddLine(linePos);

            // summary table
            int offset = 0;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 120, 50);
            element.AddText("Razem:", sf, footerFont, null, linePos);
            linePos.Y += 50;
            element.AddText("w tym:", sf, footerFont, null, linePos);

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 90, 50);
            element.AddText(_document.DocumentSummary.Netto.ToString("0.00"), sf, extraSmall, null, linePos);
            linePos.Y += 50;
            element.AddText(_document.DocumentSummary.Netto.ToString("0.00"), sf, extraSmall, null, linePos);

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 55, 50);
            element.AddText(23.ToString(), sf, extraSmall, null, linePos);//TODO
            linePos.Y += 50;
            element.AddText(23.ToString(), sf, extraSmall, null, linePos);//TODO

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 85, 50);
            element.AddText(_document.DocumentSummary.TotalVat.ToString("0.00"), sf, extraSmall, null, linePos);
            linePos.Y += 50;
            element.AddText(_document.DocumentSummary.TotalVat.ToString("0.00"), sf, extraSmall, null, linePos);

            offset += (int)linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 100, 50);
            element.AddText(_document.DocumentSummary.Brutto.ToString("0.00"), sf, extraSmall, null, linePos);
            linePos.Y += 50;
            element.AddText(_document.DocumentSummary.Brutto.ToString("0.00"), sf, extraSmall, null, linePos);


            ////////////////////////
            //vertical lines
            ////////////////////////
            //table lines
            RectangleF position = new RectangleF(50, middlePartPosition + 250, 50, middlePartPosition + 550);
            for (int i = 0; i < fieldSizes.Length; i++)
            {
                element.AddLine(position);
                position.X += fieldSizes[i];
                position.Width += fieldSizes[i];
                if (i == 3) position.Height += 125;
            }


            // value in words

            element.AddText("Słownie:", sf, footerFont, null, new RectangleF(50, middlePartPosition + 675, 100, 50));
            format.Alignment = StringAlignment.Near;

            int size = TextRenderer.MeasureText(_document.DocumentSummary.InWords, extraSmall).Width;
            if (size >= 570)
            {
                Font veryVerySmall = new Font("Times New Roman", 11);
                element.AddText(_document.DocumentSummary.InWords, format, veryVerySmall, null, new RectangleF(155, middlePartPosition + 675, 725, 50));
            }
            else
            {
                element.AddText(_document.DocumentSummary.InWords, format, extraSmall, null, new RectangleF(155, middlePartPosition + 675, 725, 50));
            }

            int lineOffset = 150;

            element.AddLine(new RectangleF(lineOffset, middlePartPosition + 675, lineOffset, middlePartPosition + 725));


            // footer
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
            // print names
            RectangleF position = new RectangleF(pos.X, pos.Y, 0, 50);
            for (int i = 0; i < fieldSizes.Length; i++)
            {
                position.Width = fieldSizes[i];
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
                };
                element.AddText(fieldNames[i], sf, font, PrintEngine.Instane.DefaultBrush, position);
                position.X += fieldSizes[i];
            }

            // underscore
            element.AddLine(new RectangleF(50, pos.Y + 50, 775, pos.Y + 50));
        }

        public void Print(PrintEngine printEngine)
        {
            printEngine.AddPrintObject(this);
            printEngine.Print();
            printEngine.ClearPrintingObjects();
        }

        public void ShowPreview(PrintEngine printEngine)
        {
            printEngine.AddPrintObject(this);
            printEngine.ShowPreview();
            printEngine.ClearPrintingObjects();
        }
    }
}