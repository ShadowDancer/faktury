using System;
using System.Drawing;

namespace Faktury.Print_Framework.Normal.Primitives
{
    public class PrintText : IPrintPrimitive 
    {
        // members...
        public String _Buffer;
        public StringFormat _StringFormat = new StringFormat();
        public Font _Font = null;
        public Brush _Brush = null;
        public RectangleF Position { get; set; }

        public PrintText(String Text, StringFormat StringFormat, Font Font, Brush Brush, RectangleF Position)
        {
            _Buffer = Text;
            _StringFormat = StringFormat;
            _Font = Font;
            _Brush = Brush;
            this.Position = Position;
        }

        // Print - draw the text...
        public void Draw(PrintEngine engine, Graphics graphics, RectangleF PageBounds)
        {   
            if(_Font == null)_Font = engine.DefaultFont;
            if(_Brush == null)_Brush = engine.DefaultBrush;

            if (Position.Width == 0 && Position.Height == 0)
            {
                graphics.DrawString(engine.ReplaceTokens(_Buffer), _Font, _Brush, Position.X, Position.Y, _StringFormat);
            }
            else graphics.DrawString(engine.ReplaceTokens(_Buffer), _Font, _Brush, Position, _StringFormat);
        }
    }
}
