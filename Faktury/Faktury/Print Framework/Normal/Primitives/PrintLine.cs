using System;
using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    public class PrintLine : IPrintPrimitive
    {
        Brush Brush;
        
        float Width = 0;
        RectangleF Position { get; set; }

        public PrintLine(Brush Brush, float Width, RectangleF Position)
        {
            this.Brush = Brush;
            this.Width = Width;

            this.Position = Position;

            if (Brush == null) Brush = PrintEngine.Instane.DefaultBrush;
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF PageBounds)
        {
            Pen pen = new Pen(Brush, Width);
            graphics.DrawLine(pen, Position.X, Position.Y, Position.Width, Position.Height);
        }

    }
}