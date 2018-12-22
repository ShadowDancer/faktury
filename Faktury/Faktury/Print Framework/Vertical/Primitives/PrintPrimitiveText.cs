using System;
using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    public class PrintPrimitiveText : IPrintPrimitiveVertical 
    {
        // members...
        private readonly string _buffer;
        private readonly StringFormat _stringFormat;
        private Font _font;
        private Brush _brush;

        public PrintPrimitiveText(String text, StringFormat stringFormat, Font font, Brush brush)
        {
            _buffer = text;
            _stringFormat = stringFormat;
            _font = font;
            _brush = brush;
        }
        // CalculateHeight - work out how tall the primitive is...
        public float CalculateHeight(PrintEngine engine, Graphics graphics)
        {
            float fontHeight;
            if (_font == null) fontHeight = engine.DefaultFont.GetHeight(graphics);
            else fontHeight = _font.GetHeight();

            // return the height...
            long count = 1;
            int start = 0;
            while ((start = _buffer.IndexOf('\n', start)) != -1)
            {
                count++;
                start++;
            }
            return count * fontHeight;


        }
        // Print - draw the text...
        public void Draw(PrintEngine engine, Graphics graphics, RectangleF elementBounds)
        {   
            if(_font == null)_font = engine.DefaultFont;
            if(_brush == null)_brush = engine.DefaultBrush;

            graphics.DrawString(engine.ReplaceTokens(_buffer), _font, _brush, elementBounds, _stringFormat);
        }
    }
}
