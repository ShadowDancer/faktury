using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    internal class PrintRectangle : IPrintPrimitive
    {
        private readonly Brush _brush;
        private readonly bool _filled;
        private readonly float _width;
        private RectangleF Position { get; set; }

        public PrintRectangle(Brush brush, float width, bool filled, RectangleF position)
        {
            _brush = brush;
            _width = width;
            _filled = filled;
            Position = position;

            if (brush == null)
            {
                _brush = PrintEngine.Instane.DefaultBrush;
            }
        }

        public void Draw(PrintEngine engine, Graphics graphics, RectangleF pageBounds)
        {
            Pen pen = new Pen(_brush, _width);
            if (_filled == false) graphics.DrawRectangle(pen, Position.X, Position.Y, Position.Width, Position.Height);
            else graphics.FillRectangle(_brush, Position.X, Position.Y, Position.Width, Position.Height);
        }
    }
}
