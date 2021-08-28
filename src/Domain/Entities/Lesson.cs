using BlazorSchoolManager.Domain.Contracts;
using System;

namespace BlazorSchoolManager.Domain.Entities
{
    public class Lesson : AuditableEntity<int>
    {
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

        public Venue Venue { get; set; }
        public Teacher Teacher { get; set; }
    }
}
