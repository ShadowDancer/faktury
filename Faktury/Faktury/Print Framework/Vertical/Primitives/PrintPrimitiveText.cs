using System;
using System.Drawing;

namespace Faktury.Print_Framework.Vertical.Primitives
{
    public class PrintPrimitiveText : IPrintPrimitiveVertical 
    {
        // members...
        public String _Buffer;
        public StringFormat _StringFormat = new StringFormat();
        public Font _Font = null;
        public Brush _Brush = null;

        public PrintPrimitiveText(String Text, StringFormat StringFormat, Font Font, Brush Brush)
        {
            _Buffer = Text;
            _StringFormat = StringFormat;
            _Font = Font;
            _Brush = Brush;
        }
        // CalculateHeight - work out how tall the primitive is...
        public float CalculateHeight(PrintEngine engine, Graphics graphics)
        {
            float FontHeight = 0;
            if (_Font == null) FontHeight = engine.DefaultFont.GetHeight(graphics);
            else FontHeight = _Font.GetHeight();

            // return the height...
            long count = 1;
            int start = 0;
            while ((start = _Buffer.IndexOf('\n', start)) != -1)
            {
                count++;
                start++;
            }
            return count * FontHeight;


        }
        // Print - draw the text...
        public void Draw(PrintEngine engine, Graphics graphics, RectangleF ElementBounds)
        {   
            if(_Font == null)_Font = engine.DefaultFont;
            if(_Brush == null)_Brush = engine.DefaultBrush;

            graphics.DrawString(engine.ReplaceTokens(_Buffer), _Font, _Brush, ElementBounds, _StringFormat);
        }
    }
}
