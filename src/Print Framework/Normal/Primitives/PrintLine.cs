using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    public class PrintLine : IPrintPrimitive
    {
        private readonly Brush _brush;

        private readonly float _width;
        private RectangleF Position { get; set; }

        public PrintLine(Brush brush, float width, RectangleF position)
        {
            _brush = brush;
            _width = width;

            Position = position;

            if (brush == null)
            {
                _brush = PrintEngine.Instane.DefaultBrush;
            }
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF pageBounds)
        {
            Pen pen = new Pen(_brush, _width);
            graphics.DrawLine(pen, Position.X, Position.Y, Position.Width, Position.Height);
        }

    }
}