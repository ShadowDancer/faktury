using System;
using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    public class PrintText : IPrintPrimitive 
    {
        // members...
        private readonly String _buffer;
        private readonly StringFormat _stringFormat;
        private Font _font;
        private Brush _brush;
        private RectangleF Position { get; set; }

        public PrintText(String text, StringFormat stringFormat, Font font, Brush brush, RectangleF position)
        {
            _buffer = text;
            _stringFormat = stringFormat;
            _font = font;
            _brush = brush;
            Position = position;
        }

        // Print - draw the text...
        public void Draw(PrintEngine engine, Graphics graphics, RectangleF pageBounds)
        {   
            if(_font == null)_font = engine.DefaultFont;
            if(_brush == null)_brush = engine.DefaultBrush;

            if (Position.Width <= 0.001 && Position.Height <= 0.001)
            {
                graphics.DrawString(engine.ReplaceTokens(_buffer), _font, _brush, Position.X, Position.Y, _stringFormat);
            }
            else graphics.DrawString(engine.ReplaceTokens(_buffer), _font, _brush, Position, _stringFormat);
        }
    }
}
