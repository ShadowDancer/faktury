using System;

namespace Faktury.Classes
{
    public class Company
    {
        public string Name { get; set; } = "";
        public string Owner { get; set; } = "";
        public string Address { get; set; } = "";
        public string Street { get; set; } = "";
        public string Nip { get; set; } = "";
        public string Tag { get; set; } = "";
        public bool Bank { get; set; }
        public string BankAccount { get; set; } = "";
        public string BankSection { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string MobileNumber { get; set; } = "";

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; } = DateTime.Now;

        public int Id;
    }
}