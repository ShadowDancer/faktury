using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Faktury.Print_Framework
{
    public class PrintEngine : PrintDocument
    {
        #region Values
            /// <summary>
            /// Instance of singleton
            /// </summary>
            public static PrintEngine Instane { get; set; }
            /// <summary>
            /// Enabled - Vertical printing mode:
            /// This cause that primitives are placed in queue,
            /// and primitive adding order is valid.
            /// 
            /// Disabled - normal printing mode:
            /// every primitive have position,
            /// that indicates, where it should be drawed.
            /// </summary>
            public bool PrintingModeVertical { get; set; }
            private readonly ArrayList _printObjects = new ArrayList(); //array with all objects to print
            //used in BeginPrint, PrintPage functions
            private ArrayList _printElements; //array with elements to print(for begin print)
            private int _printIndex; //currently printed element
            private int _pageNum; //current page number


            /// <summary>
            /// Font used to draw text primitives. Default Arial, size 10pts.
            /// </summary>
            public Font DefaultFont = new Font("Times New Roman", 20);
            /// <summary>
            /// Brush used to draw primitives. Default black
            /// </summary>
            public Brush DefaultBrush = Brushes.Black;
            /// <summary>
            /// Header added to all pages
            /// Used only in Vertical mode
            /// </summary>
            public PrintElement VHeader; //header of the page
            /// <summary>
            /// Footer added to all pages
            /// Used only in Vertical mode
            /// </summary>
            public PrintElement VFooter; //footer of the page

            /// <summary>
            /// Dictionary that contains all tookens used in editor. Default:
            /// [pagenum] - number of current page
            /// [docname] - name of current document
            /// [elementnum] - number of currently printed element
            /// </summary>
            public Dictionary<string, string> Tookens = new Dictionary<string, string>();
        #endregion
        /// <summary>
        /// default constructor - initializing singleton and tookens
        /// </summary>
        public PrintEngine()
        {
            //init singleton
            Instane = this;
            //init tookens
            Tookens.Add("[pagenum]", "0");
            Tookens.Add("[docname]", DocumentName);
            Tookens.Add("[elementnum]", "0");
            PrintingModeVertical = false;
        }

        public void VCreateDefaultHeaderAndFooter()
        {            
            // create the header...
            VHeader = new PrintElement(null);
            VHeader.VAddText("[docname]");
            VHeader.HAddLine();

            // create the footer...
            VFooter = new PrintElement(null);
            VFooter.HAddLine();
            StringFormat sf = new StringFormat {Alignment = StringAlignment.Far};
            VFooter.VAddText("[pagenum]", sf);
        }

        #region Dialogs

        /// <summary>
        /// Show a print preview dialog
        /// </summary>
        public void ShowPreview()
        {
            // now, show the print dialog...
            PrintPreviewDialog dialog = new PrintPreviewDialog {Document = this};

            // show the dialog...
            try
            {
                dialog.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Drukarka niedostępna!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Show printer settings dialog
        /// </summary>
        public void ShowPageSettings()
        {
            PageSetupDialog setup = new PageSetupDialog();
            PageSettings settings = DefaultPageSettings;
            setup.PageSettings = settings;

            // display the dialog and,
            if (setup.ShowDialog() == DialogResult.OK)
                DefaultPageSettings = setup.PageSettings;
        }

        /// <summary>
        /// Show the print dialog...
        /// </summary>
        public void ShowPrintDialog()
        {
            // create and show...
            PrintDialog dialog = new PrintDialog {PrinterSettings = PrinterSettings, Document = this};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // save the changes...
                PrinterSettings = dialog.PrinterSettings;

                // do the printing...
                try
                {
                    Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Drukarka niedostępna!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion


        #region Printing
        // OnBeginPrint - called when printing starts
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            // reset...
            _printElements = new ArrayList();
            _pageNum = 0;
            _printIndex = 0;

            Tookens["[docname]"] = DocumentName;
            Tookens["[pagenum]"] = _pageNum.ToString();
            Tookens["[elementnum]"] = _printIndex.ToString();

            // go through the objects in the list and create print elements for each one...
            foreach (IPrintable printObject in _printObjects)
            {
                // create an element...
                PrintElement element = new PrintElement(printObject);
                _printElements.Add(element);

                // tell it to print...
                element.Print();
            }
        } 

        
        // OnPrintPage - called when printing needs to be done...
        protected override void OnPrintPage(PrintPageEventArgs e)
        {


            // adjust the page number...
            _pageNum++;
            Tookens["[pagenum]"] = _pageNum.ToString();

            Rectangle pageBounds = new Rectangle();

            #region Header and footer printing
            if (PrintingModeVertical)
            {
                //print header
                float headerHeight = 0;
                if (VHeader != null)
                {
                    headerHeight = VHeader.HCalculateHeight(this, e.Graphics);
                    VHeader.HDraw(this, e.MarginBounds.Top, e.Graphics, e.MarginBounds);
                }
                //print footer
                float footerHeight = 0;
                if (VFooter != null)
                {
                    footerHeight = VFooter.HCalculateHeight(this, e.Graphics);
                    VFooter.HDraw(this, e.MarginBounds.Bottom - footerHeight, e.Graphics, e.MarginBounds);
                }

                pageBounds = new Rectangle(e.MarginBounds.Left,
                (int)(e.MarginBounds.Top + headerHeight), e.MarginBounds.Width,
                (int)(e.MarginBounds.Height - footerHeight - headerHeight));

            }
            #endregion

            float yPos = pageBounds.Top;

            // loop through the elements...
            bool morePages = false;
            int elementsOnPage = 0;
            while (_printIndex < _printElements.Count)
            {
                // get the element...
                PrintElement element = (PrintElement)_printElements[_printIndex];

                if (PrintingModeVertical)
                {
                    float height = element.HCalculateHeight(this, e.Graphics);

                    // will it fit on the page?
                    if (yPos + height > pageBounds.Bottom)
                    {
                        // we don't want to do this if we're the first thing on the page...
                        if (elementsOnPage != 0)
                        {
                            morePages = true;
                            break;
                        }

                        // now draw the element...
                        element.HDraw(this, yPos, e.Graphics, pageBounds);

                        // move the ypos...
                        yPos += height;
                    }


                }
                else element.Draw(this, e.Graphics, e.PageBounds);

                // next...
                _printIndex++;
                Tookens["[elementnum]"] = _printIndex.ToString();
                elementsOnPage++;
            }
            // do we have more pages?
            e.HasMorePages = morePages;
        }

        /// <summary>
        /// Adding object to Printer
        /// </summary>
        /// <param name="printObject">Must inhreit from IPrintable</param>
        public void AddPrintObject(IPrintable printObject)
        {
            _printObjects.Add(printObject);
        }
        /// <summary>
        /// Clearing buffer
        /// </summary>
        public void ClearPrintingObjects()
        {
            _printObjects.Clear();
        }
        /// <summary>
        /// Replaces tookens, launched automatically when printing. Tookens:
        /// [pagenum] - numer of current page
        /// [docname] - name of current document
        /// [elementnum] - number of currently printed element
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        public String ReplaceTokens(String input)
        {
            // replace...
            foreach(KeyValuePair<string, string> currentKvP in Tookens)
            {
                input = input.Replace(currentKvP.Key, currentKvP.Value);
            }

            // return...
            return input;
        }

        #endregion
    }
}