namespace Faktury.Print_Framework
{
    /// <summary>
    /// All printable objects in applications should inherit this interface 
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// To override. Data added to element will be printed.
        /// </summary>
        void Print(PrintElement element);
    }

}
