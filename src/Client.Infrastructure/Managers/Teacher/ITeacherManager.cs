using BlazorSchoolManager.Application.Features.Teachers.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Teacher
{
    public interface ITeacherManager : ISharedApiService
    {
        Task<PaginatedResult<GetTeachersResponsePaged>> GetAsync(GetTeachersRequest request);
    }
}
