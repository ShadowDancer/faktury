using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    internal class PrintPrimitiveBlankLine : IPrintPrimitiveVertical 
    {
        private readonly float _height;

            public PrintPrimitiveBlankLine(float height)
            {
                _height = height;
            }
            public PrintPrimitiveBlankLine()
            {
                _height = PrintEngine.Instane.DefaultFont.Height;
            }

            public float CalculateHeight(PrintEngine engine, Graphics graphics)
            {
                return _height;
            }

            public void Draw(PrintEngine engine, Graphics graphics, RectangleF elementBounds){ }
    }
}
