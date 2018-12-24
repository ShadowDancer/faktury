using System;

namespace Faktury.Classes
{
    [Serializable]
    public class Document
    {
        public int CompanyId { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime SellDate { get; set; }

        public string Payment { get; set; }
        public string PaymentTime { get; set; }

        public string Name { get; set; }
        public bool DefaultName { get; set; }

        public int Number { get; set; }
        public int Year { get; set; }

        public MoneyData MoneyData { get; set; } = new MoneyData();

        public bool Paid { get; set; }

        public static Document CreateNewDocument()
        {

            Document newDocument = new Document
            {
                Name = "bez_nazwy",
                DefaultName = true,
                Payment = "Przelew",
                PaymentTime = "14 dni",
                Year = DateTime.Today.Year,
                IssueDate = DateTime.Today,
                SellDate = DateTime.Today
            };

            return newDocument;
        }
    }
}
