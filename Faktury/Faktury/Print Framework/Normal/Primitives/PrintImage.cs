using System;
using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    class PrintImage : IPrintPrimitive
    {
        Image Image;
        PointF Position { get; set; }

            public PrintImage(string bmpPath, PointF Position)
            {
                Image = Image.FromFile( bmpPath );
                this.Position = Position;
            }

            public PrintImage(Image Image, PointF Position)
            {
                this.Image = Image;
                this.Position = Position;
            }

            public void Draw(PrintEngine engine, Graphics graphics, RectangleF PageBounds)
            {
                graphics.DrawImage(Image, Position.X, Position.Y);
            }

    }
}
