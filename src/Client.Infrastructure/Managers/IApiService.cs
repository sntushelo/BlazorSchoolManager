using BlazorSchoolManager.Client.Infrastructure.Extensions;
using BlazorSchoolManager.Shared.Wrapper;
using MediatR;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers
{
    public interface ISharedApiService : IManager
    {
        Task<IResult<int>> DeleteAsync(int id);
        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<IResult<string>> GetImageAsync(int id);
    }

    public interface IApiService
    {
        Task<HttpResponseMessage> GetResponseAsync(string endPoint);
        Task<IResult<string>> GetAsync(string endPoint);
        Task<IResult<string>> GetImageAsync(string controllerName, int id);
        Task<IResult<int>> SaveAsync(string controllername, IRequest<Result<int>> request);
        Task<IResult<int>> DeleteAsync(string controllername, int id);
        Task<IResult<string>> ExportToExcelAsync(string controllername, string searchString = "");
    }

    public class BaseApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public BaseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(string controllername, int id)
        {
            var response = await _httpClient.DeleteAsync($"{EndpointHelper.Delete(controllername)}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string controllername, string searchString = "")
        {
            var url = string.IsNullOrWhiteSpace(searchString)
                ? EndpointHelper.Export(controllername)
                : EndpointHelper.ExportFiltered(controllername, searchString);

            var response = await _httpClient.GetAsync(url);
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> GetAsync(string endPoint)
        {
            var response = await GetResponseAsync(endPoint);
            return await response.ToResult<string>();
        }

        public async Task<HttpResponseMessage> GetResponseAsync(string endPoint)
            => await _httpClient.GetAsync(endPoint);

        public async Task<IResult<string>> GetImageAsync(string controllerName, int id)
            => await GetAsync(EndpointHelper.GetImage(controllerName, id));

        public async Task<IResult<int>> SaveAsync(string controllername, IRequest<Result<int>> request)
        {
            var response = await _httpClient.PostAsJsonAsync(EndpointHelper.Save(controllername), request);
            return await response.ToResult<int>();
        }
    }
}