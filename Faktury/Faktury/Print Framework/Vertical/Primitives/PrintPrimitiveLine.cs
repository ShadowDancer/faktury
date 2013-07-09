using System;
using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    public class PrintPrimitiveLine : IPrintPrimitiveVertical 
    {
        Brush Brush;
        LineAlignment Alignment;
        
        float Length = -1;
        float Width = 0;

        public PrintPrimitiveLine(Brush Brush, float Length, float Width, LineAlignment Alignment)
        {
            this.Brush = Brush;
            this.Length = Length;
            this.Width = Width;
            this.Alignment = Alignment;

            if (Brush == null) Brush = PrintEngine.Instane.DefaultBrush;
        }

        public float CalculateHeight(PrintEngine engine, Graphics graphics)
        {
            return 4 + Width;
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF ElementBounds)
        {

            Pen pen = new Pen(Brush, Width);
            PointF[] Points = new PointF[2];

            ElementBounds.Y = ElementBounds.Y + 2;

                if (Length == -1)
                {
                    Points[0] = new PointF(ElementBounds.X, ElementBounds.Y);
                    Points[1] = new PointF(ElementBounds.Right, ElementBounds.Y);
                }
                else
                {
                    if (Alignment == LineAlignment.Left)
                    {
                        Points[0] = new PointF(ElementBounds.X, ElementBounds.Y);
                        Points[1] = new PointF(ElementBounds.X + Length, ElementBounds.Y);
                    }
                    if (Alignment == LineAlignment.Center)
                    {
                        Points[0] = new PointF(ElementBounds.X + (((ElementBounds.Width - ElementBounds.X) - Length) / 2), ElementBounds.Y);
                        Points[1] = new PointF(ElementBounds.X - (((ElementBounds.Width - ElementBounds.X) - Length) / 2), ElementBounds.Y);
                    }
                    if (Alignment == LineAlignment.Right)
                    {
                        Points[0] = new PointF(ElementBounds.Width - Length, ElementBounds.Y);
                        Points[1] = new PointF(ElementBounds.Width, ElementBounds.Y);                    
                    }
                }

            graphics.DrawLine(pen, Points[0],  Points[1]);
        }

    }
}