using System.Collections.Generic;

namespace Faktury.Domain.Classes
{
    public class DocumentRepository
    {
        public readonly List<Document> Documents = new List<Document>();
        private readonly Dictionary<int, int> _highestDocumentInYearId = new Dictionary<int, int>();

        public int NewDocumentId(int year)
        {
            if (_highestDocumentInYearId.TryGetValue(year, out int value))
            {
                var newValue = value + 1;
                _highestDocumentInYearId[year] = newValue;
                return newValue;
            }

            _highestDocumentInYearId[year] = 1;
            return 1;
        }

        public void UpdateHighestDocumentId()
        {
            _highestDocumentInYearId.Clear();
            foreach (var currentDocument in Documents)
            {
                if (_highestDocumentInYearId.ContainsKey(currentDocument.Year))
                {
                    if (_highestDocumentInYearId[currentDocument.Year] < currentDocument.Number)
                    {
                        _highestDocumentInYearId[currentDocument.Year] = currentDocument.Number;
                    }
                }
                else
                {
                    if (currentDocument.Number > 0)
                        _highestDocumentInYearId.Add(currentDocument.Year, currentDocument.Number);
                    else
                        _highestDocumentInYearId.Add(currentDocument.Year, 1);
                }
            }
        }

        public Document FindDocument(int number, int year)
        {
            return Documents.Find(n => (n.Number == number && n.Year == year));
        }
    }
}