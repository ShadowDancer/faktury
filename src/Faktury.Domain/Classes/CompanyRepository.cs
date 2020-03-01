using System.Collections.Generic;

namespace Faktury.Classes
{
    public class CompanyRepository
    {
        private readonly List<Company> _companies = new List<Company>();
        private int _highestCompanyId;

        public IEnumerable<Company> Companies => _companies;
        public int NewCompanyId() => ++_highestCompanyId;

        public Company FindCompany(int companyId)
        {
            return _companies.Find(n => n.Id == companyId);
        }

        public void AddCompany(Company company)
        {
            _companies.Add(company);
            if (company.Id > _highestCompanyId)
            {
                _highestCompanyId = company.Id;
            }
        }

        public void RemoveCompany(Company companyToRemove)
        {
            _companies.Remove(companyToRemove);
        }

        public void ClearCompanies()
        {
            _companies.Clear();
        }
    }
}