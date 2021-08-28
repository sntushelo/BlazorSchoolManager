using BlazorSchoolManager.Application.Features.Lessons.Queries;
using BlazorSchoolManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Client.Infrastructure.Managers.Lesson
{
    public interface ILessonManager : ISharedApiService
    {
        Task<PaginatedResult<GetLessonsResponsePaged>> GetAsync(GetLessonsRequest request);
    }
}
