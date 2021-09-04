using BlazorSchoolManager.Application.Features.Students.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Student
{
    public interface IStudentManager : ISharedApiService
    {
        Task<PaginatedResult<GetStudentsResponsePaged>> GetAsync(GetStudentsRequest request);
    }
}
