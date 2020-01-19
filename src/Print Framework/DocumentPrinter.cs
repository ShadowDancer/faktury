using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Faktury.Classes;

namespace Faktury.Print_Framework
{
    public class DocumentPrinter : IPrintable
    {
        /// <summary>
        /// Summary height
        /// </summary>
        private const int Sh = 30;
        private readonly Document _document;

        public DocumentPrinter(Document document)
        {
            _document = document;
        }

        public void Print(PrintElement element)
        {
            Company company = _document.Customer;
            Company ownerCompany = _document.Issuer;
            if (company == null || ownerCompany == null)
            {
                MessageBox.Show("Nie znaleziono firmy! Błąd jest prawdopodobnie spowodowany usunięciem kontrahenta", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Font smallFont = new Font("Times New Roman", 14);
            Font extraSmall = new Font("Times New Roman", 11);
            Font extraExtraSmall = new Font("Times New Roman", 9);

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

            element.AddText("NIP: " + ownerCompany.Nip, smallFont, headerTextPosition);
            if (ownerCompany.Bank)
            {
                headerTextPosition.Y += 20;
                element.AddText("Bank: " + ownerCompany.BankSection, smallFont, headerTextPosition);
                headerTextPosition.Y += 20;
                element.AddText(ownerCompany.BankAccount, smallFont, headerTextPosition);
            }

            headerTextPosition.Y += 17;

            if (!string.IsNullOrEmpty(ownerCompany.PhoneNumber))
            {
                headerTextPosition.Y += 17;                
                element.AddText("Telefon: " + ownerCompany.PhoneNumber, smallFont, headerTextPosition);

            }
            if (!string.IsNullOrEmpty(ownerCompany.MobileNumber))
            {
                headerTextPosition.Y += 17;
                element.AddText("Komórka: " + ownerCompany.MobileNumber, smallFont, headerTextPosition);
            }

            headerTextPosition.Y += 21;
            int middlePartPosition = 150;


            // line
            element.AddHorizontalLine(middlePartPosition);


            // number
            element.AddText("Faktura VAT nr: " + _document.Number + "/" + _document.Year, new PointF(50, middlePartPosition + 10));
            if (_document.ReverseVAT)
            {
                element.AddText("Odwrotne obciążenie", new PointF(50, middlePartPosition + 40));
            }
            // dates
            element.AddText($"Data wystawienia: {_document.IssueDate.Day:00}.{_document.IssueDate.Month:00}.{_document.IssueDate.Year}", new PointF(420, middlePartPosition + 30));
            element.AddText(
                $"Data sprzedaży: {NumberToWordConventer.ConvertNumberToMonth(_document.SellDate.Month.ToString())} {_document.SellDate.Year.ToString()}", new PointF(420, middlePartPosition + 60));
            
            // payment
            string[] paymentTexts = { "Forma płatności: " + _document.PaymentType, "Termin płatności: " + _document.PaymentTime };
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

            element.AddText(company.Name, smallFont, new PointF(65, middlePartPosition + 120));
            element.AddText(company.Owner, smallFont, new PointF(65, middlePartPosition + 140));
            element.AddText(company.Address, smallFont, new PointF(65, middlePartPosition + 160));
            element.AddText(company.Street, smallFont, new PointF(65, middlePartPosition + 180));
            element.AddText("NIP:" + company.Nip, smallFont, new PointF(65, middlePartPosition + 210));

            // border
            element.AddRectangle(new RectangleF(50, middlePartPosition + 250, 725, 475));

            //empty space
            element.AddRectangle(new RectangleF(50, middlePartPosition + 550, 275, 125));

            /////////////////////////
            //horizontal lines
            ////////////////////////

            //table header


            //header separator
            int[] fieldSizes = { 30, 165, 35, 45, 120, 90, 55, 85, 100 };



            string[] fieldNames = { "#", "Nazwa", "jm", "Ilość", "Cena jed.bez\npod. VAT", "Wartość bez VAT", "VAT\n%", "Kwota Vat", "Wartość z pod. VAT" };

            // table constants
            var summaryTableStart = fieldSizes.Take(5).Sum();

            bool PKWiUColumn = _document.Items.Any(n => !string.IsNullOrWhiteSpace(n.PKWiU));
            if (PKWiUColumn)
            {
                fieldSizes = new int[]{ 30, 100, 65, 35, 45, 120, 90, 55, 85, 100 };
                fieldNames = new []{ "#", "Nazwa", "Symbol PKWiU", "jm", "Ilość", "Cena jedn.\nnetto (zł)", "Wartość\nnetto (zł)", "VAT\n%", "Kwota VAT", "Wartość\nbrutto (zł)" };

            }

            //header texts
            PrintItem(element, new PointF(50, middlePartPosition + 250), fieldSizes, fieldNames, extraSmall);
            
            //items
            PointF position1 = new PointF(50, middlePartPosition + 300);
            var documentItems = _document.Items;
            for (int i = 0; i < documentItems.Count; i++)
            {
                List<string> input = new List<string>();
                int lp = i + 1;
                input.Add(lp.ToString());
                input.Add(documentItems[i].Name);
                if (PKWiUColumn)
                {
                    input.Add(documentItems[i].PKWiU ?? "");
                }
                input.Add(documentItems[i].Unit);
                input.Add(documentItems[i].Quantity.ToString(CultureInfo.CurrentCulture));
                input.Add(documentItems[i].PriceNet.ToString("0.00"));
                input.Add(documentItems[i].SumNet.ToString("0.00"));

                if (_document.ReverseVAT)
                {
                    input.Add("-");
                }
                else
                {
                    input.Add(documentItems[i].VatRate.ToString());
                }
                input.Add(documentItems[i].SumVat.ToString("0.00"));
                input.Add(documentItems[i].SumGross.ToString("0.00"));


                var fonts = Enumerable.Repeat(extraSmall, input.Count).ToArray();
                if (PKWiUColumn)
                {
                    fonts[2] = extraExtraSmall;
                }

                PrintItem(element, position1, fieldSizes, input.ToArray(), fonts);

                position1.Y += 60;
            }

            //summary lines
            
            DrawSummaryTable(element, middlePartPosition, sf, smallFont, extraSmall);


            RectangleF linePos = new RectangleF(325, middlePartPosition + 550, 775, middlePartPosition + 550);
            element.AddLine(linePos);
            linePos.Y += Sh; linePos.Height += Sh;
            element.AddLine(linePos);

            if(!_document.ReverseVAT)
            {

            List<(string symbol, decimal net, decimal vat, decimal gross)> vatSummaries = _document.Items.GroupBy(n => n.VatRate.Symbol)
                .Select(n => (n.Key, n.Sum(m => m.SumNet), n.Sum(m => m.SumVat), n.Sum(m => m.SumGross))).ToList();

            int index = 1;
            foreach (var (symbol, net, vat, gross) in vatSummaries)
            {
                string s = symbol;
                if (_document.ReverseVAT)
                {
                    s = "-";
                }
                DrawVatSummary(element, middlePartPosition, sf, extraSmall, index, net, vat, gross, s);

                linePos.Y += Sh; linePos.Height += Sh;
                element.AddLine(linePos);
                index++;
            }
            }

            linePos.Y = linePos.Height = middlePartPosition + 550 + 100 + 25;
            element.AddLine(linePos);


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

            element.AddText("Słownie:", sf, smallFont, null, new RectangleF(50, middlePartPosition + 675, 100, 50));
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
            string footerText =
                "Faktura jest wezwaniem do zapłaty\nPotwierdzenie odbioru faktury jest jednocześnie potwierdzeniem wykonania zlecenia.";

            if (_document.ReverseVAT)
            {
                footerText += "\r\nUwagi: Zgodnie z art. 17 ust. 1 pkt. 8 Ustawy o VAT podatek VAT rozlicza nabywca.";

            }
            element.AddText(footerText, extraSmall, new PointF(50, footerPartPosition));



            element.AddText("W/G oświadczenia\npodpis osoby uprawnionej do odbioru faktury", StringAlignment.Center, smallFont, new RectangleF(0, footerPartPosition + 70, 350, footerPartPosition + 70));
            element.AddText("Podpis osoby uprawnionej\ndo wystawienia faktury", StringAlignment.Center, smallFont, new RectangleF(300, footerPartPosition + 70, 700, footerPartPosition + 70));
            int length = 300;
            int x = 25;
            element.AddLine(new RectangleF(x, footerPartPosition + 190, x + length, footerPartPosition + 190));
            x = 500;
            element.AddLine(new RectangleF(x, footerPartPosition + 190, x + length, footerPartPosition + 190));
        }

        private void DrawSummaryTable(PrintElement element, int middlePartPosition, StringFormat sf, Font footerFont,
            Font extraSmall)
        {
            int offset = 0;
            var linePos = new RectangleF(325 + offset, middlePartPosition + 550, 120, Sh);
            element.AddText("Razem:", sf, footerFont, null, linePos);
            linePos.Y += Sh;
            if(!_document.ReverseVAT)
            {
                element.AddText("w tym:", sf, footerFont, null, linePos);
            }

            

            offset += (int) linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 90, Sh);
            element.AddText(_document.DocumentSummary.TotalNet.ToString("0.00"), sf, extraSmall, null, linePos);
            
            offset += (int) linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 55, Sh);
            //element.AddText("X", sf, extraSmall, null, linePos); //TODO

            offset += (int) linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 85, Sh);
            element.AddText(_document.DocumentSummary.TotalVat.ToString("0.00"), sf, extraSmall, null, linePos);
            
            offset += (int) linePos.Width;
            linePos = new RectangleF(325 + offset, middlePartPosition + 550, 100, Sh);
            element.AddText(_document.DocumentSummary.TotalGross.ToString("0.00"), sf, extraSmall, null, linePos);
        }


        private void DrawVatSummary(PrintElement element, int middlePartPosition, StringFormat sf, Font extraSmall, int index, decimal sumNet, decimal sumTax, decimal sumGross, string taxSymbol)
        {
            int offsetX = 120;
            int offsetY = middlePartPosition + 550 + index * Sh;

            var linePos = new RectangleF(325 + offsetX, offsetY, 90, Sh);
            element.AddText(sumNet.ToString("0.00"), sf, extraSmall, null, linePos);

            offsetX += (int)linePos.Width;
            linePos = new RectangleF(325 + offsetX, offsetY, 55, Sh);
            element.AddText(taxSymbol, sf, extraSmall, null, linePos);

            offsetX += (int)linePos.Width;
            linePos = new RectangleF(325 + offsetX, offsetY, 85, Sh);
            element.AddText(sumTax.ToString("0.00"), sf, extraSmall, null, linePos);

            offsetX += (int)linePos.Width;
            linePos = new RectangleF(325 + offsetX, offsetY, 100, Sh);
            element.AddText(sumGross.ToString("0.00"), sf, extraSmall, null, linePos);
        }

        private void PrintItem(PrintElement element, PointF pos, int[] fieldSizes, string[] fieldNames, Font font)
        {
            PrintItem(element, pos, fieldSizes, fieldNames, Enumerable.Repeat(font, fieldNames.Length).ToArray());
        }

        private void PrintItem(PrintElement element, PointF pos, int[] fieldSizes, string[] fieldNames, Font[] fonts)
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
                element.AddText(fieldNames[i], sf, fonts[i], PrintEngine.Instane.DefaultBrush, position);
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