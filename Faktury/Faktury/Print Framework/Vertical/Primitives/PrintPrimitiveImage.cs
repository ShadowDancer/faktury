using System;
using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    class PrintPrimitiveImage : IPrintPrimitiveVertical 
    {
        Image Image;

            public PrintPrimitiveImage(string bmpPath)
            {
                Image = Image.FromFile( bmpPath );
            }

            public PrintPrimitiveImage(Image Image)
            {
                this.Image = Image;
            }

            public float CalculateHeight(PrintEngine engine, Graphics graphics)
            {
                return (Image.Height/Image.VerticalResolution) * 100;
            }


            public void Draw(PrintEngine engine, Graphics graphics, RectangleF ElementBounds)
            {
                graphics.DrawImage( Image, ElementBounds.X,ElementBounds.Y );
            }

    }
}
