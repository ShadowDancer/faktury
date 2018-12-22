using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    public class PrintHorizontalLine : IPrintPrimitive
    {
        private readonly Brush _brush;

        private readonly float _width;
        private readonly float _height;

        public PrintHorizontalLine(Brush brush, float width, float height)
        {
            _brush = brush;
            _width = width;

            _height = height;

            if (brush == null)
            {
                _brush = PrintEngine.Instane.DefaultBrush;
            }
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF pageBounds)
        {
            Pen pen = new Pen(_brush, _width);
            graphics.DrawLine(pen, pageBounds.Left, _height, pageBounds.Right, _height);
        }

    }
}