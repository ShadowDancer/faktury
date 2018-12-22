using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    internal class PrintPrimitiveImage : IPrintPrimitiveVertical 
    {
        private readonly Image _image;

            public PrintPrimitiveImage(string bmpPath)
            {
                _image = Image.FromFile( bmpPath );
            }

            public PrintPrimitiveImage(Image image)
            {
                _image = image;
            }

            public float CalculateHeight(PrintEngine engine, Graphics graphics)
            {
                return (_image.Height/_image.VerticalResolution) * 100;
            }


            public void Draw(PrintEngine engine, Graphics graphics, RectangleF elementBounds)
            {
                graphics.DrawImage( _image, elementBounds.X,elementBounds.Y );
            }

    }
}
