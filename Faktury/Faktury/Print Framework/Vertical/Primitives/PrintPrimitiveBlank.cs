using System;
using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    class PrintPrimitiveBlankLine : IPrintPrimitiveVertical 
    {
        float Height;

            public PrintPrimitiveBlankLine(float Height)
            {
                this.Height = Height;
            }
            public PrintPrimitiveBlankLine()
            {
                Height = PrintEngine.Instane.DefaultFont.Height;
            }

            public float CalculateHeight(PrintEngine engine, Graphics graphics)
            {
                return Height;
            }

            public void Draw(PrintEngine engine, Graphics graphics, RectangleF ElementBounds){ }
    }
}
