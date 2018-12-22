using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    internal class PrintImage : IPrintPrimitive
    {
        private readonly Image _image;
        private PointF Position { get; set; }

            public PrintImage(string bmpPath, PointF position)
            {
                _image = Image.FromFile( bmpPath );
                Position = position;
            }

            public PrintImage(Image image, PointF position)
            {
                _image = image;
                Position = position;
            }

            public void Draw(PrintEngine engine, Graphics graphics, RectangleF pageBounds)
            {
                graphics.DrawImage(_image, Position.X, Position.Y);
            }

    }
}
