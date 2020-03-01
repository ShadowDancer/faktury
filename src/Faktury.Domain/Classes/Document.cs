using System;
using System.Collections.Generic;

namespace Faktury.Domain.Classes
{
    public class Document
    {
        /// <summary>
        ///     Company issuing document
        /// </summary>
        public Company Issuer { get; set; }

        /// <summary>
        ///     Company buying goods/services
        /// </summary>
        public Company Customer { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime SellDate { get; set; }

        public string PaymentType { get; set; }

        public string PaymentTime { get; set; }

        /// <summary>
        ///     Document number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     Document Year
        /// </summary>
        public int Year { get; set; }

        public List<DocumentItem> Items { get; } = new List<DocumentItem>();

        public DocumentSummary DocumentSummary { get; set; } = new DocumentSummary();

        /// <summary>
        /// Odwrotne obciążeniee
        /// </summary>
        public bool ReverseVAT { get; set; }

        public static Document CreateNewDocument()
        {
            var newDocument = new Document
            {
                PaymentType = "Przelew",
                PaymentTime = "14 dni",
                Year = DateTime.Today.Year,
                IssueDate = DateTime.Today,
                SellDate = DateTime.Today
            };

            return newDocument;
        }
    }
}