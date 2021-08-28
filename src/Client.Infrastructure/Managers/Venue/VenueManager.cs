using BlazorSchoolManager.Application.Features.Venues.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;
using BlazorSchoolManager.Client.Infrastructure.Extensions;
using System.Net.Http;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Venue
{
    public class VenueManager : BaseApiService, IVenueManager
    {
        private const string CONTROLLER_NAME = "venues";

        public VenueManager(HttpClient httpClient) : base(httpClient) { }

        public async Task<IResult<string>> GetAsync(int id) => await GetAsync($"{CONTROLLER_NAME}/{id}");

        public async Task<IResult<int>> DeleteAsync(int id) => await DeleteAsync(CONTROLLER_NAME, id);

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
            => await ExportToExcelAsync(CONTROLLER_NAME, searchString);

        public async Task<IResult<string>> GetImageAsync(int id) => await GetImageAsync(CONTROLLER_NAME, id);

        public async Task<PaginatedResult<GetVenuesResponsePaged>> GetAsync(GetVenuesRequest request)
        {
            var response = await GetResponseAsync(EndpointHelper.GetAllPaged(CONTROLLER_NAME, request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetVenuesResponsePaged>();
        }
    }
}
