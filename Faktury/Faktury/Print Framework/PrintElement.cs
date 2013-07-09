using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing; 

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
        private ArrayList _printPrimitives = new ArrayList();
        private ArrayList _printPrimitivesVertical = new ArrayList();

        private IPrintable _printObject;

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
        private void VAddPrimitive(Vertical.IPrintPrimitiveVertical primitive)
        {
            _printPrimitivesVertical.Add(primitive);
        }
        /// <summary>
        /// Adds primitive to the list(primitive must inhreit from IPrintPrimitive)
        /// </summary>
        /// <param name="primitive">primitive object</param>
        private void AddPrimitive(Normal.IPrintPrimitive primitive)
        {
            _printPrimitives.Add(primitive);
        }

        #region AddText
        //version with point input
        public void AddText(string Text, PointF Position)
        {
            RectangleF Pos = new RectangleF(Position.X, Position.Y, 0, 0);
            AddPrimitive(new Normal.Primitives.PrintText(Text, new StringFormat(), PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, Pos));
        }
        public void AddText(string Text, StringFormat StringFormat, PointF Position)
        {
            RectangleF Pos = new RectangleF(Position.X, Position.Y, 0, 0);
            AddPrimitive(new Normal.Primitives.PrintText(Text, StringFormat, PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, Pos));
        }
        public void AddText(string Text, Font Font, PointF Position)
        {
            RectangleF Pos = new RectangleF(Position.X, Position.Y, 0, 0);
            AddPrimitive(new Normal.Primitives.PrintText(Text, new StringFormat(), Font, PrintEngine.Instane.DefaultBrush, Pos));
        }
        public void AddText(string Text, Font Font, Brush Brush, PointF Position)
        {
            RectangleF Pos = new RectangleF(Position.X, Position.Y, 0, 0);
            AddPrimitive(new Normal.Primitives.PrintText(Text, new StringFormat(), Font, Brush, Pos));
        }

        public void AddText(string Text, StringFormat StringFromat, Font Font, Brush Brush, PointF Position)
        {
            RectangleF Pos = new RectangleF(Position.X, Position.Y, 0, 0);
            AddPrimitive(new Normal.Primitives.PrintText(Text, StringFromat, Font, Brush, Pos));
        }


        //version with rectnagle input
        public void AddText(string Text, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintText(Text, new StringFormat(), PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, Position));
        }
        public void AddText(string Text, StringAlignment Alignment, RectangleF Position)
        {
            StringFormat SF = new StringFormat();
            SF.Alignment = Alignment;
            AddPrimitive(new Normal.Primitives.PrintText(Text, SF, PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, Position));
        }
        public void AddText(string Text, StringFormat StringFormat, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintText(Text, StringFormat, PrintEngine.Instane.DefaultFont, PrintEngine.Instane.DefaultBrush, Position));
        }
        public void AddText(string Text, Font Font, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintText(Text, new StringFormat(), Font, PrintEngine.Instane.DefaultBrush, Position));
        }
        public void AddText(string Text, StringAlignment Alignment, Font Font, RectangleF Position)
        {
            StringFormat SF = new StringFormat();
            SF.Alignment = Alignment;
            AddPrimitive(new Normal.Primitives.PrintText(Text, SF, Font, PrintEngine.Instane.DefaultBrush, Position));
        }
        public void AddText(string Text, StringFormat StringFormat, Font Font, Brush Brush, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintText(Text, StringFormat, Font, Brush, Position));
        }
        #endregion
        #region AddLine
        public void AddLine(RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintLine(PrintEngine.Instane.DefaultBrush, 1, Position));
        }
        public void AddLine(float Width, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintLine(PrintEngine.Instane.DefaultBrush, Width, Position));
        }
        public void AddLine(Brush Brush, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintLine(Brush, 1, Position));
        }
        public void AddLine(Brush Brush, float Width, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintLine(Brush, Width, Position));
        }
        #endregion
        #region AddHorizontalLine

        public void AddHorizontalLine(float Height)
        {
            AddPrimitive(new Normal.Primitives.PrintHorizontalLine(PrintEngine.Instane.DefaultBrush, 1, Height));
        }

        public void AddHorizontalLine(float Width, float Height)
        {
            AddPrimitive(new Normal.Primitives.PrintHorizontalLine(PrintEngine.Instane.DefaultBrush, Width, Height));
        }
        public void AddHorizontalLine(Brush Brush, float Height)
        {
            AddPrimitive(new Normal.Primitives.PrintHorizontalLine(Brush, 1, Height));
        }
        public void AddHorizontalLine(Brush Brush, float Width, float Height)
        {
            AddPrimitive(new Normal.Primitives.PrintHorizontalLine(Brush, Width, Height));
        }

        #endregion
        #region AddRectangle

        /// <summary>
        /// Drawing nonfilled rectangle.
        /// </summary>
        /// <param name="Position"></param>
        public void AddRectangle(RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintRectangle(PrintEngine.Instane.DefaultBrush, 1, false, Position));
        }
        /// <summary>
        /// Drawing non filled rectangle
        /// </summary>
        /// <param name="Width">Width of the line</param>
        /// <param name="Position"></param>
        public void AddRectangle(int Width, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintRectangle(PrintEngine.Instane.DefaultBrush, Width, false, Position));
        }
        /// <summary>
        /// Drawing rectangle on target position
        /// </summary>
        /// <param name="Width">Width of the line if filled is false</param>
        /// <param name="Filled"></param>
        /// <param name="Position"></param>
        public void AddRectangle(int Width, bool Filled, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintRectangle(PrintEngine.Instane.DefaultBrush, Width, Filled, Position));
        }
        /// <summary>
        /// Draw rectangle in target area
        /// </summary>
        /// <param name="Brush">Brush used to draw</param>
        /// <param name="Width">Width of the line(if filling is off)</param>
        /// <param name="Filled"></param>
        /// <param name="Position"></param>
        public void AddRectangle(Brush Brush, int Width, bool Filled, RectangleF Position)
        {
            AddPrimitive(new Normal.Primitives.PrintRectangle(Brush, Width, Filled, Position));
        }


        #endregion


        #region VAddText
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="buf">Input string</param>
            public void VAddText(String Text)
            {
                VAddText(Text, new StringFormat());
            }
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="Text">Input string</param>
            /// <param name="StringFromat">String formatter (aligment, clipping)</param>
            public void VAddText(String Text, StringFormat StringFromat)
            {
                VAddText(Text, StringFromat, PrintEngine.Instane.DefaultFont);
            }
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="Text">Input string</param>
            /// <param name="StringFromat">String formatter (aligment, clipping)</param>
            /// <param name="Font">Font used to draw text</param>
            public void VAddText(String Text, StringFormat StringFromat, Font Font)
            {
                VAddText(Text, StringFromat, Font, PrintEngine.Instane.DefaultBrush);
            }
            /// <summary>
            /// Adding text to the element
            /// </summary>
            /// <param name="Text">Input string</param>
            /// <param name="StringFromat">String formatter (aligment, clipping)</param>
            /// <param name="Font">Font used to draw text</param>
            /// <param name="Brush">Brush used to draw text</param>
            public void VAddText(String Text, StringFormat StringFromat, Font Font, Brush Brush)
            {
                VAddPrimitive(new Vertical.Primitives.PrintPrimitiveText(Text, StringFromat, Font, Brush));
            }
        #endregion
        #region HAddImage
            /// <summary>
            /// Adding image to element
            /// </summary>
            /// <param name="FilePath">Path to bitmap file on HDD</param>
            public void HAddImage(String FilePath)
            {
                VAddPrimitive(new Vertical.Primitives.PrintPrimitiveImage(FilePath));
            }
            /// <summary>
            /// Adding image to element
            /// </summary>
            /// <param name="Image">Image object that will be printed</param>
            public void HAddImage(Image Image)
            {
                VAddPrimitive(new Vertical.Primitives.PrintPrimitiveImage(Image));
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
            /// <param name="Width">Width of the line</param>
            public void HAddLine(float Width)
            {
                HAddLine(PrintEngine.Instane.DefaultBrush, Width, -1, LineAlignment.Left);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="Width">Width of the line</param>
            public void HAddLine(Brush Brush)
            {
                HAddLine(Brush, 1);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="Brush">Brush used to draw the line</param>
            public void HAddLine(Brush Brush, float Width)
            {
                HAddLine(Brush, -1, Width, LineAlignment.Left);
            }
            /// <summary>
            /// Adding line to the element
            /// </summary>
            /// <param name="Brush">Brush used to draw line</param>
            /// <param name="Width">Width of the line</param>
            /// <param name="Length">Lenght of the line</param>
            /// <param name="Alignment">Alignment of the line</param>
            public void HAddLine(Brush Brush, float Length, float Width, LineAlignment Alignment)
            {
                VAddPrimitive(new Vertical.Primitives.PrintPrimitiveLine(Brush, Length, Width, Alignment));
            }
        #endregion
        #region HAddBlankLine
            /// <summary>
            /// Adding blank line to element
            /// </summary>
            public void HAddBlankLine()
            {
                VAddPrimitive(new Vertical.Primitives.PrintPrimitiveBlankLine());
            }
            /// <summary>
            /// Adding blank line to element
            /// </summary>
            /// <param name="Height">Height of the line</param>
            public void HAddBlankLine(float Height)
            {
                VAddPrimitive(new Vertical.Primitives.PrintPrimitiveBlankLine(Height));
            }
        #endregion
        #region HAddParagraph
            /// <summary>
        /// Adding blank line, text and line
        /// Only for Vertical mode
        /// </summary>
        /// <param name="buf">Input string</param>
            public void HAddParagraph(String Name)
            {
                HAddBlankLine();
                VAddText(Name);
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
            foreach (Vertical.IPrintPrimitiveVertical primitive in _printPrimitivesVertical)
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
        public void Draw(PrintEngine engine, Graphics graphics, Rectangle PageBounds)
        {
            foreach (Normal.IPrintPrimitive primitive in _printPrimitives)
            {
                // render it...
                primitive.Draw(engine, graphics, PageBounds);
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
            RectangleF ElementBounds = new RectangleF(pageBounds.Left, (int)yPos, pageBounds.Right - pageBounds.Left, (int)height);

            // now, tell the primitives to print themselves...
            foreach (Vertical.IPrintPrimitiveVertical primitive in _printPrimitivesVertical)
            {
                // render it...
                primitive.Draw(engine, graphics, ElementBounds);

                // move to the next line...
                yPos += primitive.CalculateHeight(engine, graphics);
                ElementBounds.Y = yPos;
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
