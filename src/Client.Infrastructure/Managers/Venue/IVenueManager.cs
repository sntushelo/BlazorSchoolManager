using BlazorSchoolManager.Application.Features.Venues.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Venue
{
    public interface IVenueManager : ISharedApiService
    {
        Task<PaginatedResult<GetVenuesResponsePaged>> GetAsync(GetVenuesRequest request);
    }
}
