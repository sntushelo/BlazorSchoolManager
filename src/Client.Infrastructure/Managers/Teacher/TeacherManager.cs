using BlazorSchoolManager.Application.Features.Teachers.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;
using BlazorSchoolManager.Client.Infrastructure.Extensions;
using System.Net.Http;
using MediatR;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Teacher
{
    public class TeacherManager : BaseApiService, ITeacherManager
    {
        private const string CONTROLLER_NAME = "teachers";

        public TeacherManager(HttpClient httpClient) : base(httpClient) { }

        public async Task<IResult<string>> GetAsync(int id) => await GetAsync($"{CONTROLLER_NAME}/{id}");
        public async Task<IResult<int>> SaveAsync(IRequest<Result<int>> command) => await SaveAsync(CONTROLLER_NAME, command);
        public async Task<IResult<int>> DeleteAsync(int id) => await DeleteAsync(CONTROLLER_NAME, id);

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
            => await ExportToExcelAsync(CONTROLLER_NAME, searchString);

        public async Task<IResult<string>> GetImageAsync(int id) => await GetImageAsync(CONTROLLER_NAME, id);

        public async Task<PaginatedResult<GetTeachersResponsePaged>> GetAsync(GetTeachersRequest request)
        {
            var response = await GetResponseAsync(EndpointHelper.GetAllPaged(CONTROLLER_NAME, request.PageNumber.ToString(), request.PageSize.ToString(), request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetTeachersResponsePaged>();
        }
    }
}
