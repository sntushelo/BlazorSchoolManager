using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly IRepositoryAsync<Venue, int> _repository;

        public VenueRepository(IRepositoryAsync<Venue, int> repository)
        {
            _repository = repository;
        }
    }
}