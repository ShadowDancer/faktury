using System;

namespace Faktury.Domain.Domain
{
    public class Service
    {
        public int Id = - 1;

        /// <summary>
        /// Name of service
        /// </summary>
        public string Name = "";
        public string Tag = "";
        public string Unit = "";
        public decimal PriceNet = 0;
        public int Vat = 0;

        public DateTime CreationDate;
        public DateTime ModificationDate;
        public string PKWiU = "";
    }
}
