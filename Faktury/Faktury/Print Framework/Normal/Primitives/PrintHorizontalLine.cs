using System;
using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    public class PrintHorizontalLine : IPrintPrimitive
    {
        Brush Brush;
        
        float Width = 0;
        float Height = 0;

        public PrintHorizontalLine(Brush Brush, float Width, float Height)
        {
            this.Brush = Brush;
            this.Width = Width;

            this.Height = Height;

            if (Brush == null) Brush = PrintEngine.Instane.DefaultBrush;
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF PageBounds)
        {
            Pen pen = new Pen(Brush, Width);
            graphics.DrawLine(pen, PageBounds.Left, Height, PageBounds.Right, Height);
        }

    }
}