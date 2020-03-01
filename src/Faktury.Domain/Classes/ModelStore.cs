using System.Collections.Generic;

namespace Faktury.Classes
{
    public partial class ModelStore
    {
        public ServiceRepository ServiceRepository { get; } = new ServiceRepository();
        public DocumentRepository DocumentRepository { get; } = new DocumentRepository();
    }

    [System.Runtime.InteropServices.Guid("15908FA9-6D26-4E64-BB98-06759F76DABF")]
    public partial class ModelStore
    {
        public CompanyRepository CompanyRepository
        {
            get { return _companyRepository; }
        }


        private readonly CompanyRepository _companyRepository = new CompanyRepository();
    }
}