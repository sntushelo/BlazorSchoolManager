using BlazorSchoolManager.Application.Requests;

namespace BlazorSchoolManager.Application.Features.Attendance.Queries
{
    public class GetAttendanceResponsePaged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool IsOnline { get; set; }
    }

    public class GetAttendanceRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}