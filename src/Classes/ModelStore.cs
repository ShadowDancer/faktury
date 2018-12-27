using System.Collections.Generic;

namespace Faktury.Classes
{
    public class ModelStore
    {
        public IEnumerable<Company> Companies => _companies;
        public IEnumerable<Service> Services => _services;

        private readonly List<Company> _companies = new List<Company>();
        public readonly List<Document> Documents = new List<Document>();
        private readonly List<Service> _services = new List<Service>();
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

        public void UpdateHighestCompanyId()
        {
            foreach (Company currentCompany in _companies)
            {
                if (currentCompany.Id > _highestCompanyId) _highestCompanyId = currentCompany.Id;
            }
        }

        public void UpdateHigestServiceId()
        {
            foreach (Service currentService in _services)
            {
                if (currentService.Id > _highestServiceId) _highestServiceId = currentService.Id;
            }
        }

        public Document FindDocument(int number, int year)
        {
            return Documents.Find(n => (n.Number == number && n.Year == year));
        }

        public Company FindCompany(int companyId)
        {
            return _companies.Find(n => n.Id == companyId);
        }

        public void AddCompany(Company company)
        {
            _companies.Add(company);
        }

        public void RemoveCompany(Company companyToRemove)
        {
            _companies.Remove(companyToRemove);
        }

        public void ClearCompanies()
        {
            _companies.Clear();
        }

        public void AddService(Service service)
        {
            _services.Add(service);
        }

        public Service FindService(int id)
        {
            return _services.Find(n => n.Id == id);
        }

        public void RemoveService(Service serviceToRemove)
        {
            _services.Remove(serviceToRemove);
        }

        public void ClearServices()
        {
            _services.Clear();
        }
    }
}