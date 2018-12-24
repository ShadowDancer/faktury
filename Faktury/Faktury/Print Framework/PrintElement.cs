using System;
using System.Collections;
using System.Drawing;
using Faktury.Print_Framework.Normal;
using Faktury.Print_Framework.Normal.Primitives;
using Faktury.Print_Framework.Vertical;
using Faktury.Print_Framework.Vertical.Primitives;

namespace Faktury.Print_Framework
{
    public enum LineAlignment
    {
        Left,
        Center,
        Right
    }

    /// <summary>
    /// Class that contains primitives. This can represent for example one object, that you want print.
    /// All primitives in element will bo on one page.
    /// </summary>
    public class PrintElement
    {
        // members...
        /// <summary>
        /// List of primitives
        /// </summary>
        private readonly ArrayList _printPrimitives = new ArrayList();
        private readonly ArrayList _printPrimitivesVertical = new ArrayList();

        private readonly IPrintable _printObject;

        /// <summary>
        /// Creating PrintElement from object
        /// </summary>
        /// <param name="printObject">Mus ihreit from IPrintable</param>
        public PrintElement(IPrintable printObject)
        {
            _printObject = printObject;
        }

        #region AddPrimitives
        /// <summary>
        /// Adds primitive to the list(primitive must inhreit from IPrintPrimitiveVertical)
        /// Only for Vertical mode
        /// </summary>
        /// <param name="primitive">primitive object</param>
        private void VAddPrimitive(IPrintPrimitiveVertical primitive)
        {
            _printPrimitivesVertical.Add(primitive);
        }
        /// <summary>
        /// Adds primitive to the list(primitive must inhreit from IPrintPrimitive)
        /// </summary>
        /// <param name="primitive">primitive object</param>
        private void AddPrimitive(IPrintPrimitive primitive)
        {
            _printPrimitives.Add(primitive);
        }

        #region AddText
        //version with point input
        public void AddText(string text, PointF position)
        {
            RectangleF pos = new RectangleF(position.X, position.Y, 0, 0);
            AddPrimitive(new PrintText(text, new StringFormat(), PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, pos));
        }
        public void AddText(string text, StringFormat stringFormat, PointF position)
        {
            RectangleF pos = new RectangleF(position.X, position.Y, 0, 0);
            AddPrimitive(new PrintText(text, stringFormat, PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, pos));
        }
        public void AddText(string text, Font font, PointF position)
        {
            RectangleF pos = new RectangleF(position.X, position.Y, 0, 0);
            AddPrimitive(new PrintText(text, new StringFormat(), font, PrintEngine.Instane.DefaultBrush, pos));
        }
        public void AddText(string text, Font font, Brush brush, PointF position)
        {
            RectangleF pos = new RectangleF(position.X, position.Y, 0, 0);
            AddPrimitive(new PrintText(text, new StringFormat(), font, brush, pos));
        }

        public void AddText(string text, StringFormat stringFromat, Font font, Brush brush, PointF position)
        {
            RectangleF pos = new RectangleF(position.X, position.Y, 0, 0);
            AddPrimitive(new PrintText(text, stringFromat, font, brush, pos));
        }


        //version with rectnagle input
        public void AddText(string text, RectangleF position)
        {
            AddPrimitive(new PrintText(text, new StringFormat(), PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, position));
        }
        public void AddText(string text, StringAlignment alignment, RectangleF position)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = alignment;
            AddPrimitive(new PrintText(text, sf, PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, position));
        }
        public void AddText(string text, StringFormat stringFormat, RectangleF position)
        {
            AddPrimitive(new PrintText(text, stringFormat, PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, position));
        }
        public void AddText(string text, Font font, RectangleF position)
        {
            AddPrimitive(new PrintText(text, new StringFormat(), font, PrintEngine.Instane.DefaultBrush, position));
        }
        public void AddText(string text, StringAlignment alignment, Font font, RectangleF position)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = alignment;
            AddPrimitive(new PrintText(text, sf, font, PrintEngine.Instane.DefaultBrush, position));
        }
        public void AddText(string text, StringFormat stringFormat, Font font, Brush brush, RectangleF position)
        {
            AddPrimitive(new PrintText(text, stringFormat, font, brush, position));
        }
        #endregion
        #region AddLine
        public void AddLine(RectangleF position)
        {
            AddPrimitive(new PrintLine(PrintEngine.Instane.DefaultBrush, 1, position));
        }
        public void AddLine(float width, RectangleF position)
        {
            AddPrimitive(new PrintLine(PrintEngine.Instane.DefaultBrush, width, position));
        }
        public void AddLine(Brush brush, RectangleF position)
        {
            AddPrimitive(new PrintLine(brush, 1, position));
        }
        public void AddLine(Brush brush, float width, RectangleF position)
        {
            AddPrimitive(new PrintLine(brush, width, position));
        }
        #endregion
        #region AddHorizontalLine

        public void AddHorizontalLine(float height)
        {
            AddPrimitive(new PrintHorizontalLine(PrintEngine.Instane.DefaultBrush, 1, height));
        }

        public void AddHorizontalLine(float width, float height)
        {
            AddPrimitive(new PrintHorizontalLine(PrintEngine.Instane.DefaultBrush, width, height));
        }
        public void AddHorizontalLine(Brush brush, float height)
        {
            AddPrimitive(new PrintHorizontalLine(brush, 1, height));
        }
        public void AddHorizontalLine(Brush brush, float width, float height)
        {
            AddPrimitive(new PrintHorizontalLine(brush, width, height));
        }

        #endregion
        #region AddRectangle

        /// <summary>
        /// Drawing nonfilled rectangle.
        /// </summary>
        /// <param name="position"></param>
        public void AddRectangle(RectangleF position)
        {
            AddPrimitive(new PrintRectangle(PrintEngine.Instane.DefaultBrush, 1, false, position));
        }
        /// <summary>
        /// Drawing non filled rectangle
        /// </summary>
        /// <param name="width">Width of the line</param>
        /// <param name="position"></param>
        public void AddRectangle(int width, RectangleF position)
        {
            AddPrimitive(new PrintRectangle(PrintEngine.Instane.DefaultBrush, width, false, position));
        }
        /// <summary>
        /// Drawing rectangle on target position
        /// </summary>
        /// <param name="width">Width of the line if filled is false</param>
        /// <param name="filled"></param>
        /// <param name="position"></param>
        public void AddRectangle(int width, bool filled, RectangleF position)
        {
            AddPrimitive(new PrintRectangle(PrintEngine.Instane.DefaultBrush, width, filled, position));
        }
        /// <summary>
        /// Draw rectangle in target area
        /// </summary>
        /// <param name="brush">Brush used to draw</param>
        /// <param name="width">Width of the line(if filling is off)</param>
        /// <param name="filled"></param>
        /// <param name="position"></param>
        public void AddRectangle(Brush brush, int width, bool filled, RectangleF position)
        {
            AddPrimitive(new PrintRectangle(brush, width, filled, position));
        }


        #endregion


        #region VAddText
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="buf">Input string</param>
            public void VAddText(String text)
            {
                VAddText(text, new StringFormat());
            }
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="text">Input string</param>
            /// <param name="stringFromat">String formatter (aligment, clipping)</param>
            public void VAddText(String text, StringFormat stringFromat)
            {
                VAddText(text, stringFromat, PrintEngine.Instane.DefaultFont);
            }
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="text">Input string</param>
            /// <param name="stringFromat">String formatter (aligment, clipping)</param>
            /// <param name="font">Font used to draw text</param>
            public void VAddText(String text, StringFormat stringFromat, Font font)
            {
                VAddText(text, stringFromat, font, PrintEngine.Instane.DefaultBrush);
            }
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="text">Input string</param>
            /// <param name="stringFromat">String formatter (aligment, clipping)</param>
            /// <param name="font">Font used to draw text</param>
            /// <param name="brush">Brush used to draw text</param>
            public void VAddText(String text, StringFormat stringFromat, Font font, Brush brush)
            {
                VAddPrimitive(new PrintPrimitiveText(text, stringFromat, font, brush));
            }
        #endregion
        #region HAddImage
            /// <summary>
            /// Adding image to element
            /// </summary>
            /// <param name="filePath">Path to bitmap file on HDD</param>
            public void HAddImage(String filePath)
            {
                VAddPrimitive(new PrintPrimitiveImage(filePath));
            }
            /// <summary>
            /// Adding image to element
            /// </summary>
            /// <param name="image">Image object that will be printed</param>
            public void HAddImage(Image image)
            {
                VAddPrimitive(new PrintPrimitiveImage(image));
            }
        #endregion
        #region HAddLine
            /// <summary>
            /// Adding line to the element
            /// </summary>
            public void HAddLine()
            {
                HAddLine(PrintEngine.Instane.DefaultBrush);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="width">Width of the line</param>
            public void HAddLine(float width)
            {
                HAddLine(PrintEngine.Instane.DefaultBrush, width, -1, LineAlignment.Left);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="Width">Width of the line</param>
            public void HAddLine(Brush brush)
            {
                HAddLine(brush, 1);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="brush">Brush used to draw the line</param>
            public void HAddLine(Brush brush, float width)
            {
                HAddLine(brush, -1, width, LineAlignment.Left);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="brush">Brush used to draw line</param>
            /// <param name="width">Width of the line</param>
            /// <param name="length">Lenght of the line</param>
            /// <param name="alignment">Alignment of the line</param>
            public void HAddLine(Brush brush, float length, float width, LineAlignment alignment)
            {
                VAddPrimitive(new PrintPrimitiveLine(brush, length, width, alignment));
            }
        #endregion
        #region HAddBlankLine
            /// <summary>
            /// Adding blank line to element
            /// </summary>
            public void HAddBlankLine()
            {
                VAddPrimitive(new PrintPrimitiveBlankLine());
            }
            /// <summary>
            /// Adding blank line to element
            /// </summary>
            /// <param name="height">Height of the line</param>
            public void HAddBlankLine(float height)
            {
                VAddPrimitive(new PrintPrimitiveBlankLine(height));
            }
        #endregion
        #region HAddParagraph
            /// <summary>
        /// Adding blank line, text and line
        /// Only for Vertical mode
        /// </summary>
        /// <param name="buf">Input string</param>
            public void HAddParagraph(String name)
            {
                HAddBlankLine();
                VAddText(name);
                HAddLine();
            }
            #endregion

        #endregion


        /// <summary>
        /// Getting height of the element
        /// Only for Vertical mode
        /// </summary>
        /// <returns></returns>
        public float HCalculateHeight(PrintEngine engine, Graphics graphics)
        {
            // loop through the print height...
            float height = 0;
            foreach (IPrintPrimitiveVertical primitive in _printPrimitivesVertical)
            {
                // get the height...
                height += primitive.CalculateHeight(engine, graphics);
            }

            // return the height...
            return height;
        }

        /// <summary>
        /// Drawing primitives on the graphics object
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="graphics"></param>
        public void Draw(PrintEngine engine, Graphics graphics, Rectangle pageBounds)
        {
            foreach (IPrintPrimitive primitive in _printPrimitives)
            {
                // render it...
                primitive.Draw(engine, graphics, pageBounds);
            }
        }
        /// <summary>
        /// Drawing primitives on the graphics object
        /// Used only by printing engine in Vertical mode
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="yPos">pos y to render first element</param>
        /// <param name="graphics"></param>
        /// <param name="pageBounds"></param>
        public void HDraw(PrintEngine engine, float yPos, Graphics graphics, Rectangle pageBounds)
        {
            // where...
            float height = HCalculateHeight(engine, graphics);
            RectangleF elementBounds = new RectangleF(pageBounds.Left, (int)yPos, pageBounds.Right - pageBounds.Left, (int)height);

            // now, tell the primitives to print themselves...
            foreach (IPrintPrimitiveVertical primitive in _printPrimitivesVertical)
            {
                // render it...
                primitive.Draw(engine, graphics, elementBounds);

                // move to the next line...
                yPos += primitive.CalculateHeight(engine, graphics);
                elementBounds.Y = yPos;
            }
        }



        /// <summary>
        /// Printing object - used only by print engine
        /// </summary>
        public void Print()
        {
            _printObject.Print(this);
        }

    }
}
