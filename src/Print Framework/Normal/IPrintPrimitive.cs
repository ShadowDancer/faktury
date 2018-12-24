using System.Drawing;

namespace Faktury.Print_Framework.Normal
{
    /// <summary>
    /// All primitive classes should inherit this interface
    /// </summary>
    public interface IPrintPrimitive
    {
        /// <summary>
        /// To override. This function should psyhically draw primitive
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="graphics"></param>
        /// <param name="pageBounds"></param>
        void Draw(PrintEngine engine, Graphics graphics, RectangleF pageBounds);
    } 
}
