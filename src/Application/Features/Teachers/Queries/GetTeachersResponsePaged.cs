using BlazorSchoolManager.Application.Requests;

namespace BlazorSchoolManager.Application.Features.Teachers.Queries
{
    public class GetTeachersResponsePaged
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Description { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
    }

    public class GetTeachersRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}