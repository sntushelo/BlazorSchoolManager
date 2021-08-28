using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities.Catalog;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}