using System;
using BlazorSchoolManager.Application.Requests;
namespace BlazorSchoolManager.Application.Features.Lessons.Queries
{
    
    public class GetLessonsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }

    public class GetLessonsResponsePaged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int TeacherId { get; set; }
        public int VenueId { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}