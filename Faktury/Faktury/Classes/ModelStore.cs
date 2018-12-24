using System.Collections.Generic;

namespace Faktury.Classes
{
    public class ModelStore
    {
        public readonly List<Company> Companies = new List<Company>();
        public readonly List<Document> Documents = new List<Document>();
        public readonly List<Service> Services = new List<Service>();
        private readonly Dictionary<int, int> _highestDocumentInYearId = new Dictionary<int, int>();
        private int _highestCompanyId;
        private int _highestServiceId;

        public int NewCompanyId() => ++_highestCompanyId;
        public int NewServiceId() => ++_highestServiceId;

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

        public void UpdateHigestCompanyId()
        {
            foreach (Company currentComapny in Companies)
            {
                if (currentComapny.Id > _highestCompanyId) _highestCompanyId = currentComapny.Id;
            }
        }

        public void UpdateHigestServiceId()
        {
            foreach (Service currentService in Services)
            {
                if (currentService.Id > _highestServiceId) _highestServiceId = currentService.Id;
            }
        }

        public Document FindDocument(int number, int year)
        {
            return Documents.Find(n => (n.Number == number && n.Year == year));
        }

    }
}