using BlazorSchoolManager.Application.Features.Attendance.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Attendance
{
    public interface IAttendanceManager : ISharedApiService
    {
        Task<PaginatedResult<GetAttendanceResponsePaged>> GetAsync(GetAttendanceRequest request);
    }
}
