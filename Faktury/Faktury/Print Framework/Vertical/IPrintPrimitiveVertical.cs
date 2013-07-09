using System;
using System.Drawing; 

namespace Faktury.Print_Framework.Vertical
{
    /// <summary>
    /// All primitive classes should inherit this interface
    /// </summary>
    public interface IPrintPrimitiveVertical
    {
        /// <summary>
        /// To override. Return height of the primitive
        /// </summary>
        float CalculateHeight(PrintEngine engine, Graphics graphics);

        /// <summary>
        /// To override. This function should physically draw primitive
        /// </summary>
        void Draw(PrintEngine engine, Graphics graphics, RectangleF elementBounds);
    } 
}
