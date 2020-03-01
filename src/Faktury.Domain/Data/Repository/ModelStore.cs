namespace Faktury.Domain.Data.Repository
{
    public class ModelStore
    {
        public ServiceRepository ServiceRepository { get; } = new ServiceRepository();
        public DocumentRepository DocumentRepository { get; } = new DocumentRepository();
        public CompanyRepository CompanyRepository { get; } = new CompanyRepository();
    }
}