using System;
using System.Linq;
using System.Xml.Serialization;

namespace Faktury.Domain.Classes
{
    public class Company
    {
        public int Id;
        private string _shortName = "";
        public string Name { get; set; } = "";
        [XmlIgnore]
        public string ShortNameStripped { get; private set; }

        public string ShortName
        {
            get => _shortName;
            set
            {
                _shortName = value;
                if (value == null)
                {
                    ShortNameStripped = null;
                }
                else
                {
                    ShortNameStripped = new string(_shortName.Where(char.IsLetterOrDigit).ToArray());
                }
            }
        }

        public string Owner { get; set; } = "";
        public string Address { get; set; } = "";
        public string Street { get; set; } = "";
        public string Nip { get; set; } = "";
        public bool Bank { get; set; }
        public string BankAccount { get; set; } = "";
        public string BankSection { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
        public string MobileNumber { get; set; } = "";

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; } = DateTime.Now;
    }
}