using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities.Misc;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly IRepositoryAsync<DocumentType, int> _repository;

        public DocumentTypeRepository(IRepositoryAsync<DocumentType, int> repository)
        {
            _repository = repository;
        }
    }
}