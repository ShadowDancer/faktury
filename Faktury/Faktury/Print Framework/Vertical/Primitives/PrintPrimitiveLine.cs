using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    public class PrintPrimitiveLine : IPrintPrimitiveVertical 
    {
        private readonly Brush _brush;
        private readonly LineAlignment _alignment;

        private readonly float _length;
        private readonly float _width;

        public PrintPrimitiveLine(Brush brush, float length, float width, LineAlignment alignment)
        {
            _brush = brush;
            _length = length;
            _width = width;
            _alignment = alignment;

            if (brush == null)
            {
                _brush = PrintEngine.Instane.DefaultBrush;
            }
        }

        public float CalculateHeight(PrintEngine engine, Graphics graphics)
        {
            return 4 + _width;
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF elementBounds)
        {

            Pen pen = new Pen(_brush, _width);
            PointF[] points = new PointF[2];

            elementBounds.Y = elementBounds.Y + 2;

                if (_length == -1)
                {
                    points[0] = new PointF(elementBounds.X, elementBounds.Y);
                    points[1] = new PointF(elementBounds.Right, elementBounds.Y);
                }
                else
                {
                    if (_alignment == LineAlignment.Left)
                    {
                        points[0] = new PointF(elementBounds.X, elementBounds.Y);
                        points[1] = new PointF(elementBounds.X + _length, elementBounds.Y);
                    }
                    if (_alignment == LineAlignment.Center)
                    {
                        points[0] = new PointF(elementBounds.X + (((elementBounds.Width - elementBounds.X) - _length) / 2), elementBounds.Y);
                        points[1] = new PointF(elementBounds.X - (((elementBounds.Width - elementBounds.X) - _length) / 2), elementBounds.Y);
                    }
                    if (_alignment == LineAlignment.Right)
                    {
                        points[0] = new PointF(elementBounds.Width - _length, elementBounds.Y);
                        points[1] = new PointF(elementBounds.Width, elementBounds.Y);                    
                    }
                }

            graphics.DrawLine(pen, points[0],  points[1]);
        }

    }
}