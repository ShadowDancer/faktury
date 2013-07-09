using System;
using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    class PrintRectangle : IPrintPrimitive
    {
        Brush Brush;
        bool Filled;
        float Width = 0;
        RectangleF Position { get; set; }

        public PrintRectangle(Brush Brush, float Width, bool Filled, RectangleF Position)
        {
            this.Brush = Brush;
            this.Width = Width;
            this.Filled = Filled;
            this.Position = Position;

            if (Brush == null) Brush = PrintEngine.Instane.DefaultBrush;
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF PageBounds)
        {
            Pen pen = new Pen(Brush, Width);
            if (Filled == false) graphics.DrawRectangle(pen, Position.X, Position.Y, Position.Width, Position.Height);
            else graphics.FillRectangle(Brush, Position.X, Position.Y, Position.Width, Position.Height);
        }
    }
}
