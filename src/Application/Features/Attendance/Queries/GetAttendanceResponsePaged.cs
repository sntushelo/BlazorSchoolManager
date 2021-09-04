using BlazorSchoolManager.Application.Requests;

namespace BlazorSchoolManager.Application.Features.Attendance.Queries
{
    public class GetAttendanceResponsePaged
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
    }

    public class GetAttendanceRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}